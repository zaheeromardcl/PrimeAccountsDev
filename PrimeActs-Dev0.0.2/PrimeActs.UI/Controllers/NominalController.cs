using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrimeActs.Domain.ViewModels.Nominal;
using PrimeActs.Orchestras;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using CsvHelper;
using System.IO;
using System.Web.Script.Serialization;
using AutoMapper;
using PrimeActs.Domain;
using System.Net.Http.Formatting;
using System.Web.Http;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.UI.Controllers
{

    public class FromJsonAttribute : CustomModelBinderAttribute
    {
        private readonly static JavaScriptSerializer Serializer = new JavaScriptSerializer();

        public override IModelBinder GetBinder()
        {
            return new JsonModelBinder();
        }


        private class JsonModelBinder : IModelBinder
        {
            public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
            {
                var stringified = controllerContext.HttpContext.Request[bindingContext.ModelName];
                if (string.IsNullOrEmpty(stringified)) return null;
                return Serializer.Deserialize(stringified, bindingContext.ModelType);
            }
        }
    }


    public class NominalController : Controller
    {
        private readonly INominalOrchestra _nominalOrchestra;
        private readonly ITempBankNominalOrchestra _tempBankNominalOrchestra;
        private readonly IBankStatementOrchestra _bankStatementOrchestra;
        private readonly IBankStatementService _bankStatementService;
        private readonly IBankStatementItemService _bankStatementItemService;
        private readonly IUnitOfWorkAsync _unitofWork;
        private readonly ISetupLocalService _setupService;
        private readonly IBankStatementItemNominalLedgerEntryService _bankStatementItemNominalLedgerEntryService;
        private readonly ITempBankStatementItemNominalLedgerEntryService _tempBankStatementItemNominalLedgerEntryService;
        private string _serverCode = "L";//Need to change with actual at runtime.

        public NominalController(INominalOrchestra nominalOrchestra, IBankStatementOrchestra bankStatementOrchestra, IBankStatementService bankStatementService, IBankStatementItemService bankStatementItemService, IUnitOfWorkAsync unitofWork, ITempBankNominalOrchestra tempBankNominalOrchestra, ISetupLocalService setupLocalService, IBankStatementItemNominalLedgerEntryService bankStatementItemNominalLedgerEntryService, ITempBankStatementItemNominalLedgerEntryService tempBankStatementItemNominalLedgerEntryService)
        {
            _nominalOrchestra = nominalOrchestra;
            _tempBankNominalOrchestra = tempBankNominalOrchestra;
            _bankStatementOrchestra = bankStatementOrchestra;
            _bankStatementService = bankStatementService;
            _bankStatementItemService = bankStatementItemService;
            _unitofWork = unitofWork;
            _setupService = setupLocalService;
            _bankStatementItemNominalLedgerEntryService = bankStatementItemNominalLedgerEntryService;
            _tempBankStatementItemNominalLedgerEntryService = tempBankStatementItemNominalLedgerEntryService;
        }
        public PrimeActsUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<PrimeActsUserManager>(); }
            private set { _userManager = value; }
        }

        public void PopulateUser()
        {
            var user = User.Identity.GetApplicationUser();
            var appUser = new PrimeActs.Domain.ApplicationUser
            {
                Id = user.Id,
                UserName = user.UserName,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Nickname = user.Nickname,
                CompanyId = user.CompanyId,
                DepartmentId = user.DepartmentId,
                DivisionId = user.DivisionId
            };

            _nominalOrchestra.Initialize(appUser);


            //_nominalOrchestra.Initialize(new PrimeActs.Domain.ApplicationUser
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Firstname = user.Firstname,
            //    Lastname = user.Lastname,
            //    Nickname = user.Nickname,
            //    CompanyId = user.CompanyId,
            //    DepartmentId = user.DepartmentId,
            //    DivisionId = user.DivisionId
            //});


        }

        private PrimeActsUserManager _userManager;

        // GET: Nominal
        public ActionResult Index()
        {
            return View();
        }

        //[System.Web.Mvc.HttpPost]
        //public bool PostChanges([FromJson] ReconciliationTestModel reconciliationsModel)
        //{
        //    return true;
        //}

        //[System.Web.Http.HttpPost]
        //public bool PostChanges2([FromBody]FormDataCollection formbody)
        //{
        //    int n = 0;
        //    return true;
        //}

        public TempBankNominalLedgerEntry MapLedgerEntriesToViewModel(ref ReconciliationTestModel reconciliationTestModel)
        {
            // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<ReconciliationTestModel, TempBankNominalLedgerEntry>()
                    .ForMember(dest => dest.TempBankNominalLedgerEntryID, opt => opt.MapFrom(src => src.RecId))
                    .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.Amount))
                    .ForMember(dest => dest.BankReconciliationDate, opt => opt.MapFrom(src => src.Date))
                    .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => src.IsReconciled))
                    .ForMember(dest => dest.NominalLedgerEntryID, opt => opt.MapFrom(src => src.RecId))
                    .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Type))
                    .ForMember(dest => dest.TempDescriptionn, opt => opt.MapFrom(src => src.Description))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper

            TempBankNominalLedgerEntry tempLedgerModel = new TempBankNominalLedgerEntry();
            mapper.Map(reconciliationTestModel, tempLedgerModel); // Map CSV to Output List
            return tempLedgerModel;
        }

        [System.Web.Mvc.HttpPost]
        public bool PostReconciliationChange(ReconciliationTestModel reconciliationTestModel)
        {

            // if (reconciliationTestModel.IsReconciled)
            //  {
            //Console.WriteLine(reconciliationTestModel.Description);
            _tempBankNominalOrchestra.UpdateIsReconciled(reconciliationTestModel.RecId, reconciliationTestModel.IsReconciled);

            // Save Match record entries
            if (reconciliationTestModel.IsReconciled == true) // add match entries
            {
                foreach (var statementItemId in reconciliationTestModel.ReconciledStatements)
                {

                    _tempBankNominalOrchestra.SaveTempStatementMatch(reconciliationTestModel.RecId,
                        statementItemId);
                }
            }
            else // delete match entries
            {
                _tempBankNominalOrchestra.DeleteTempStatementMatch(reconciliationTestModel.RecId);
            }

            return true;
        }

        [System.Web.Mvc.HttpPost]
        public bool PostChanges(List<ReconciliationTestModel> reconciliationTestModels)
        {
            foreach (var r in reconciliationTestModels)
            {
                if (r.IsReconciled)
                {
                    Console.WriteLine(r.Description);
                }
            }

            return true;
        }

        [System.Web.Mvc.HttpPost]
        public bool PostStatementChanges(List<StatementImportTestModel> statementImportTestModel)
        {
            foreach (var r in statementImportTestModel)
            {
                if (r.IsReconciled)
                {
                    Console.WriteLine(r.Description);
                }
            }

            return true;
        }

        [System.Web.Mvc.HttpPost]
        public bool PostBothChanges(List<StatementImportTestModel> statementImportTestModel, List<ReconciliationTestModel> reconciliationTestModel)
        //public bool PostBothChanges(StatementPostModel statementPostModel)
        {
            //foreach (var r in statementPostModel)
            //{
            //    if (r.IsReconciled)
            //    {
            //        Console.WriteLine(r.Description);
            //    }
            //}

            return true;
        }

        public void SaveStatementChanges(ref List<StatementImportTestModel> statementImportTestModel)
        {
            // _bankStatementItemService.InsertOrUpdateGraph();
        }


        // GET: Nominal
        public ActionResult BankReconciliation()
        {
            return View();
        }

        // GET: Nominal
        public ActionResult BankRecProto()
        {
            ReconciliationViewModel reconciliationViewModel = new ReconciliationViewModel();

            //GetStatementTestData(ref reconciliationViewModel);
            var user = User.Identity.GetApplicationUser();
            var statementHeaderparm = Request.QueryString["ID"].ToString();
            Guid statementHeaderID = new Guid(statementHeaderparm);

            BankStatement bankStatementHeader = GetValueByStatementHeaderID(statementHeaderID);
            //GetValue(user, out bankStatementHeader); // dummy test data
            GetStatementCSVData(ref reconciliationViewModel, ref bankStatementHeader);
            reconciliationViewModel.ReconciliationItems = GetNominalEntries(ref bankStatementHeader);
            reconciliationViewModel.BankStatementID = statementHeaderID;
            MarkWhereNoPossibleMatches(ref reconciliationViewModel);

            //TestCSVImport();
            return View(reconciliationViewModel);
        }

        private void MarkWhereNoPossibleMatches(ref ReconciliationViewModel reconciliationViewModel)
        {
            foreach (var m in reconciliationViewModel.StatementImportItems)
            {
                bool found = reconciliationViewModel.ReconciliationItems.Any(a => a.Amount == m.Amount);
                m.HasPossibleMatchingNominal = found;
            }
        }


        // Get Statement Header
        public ActionResult BankReconciliationSelect()
        {
            DateTime startDate = DateTime.Today.AddDays(-7);
            DateTime endDate = DateTime.Today;

            BankReconciliationHeaderViewModel reconciliationHeaderViewModel = new BankReconciliationHeaderViewModel();

            // Get Items ordered by Last Edited By User
            var allStatementHeaders = _bankStatementService.Query().Select().OrderByDescending(a => a.CreatedDate).ToList();

            // last worked on becomes active

            string statementHeaderparm = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
            //statementHeaderparm = Request.QueryString["bankrecprotoID"].ToString();

            BankStatement bankStatementHeader;
            // Get Active Reconciliation, if any. Will be if there is any entries in the tblTempBankNominalLedgerEntry table, which should only have one unique BankStatementID
            var activeReconciliation = _tempBankNominalOrchestra.GetCurrentStatementInReconciliation();


            if (statementHeaderparm != "")
            {
                Guid statementHeaderID = new Guid(statementHeaderparm);
                bankStatementHeader = GetValueByStatementHeaderID(statementHeaderID);
                reconciliationHeaderViewModel.BankStatementStartDate = ((DateTime)bankStatementHeader.StartDate).ToShortDateString();
                reconciliationHeaderViewModel.BankStatementEndDate = ((DateTime)bankStatementHeader.EndDate).ToShortDateString();
                reconciliationHeaderViewModel.CurrentBalance = _tempBankNominalOrchestra.GetTotalIsReconciled(bankStatementHeader.BankStatementID);
            }
            else
            {
                // temp - need to get the active
                // bankStatementHeader = GetBankStatementHeader("natwest.csv");
                if (activeReconciliation != null)
                {
                    bankStatementHeader = _bankStatementOrchestra.GetBankStatementHeaderByID(activeReconciliation.Value);
                    reconciliationHeaderViewModel.BankStatementStartDate = startDate.ToShortDateString();
                    reconciliationHeaderViewModel.BankStatementEndDate = endDate.ToShortDateString();
                    reconciliationHeaderViewModel.CurrentBalance = _tempBankNominalOrchestra.GetTotalIsReconciled(bankStatementHeader.BankStatementID);
                }
                else
                {
                    bankStatementHeader = new BankStatement();
                    reconciliationHeaderViewModel.CurrentBalance = bankStatementHeader.CurrentBalance.GetValueOrDefault();
                }
            }

            reconciliationHeaderViewModel.BankStatementID = bankStatementHeader.BankStatementID;
            reconciliationHeaderViewModel.BankStatementFileName = bankStatementHeader.BankStatementFileName;
            reconciliationHeaderViewModel.BankStatementImportDate = bankStatementHeader.BankStatementImportDate.GetValueOrDefault().ToShortDateString();
            reconciliationHeaderViewModel.BankStatementReconciled = bankStatementHeader.BankStatementReconciled;

            reconciliationHeaderViewModel.OpeningBalance = bankStatementHeader.OpeningBalance.GetValueOrDefault();
            reconciliationHeaderViewModel.BankAccountID = Guid.NewGuid();
            reconciliationHeaderViewModel.CanCallDetailsPage = activeReconciliation == null ? false : true;
            reconciliationHeaderViewModel.CanBeReconciled =
                _tempBankNominalOrchestra.AllStatementItemsReconciled(bankStatementHeader.BankStatementID);
            // reconciliationHeaderViewModel.CurrentBalance = reconciliationHeaderViewModel.CurrentBalance +
            // reconciliationHeaderViewModel.OpeningBalance;

            // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<BankStatement, BankReconciliationHeaderItem>()
                    .ForMember(dest => dest.BankStatementID, opt => opt.MapFrom(src => src.BankStatementID))
                    .ForMember(dest => dest.BankStatementEndDate, opt => opt.MapFrom(src => src.EndDate.GetValueOrDefault().ToShortDateString()))
                    .ForMember(dest => dest.BankStatementStartDate, opt => opt.MapFrom(src => src.StartDate.GetValueOrDefault().ToShortDateString()))
                    .ForMember(dest => dest.BankStatementFileName, opt => opt.MapFrom(src => src.BankStatementFileName))
                    .ForMember(dest => dest.BankStatementImportDate, opt => opt.MapFrom(src => src.BankStatementImportDate))
                    .ForMember(dest => dest.BankStatementReconciled, opt => opt.MapFrom(src => src.BankStatementReconciled))
                    .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.CurrentBalance))
                    .ForMember(dest => dest.OpeningBalance, opt => opt.MapFrom(src => src.OpeningBalance))
                    .ForMember(dest => dest.BankAccountID, opt => opt.MapFrom(src => Guid.NewGuid()))
                // .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            //var ledgerViewModel = new List<ReconciliationTestModel>(); // Instantiaze output Statements List

            mapper.Map(allStatementHeaders, reconciliationHeaderViewModel.BankReconciliationHeaderItems); // View Model

            var maxFileSize = _setupService.GetDisplayOption("MaxFileSizeToUpload").SetupValueInt;
            var maxNrOfFiles = _setupService.GetDisplayOption("MaxNrOfFilesToUpload").SetupValueInt;
            var mainFolder = _setupService.GetDisplayOption("UploadFolderPath").SetupValueNvarchar;
            var uploadFolder = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            var acceptedFileTypes = _setupService.GetDisplayOption("AllowedFileTypesToUpload").SetupValueNvarchar;

            reconciliationHeaderViewModel.MainFolder = mainFolder;
            reconciliationHeaderViewModel.MaxFileSize = maxFileSize.GetValueOrDefault();
            reconciliationHeaderViewModel.MaxNrOfFiles = maxNrOfFiles.GetValueOrDefault();
            reconciliationHeaderViewModel.UploadFolder = uploadFolder.ToString();
            reconciliationHeaderViewModel.AcceptedFileTypes = acceptedFileTypes;

            var user = User.Identity.GetApplicationUser();

            // Get Active Reconciliation, if any. Will be if there is any entries in the tblTempBankNominalLedgerEntry table, which should only have one unique BankStatementID
            //var activeReconciliation = _tempBankNominalOrchestra.GetCurrentStatementInReconciliation();
            if (activeReconciliation != null && reconciliationHeaderViewModel.BankStatementReconciled == false)
            {
                foreach (var s in reconciliationHeaderViewModel.BankReconciliationHeaderItems)
                {
                    s.IsActiveReconciliation = s.BankStatementID == activeReconciliation.Value ? true : false;
                    s.CurrentBalance = s.BankStatementID == bankStatementHeader.BankStatementID
                        ? reconciliationHeaderViewModel.CurrentBalance
                        : s.CurrentBalance;
                    s.CanBeReconciled = _tempBankNominalOrchestra.AllStatementItemsReconciled(s.BankStatementID);
                    if (s.BankStatementReconciled) s.IsActiveReconciliation = false;
                }
            }
            else // if no active reconciliation then set all non completed to active
            {
                foreach (var s in reconciliationHeaderViewModel.BankReconciliationHeaderItems)
                {
                    if (s.CanBeReconciled == false && s.BankStatementReconciled == false)
                    {
                        s.IsActiveReconciliation = true;
                    }
                }
            }

            //TestCSVImport();
            return View(reconciliationHeaderViewModel);
        }

        private void GetValue(ApplicationUser user, out BankStatement bankStatementHeader)
        {
            bankStatementHeader = GetBankStatementHeader("natwest.csv");
            if (bankStatementHeader == null)
            {
                bankStatementHeader = new BankStatement
                {
                    BankStatementID = Guid.NewGuid(),
                    BankStatementFileName = "natwest.csv",
                    BankStatementImportDate = DateTime.Now,
                    BankStatementReconciled = false,
                    UpdatedByUserID = user.Id,
                    UpdatedDate = DateTime.Now,
                    CreatedByUserID = user.Id,
                    CreatedDate = DateTime.Now
                };

                _bankStatementService.Insert(bankStatementHeader);
                _unitofWork.SaveChanges();
            }
        }

        private BankStatement GetValueByStatementHeaderID(Guid BankStatementID)
        {
            var bankStatementHeader = _bankStatementOrchestra.GetBankStatementHeaderByID(BankStatementID);
            return bankStatementHeader;
        }

        public void SetStatementIsReconciled(ref List<StatementImportTestModel> statements)
        {
            //NOTE Might be quicker to load all in one trip?
            foreach (var s in statements)
            {
                s.IsReconciled = _tempBankNominalOrchestra.StatementIsMatched(s.StatementId);
            }
        }

        public void GetStatementCSVData(ref ReconciliationViewModel reconciliationViewModel, ref BankStatement bankStatementHeader)
        {
            var statementList = GetCSVImport(ref bankStatementHeader);
            SetStatementIsReconciled(ref statementList); // set isReconciled Flag
            reconciliationViewModel.StatementImportItems.AddRange(statementList);
            foreach (var s in statementList)
            {
                ReconciliationTestModel t1 = new ReconciliationTestModel
                {
                    RecId = Guid.NewGuid(),
                    Amount = s.Amount,
                    Balance = s.Amount,
                    Date = s.Date,
                    Description = s.Description,
                    Type = s.Type,
                    IsSelected = false
                };
                reconciliationViewModel.ReconciliationItems.Add(t1);
            }

        }

        public BankStatement GetBankStatementHeader(string fileName)
        {
            var bankStatementHeader = _bankStatementOrchestra.GetBankStatementHeader(fileName);
            return bankStatementHeader;
        }

        public JsonResult AddBankStatementHeader()
        {
            var user = User.Identity.GetApplicationUser();
            var bankStatementID = Guid.NewGuid();
            DateTime startDate = DateTime.Today.AddDays(-7);
            DateTime endDate = DateTime.Today;

            BankStatement newBankStatement = new BankStatement
            {
                BankStatementID = bankStatementID,
                CreatedByUserID = user.Id,
                //BankStatementFileName = "natwest.csv",
                //CreatedDate = DateTime.Today,
                //UpdatedDate = DateTime.Today,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UpdatedByUserID = user.Id,
                BankAccountID = Guid.Parse("AA65CB93-F1E3-429E-A55C-4EAACB4E5B43"),
                CurrentBalance = 0,
                OpeningBalance = 0,
                //BankStatementImportDate = DateTime.Today.AddYears(99),
                StartDate = startDate,
                EndDate = endDate
            };
            _bankStatementService.Insert(newBankStatement);
            _unitofWork.SaveChanges();


            BankReconciliationHeaderItem newBankReconciliationHeaderItem = new BankReconciliationHeaderItem
            {
                BankStatementID = bankStatementID,
                BankStatementImportDate = "",
                BankStatementFileName = "",
                BankStatementReconciled = false,
                CurrentBalance = 0,
                OpeningBalance = 0,
                BankStatementStartDate = startDate.ToShortDateString(),
                BankStatementEndDate = endDate.ToShortDateString()
            };
            return Json(newBankReconciliationHeaderItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveBankStatementHeader(BankReconciliationHeaderItem bankStatement)
        {
            var existingBankStatement = GetValueByStatementHeaderID(bankStatement.BankStatementID);
          
            _bankStatementService.Delete(bankStatement.BankStatementID);
            _unitofWork.SaveChanges();
            return Json(existingBankStatement, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateBankStatementHeader(BankReconciliationHeaderItem bankStatement)
        {
            DateTime dateVal;

            var existingBankStatement = GetValueByStatementHeaderID(bankStatement.BankStatementID);
            existingBankStatement.BankStatementFileName = bankStatement.BankStatementFileName;
            existingBankStatement.BankStatementReconciled = bankStatement.BankStatementReconciled;
            existingBankStatement.UpdatedDate = DateTime.Now;
            existingBankStatement.CurrentBalance = bankStatement.CurrentBalance;
            existingBankStatement.OpeningBalance = bankStatement.OpeningBalance;
            
            if (DateTime.TryParse(bankStatement.BankStatementImportDate, out dateVal))
            {
                existingBankStatement.BankStatementImportDate = dateVal;
            }
            //if (DateTime.TryParseExact(bankStatement.BankStatementImportDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateVal)) ;
            if (DateTime.TryParse(bankStatement.BankStatementImportDate, out dateVal))
            {
                existingBankStatement.BankStatementImportDate = dateVal;
            }

            if (DateTime.TryParse(bankStatement.BankStatementEndDate, out dateVal))
            {
                existingBankStatement.EndDate = dateVal;
            }
            if (DateTime.TryParse(bankStatement.BankStatementStartDate, out dateVal))
            {
                existingBankStatement.StartDate = DateTime.Parse(bankStatement.BankStatementStartDate);
            }
            _bankStatementService.Update(existingBankStatement);
            _unitofWork.SaveChanges();

            if (bankStatement.BankStatementReconciled == true)
            {
                // move entries from tempNominal to BankStatementItemNominalLedger
                ProcessTempNominalWhenStatementReconciled(bankStatement);
            }


            return Json(bankStatement, JsonRequestBehavior.AllowGet);
        }

        private void ProcessTempNominalWhenStatementReconciled(BankReconciliationHeaderItem bankStatement)
        {
            var existingRecords = _bankStatementItemNominalLedgerEntryService.GetByStatementID(bankStatement.BankStatementID);
            if (existingRecords.Count == 0)
            {
                List<TempBankNominalLedgerEntry> tempNominalLedgerEntries = _tempBankNominalOrchestra.GetTempBankNominalLedgerEntries(bankStatement.BankStatementID);
                //List<TempBankStatementItemNominalLedgerEntry> tempBankStatementItemNominalLedgerEntries = _tempBankNominalOrchestra.GetTempBankStatementItemNominalLedgerEntries(bankStatement.BankStatementID);
                List<TempBankStatementItemNominalLedgerEntry> tempBankStatementItemNominalLedgerEntries = _tempBankNominalOrchestra.GetAllTempBankStatementItemNominalLedgerEntries();
                var recordsToInsert = MapTempBankStatementIteNominalLedgerEntry(ref tempBankStatementItemNominalLedgerEntries, ref bankStatement);
                _tempBankNominalOrchestra.SaveBankStatementItemNominalLedgerEntries(recordsToInsert);
                _tempBankNominalOrchestra.DeleteTempBankNominalLedgerRange(ref tempNominalLedgerEntries);
                _tempBankNominalOrchestra.DeleteTempBankStatementItemNominalLedgerRange(ref tempBankStatementItemNominalLedgerEntries);
            }
        }

        public void GetStatementTestData(ref ReconciliationViewModel reconciliationViewModel)
        {
            InsertTestReconciliation(ref reconciliationViewModel, "-302", "51047", "CHQ", "", "");
            InsertTestReconciliation(ref reconciliationViewModel, "-3323.5", "50973", "CHQ", "", "");
            InsertTestReconciliation(ref reconciliationViewModel, "-1050", "50969", "CHQ", "", "");
            InsertTestReconciliation(ref reconciliationViewModel, "-62.05", "50968", "CHQ", "", "");
            InsertTestReconciliation(ref reconciliationViewModel, "2728.38", "STREET KITCHEN LIM", "BAC", "STREET KITCHEN", "FP 20/09/16 2005");
            InsertTestReconciliation(ref reconciliationViewModel, "6234.32", "BUCKLEY & NUNEZ", "BAC", "PAVITTS PRODUCE", "FP 21/09/16 0241");
            InsertTestReconciliation(ref reconciliationViewModel, "3199.36", "FISHER OF NEWBURY", "BAC", "FISHER OF NEWBURY", "FP 21/09/16 0231");
            InsertTestReconciliation(ref reconciliationViewModel, "-3386", "CHATEAU DE NAGE", "STF", "EBANKGO69076348", "SAS CHATEAU DE N");
            InsertTestReconciliation(ref reconciliationViewModel, "-520", "DANNY BROOKER", "EBP", "D BROOKER LOAN", "FP 19/09/16 10");
            InsertTestReconciliation(ref reconciliationViewModel, "-1900", "UNPD CHQ 400001101", "", "PRENTIS", "");
            InsertTestReconciliation(ref reconciliationViewModel, "2218.36", "MR FRUITY WS LTD", "BAC", "MR.FRUITY  WHOLESA", "");
            InsertTestReconciliation(ref reconciliationViewModel, "10.2", "LONDON FRESH LTD", "BAC", "LONDON FRESH LTD", "");
            InsertTestReconciliation(ref reconciliationViewModel, "5806.08", "PIGOTT BUSINESS TP", "DPC", "PIGOTT", "");
            InsertTestReconciliation(ref reconciliationViewModel, "1900", "CHEQUE REPRESENTED", "CHQ", "REF 400001101", "");
            InsertTestReconciliation(ref reconciliationViewModel, "-29.95", "PAYPAL,*MATCHROOMSP", "POS", "35314369001 LU", "");
            InsertTestReconciliation(ref reconciliationViewModel, "-138.3", "BANKLINE", "BLN", "", "");
            InsertTestReconciliation(ref reconciliationViewModel, "1000", "FIJI FRUIT & VEGET", "BAC", "FIJI FRUIT/ ANWAR", "FP 16/09/16 0000");
            InsertTestReconciliation(ref reconciliationViewModel, "6893.9", "A C PRODUCE", "BAC", "AC PRODUCE IMPORTS", "FP 16/09/16 0800");
        }

        public void InsertTestReconciliation(ref ReconciliationViewModel vm, string amount, string description, string type, string nar1, string nar2)
        {
            StatementImportTestModel tim = new StatementImportTestModel
            {
                StatementId = Guid.NewGuid(),
                Amount = Decimal.Parse(amount),
                AccountNumber = "32149689",
                Date = DateTime.Now.ToShortDateString(),
                Description = description,
                Type = type,
                Narrative1 = nar1,
                Narrative2 = nar2,
                IsSelected = false
            };

            vm.StatementImportItems.Add(tim);
            InsertTestRecord(ref vm, ref tim);
        }

        public void InsertTestRecord(ref ReconciliationViewModel vm, ref StatementImportTestModel tm)
        {
            ReconciliationTestModel t1 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = tm.Amount,
                Balance = tm.Amount,
                Date = DateTime.Now.ToShortDateString(),
                Description = tm.Description,
                Type = tm.Type,
                IsSelected = false
            };
            vm.ReconciliationItems.Add(t1);
        }

        public void GetTestData(ref ReconciliationViewModel reconciliationViewModel)
        {
            // test data
            ReconciliationTestModel t1 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 100,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test1",
                Type = "abc",
                IsSelected = true
            };
            reconciliationViewModel.ReconciliationItems.Add(t1);

            ReconciliationTestModel t2 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 200,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test2",
                Test = "xxxxxxxxxxxx",
                Type = "abc",
                IsReconciled = true,
                IsSelected = false
            };

            reconciliationViewModel.ReconciliationItems.Add(t2);

            ReconciliationTestModel t3 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 300,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test3",
                Type = "abc",
                IsSelected = false
            };
            reconciliationViewModel.ReconciliationItems.Add(t3);
            ReconciliationTestModel t4 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 400,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test4",
                Type = "abc",
                Test = "xxxxx",
                IsSelected = true
            };
            ReconciliationTestModel t5 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 100,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test1a",
                Type = "abc",
                IsSelected = true
            };
            reconciliationViewModel.ReconciliationItems.Add(t5);

            ReconciliationTestModel t6 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 200,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test2a",
                Test = "xxxxxxxxxxxx",
                Type = "abc",
                IsReconciled = true,
                IsSelected = false
            };

            reconciliationViewModel.ReconciliationItems.Add(t6);

            ReconciliationTestModel t7 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 300,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test3a",
                Type = "abc",
                IsSelected = false
            };
            reconciliationViewModel.ReconciliationItems.Add(t7);
            ReconciliationTestModel t8 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 400,
                Balance = 123,
                Date = DateTime.Now.ToShortDateString(),
                Description = "Test4",
                Type = "abc",
                Test = "xxxxx",
                IsSelected = true
            };

            reconciliationViewModel.ReconciliationItems.Add(t8);

            ReconciliationTestModel t9 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = 6836.68M,
                Balance = 6836.68M,
                Date = DateTime.Now.ToShortDateString(),
                Description = "RUSHTONS THE CHEF",
                Type = "BAC",
                Test = "xxxxx",
                IsSelected = false
            };

            reconciliationViewModel.ReconciliationItems.Add(t9);

            ReconciliationTestModel t10 = new ReconciliationTestModel
            {
                RecId = Guid.NewGuid(),
                Amount = -1608M,
                Balance = -1608M,
                Date = DateTime.Now.ToShortDateString(),
                Description = "51049",
                Type = "CHQ",
                Test = "xxxxx",
                IsSelected = false
            };

            reconciliationViewModel.ReconciliationItems.Add(t10);
        }

        protected void SaveNominalEntriesToTempNominal(List<NominalLedgerEntry> nominalLedgerEntries, Guid bankStatementID)
        {
            var tempNominalLedgerEntries = MapLedgerEntriesToTempNominal(ref nominalLedgerEntries, bankStatementID);
            _tempBankNominalOrchestra.SaveTempBankNominalLedgerEntries(tempNominalLedgerEntries);
        }

        protected List<TempBankNominalLedgerEntry> GetTempBankNominalLedgerEntries(Guid bankStatementID)
        {
            var ledgerEntries = _tempBankNominalOrchestra.GetTempBankNominalLedgerEntries(bankStatementID);

            return ledgerEntries;
        }

        protected List<ReconciliationTestModel> GetNominalEntries(ref BankStatement bankStatementHeader)
        {
            DateTime startDate = DateTime.Parse("01/08/2016"); // need better filtering
            //DateTime endDate = DateTime.Parse("27/09/2016");
            DateTime endDate = DateTime.Now;
            List<NominalLedgerEntry> nominalLedgerEntries;
            List<ReconciliationTestModel> nominalViewModelEntries;

            var tempBankNominalLedgerEntries = GetTempBankNominalLedgerEntries(bankStatementHeader.BankStatementID);
            if (tempBankNominalLedgerEntries.Count > 0)
            {
                nominalViewModelEntries = MapTempListLedgerEntriesToViewModel(ref tempBankNominalLedgerEntries);
            }
            else
            {
                nominalLedgerEntries = _nominalOrchestra.GetNominalLedgerEntriesFilteredByDate(startDate, endDate);
                nominalViewModelEntries = MapLedgerEntriesToViewModel(ref nominalLedgerEntries);
                // Save Entries to DB
                SaveNominalEntriesToTempNominal(nominalLedgerEntries, bankStatementHeader.BankStatementID);
            }
            return nominalViewModelEntries;
        }

        public List<ReconciliationTestModel> MapLedgerEntriesToViewModel(ref List<NominalLedgerEntry> nominalLedgerEntries)
        {
            // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<NominalLedgerEntry, ReconciliationTestModel>()
                    .ForMember(dest => dest.RecId, opt => opt.MapFrom(src => src.NominalLedgerEntryID))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.NominalLedgerEntryAmount))
                    .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.NominalLedgerEntryAmount))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.NominalLedgerEntryDate.ToShortDateString()))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.NominalLedgerEntryDescription))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.NominalLedgerEntryReference))
                    .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var ledgerViewModel = new List<ReconciliationTestModel>(); // Instantiaze output Statements List

            mapper.Map(nominalLedgerEntries, ledgerViewModel); // Map CSV to Output List
            return ledgerViewModel;
        }

        public List<ReconciliationTestModel> MapTempListLedgerEntriesToViewModel(ref List<TempBankNominalLedgerEntry> tempNominalLedgerEntries)
        {
            // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<TempBankNominalLedgerEntry, ReconciliationTestModel>()
                    .ForMember(dest => dest.RecId, opt => opt.MapFrom(src => src.NominalLedgerEntryID))
                    .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.TransactionAmount))
                    .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.TransactionAmount))
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.BankReconciliationDate.ToShortDateString()))
                    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.TempDescriptionn))
                    .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TransactionType))
                    .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => src.IsReconciled))
                .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var ledgerViewModel = new List<ReconciliationTestModel>(); // Instantiaze output Statements List

            mapper.Map(tempNominalLedgerEntries, ledgerViewModel); // Map CSV to Output List
            foreach (var l in ledgerViewModel)
            {
                var matches = _tempBankNominalOrchestra.GetMatchingStatements(l.RecId);
                if (matches.Count > 0)
                {
                    l.ReconciledStatements = matches;
                }
            }

            return ledgerViewModel;
        }

        public List<TempBankNominalLedgerEntry> MapLedgerEntriesToTempNominal(ref List<NominalLedgerEntry> nominalLedgerEntries, Guid bankStatementID)
        {
            // Auto Mapper
            var config =
                new MapperConfiguration(cfg => cfg.CreateMap<NominalLedgerEntry, TempBankNominalLedgerEntry>()
                    .ForMember(dest => dest.TempBankNominalLedgerEntryID, opt => opt.MapFrom(src => src.NominalLedgerEntryID)) // same as Nominal Ledger ID
                    .ForMember(dest => dest.BankStatementID, opt => opt.MapFrom(src => bankStatementID))
                    .ForMember(dest => dest.NominalLedgerEntryID, opt => opt.MapFrom(src => src.NominalLedgerEntryID))
                    .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.NominalLedgerEntryAmount))
                    .ForMember(dest => dest.BankReconciliationDate, opt => opt.MapFrom(src => src.NominalLedgerEntryDate))
                    .ForMember(dest => dest.TempDescriptionn, opt => opt.MapFrom(src => src.NominalLedgerEntryDescription))
                    .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.NominalLedgerEntryReference))
                    .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => false))
                   );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var ledgerEntries = new List<TempBankNominalLedgerEntry>(); // Instantiaze output Statements List

            mapper.Map(nominalLedgerEntries, ledgerEntries); // Map CSV to Output List
            return ledgerEntries;
        }

        public List<BankStatementItem> InsertBankStatementItems(ref List<StatementImportTestModel> statementImportTestModels, ref BankStatement bankStatementHeader)
        {
            var user = User.Identity.GetApplicationUser();
            var bankStatementID = bankStatementHeader.BankStatementID;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<StatementImportTestModel, BankStatementItem>()
               .ForMember(dest => dest.Text4, opt => opt.MapFrom(src => src.AccountNumber))
               .ForMember(dest => dest.TransactionAmount, opt => opt.MapFrom(src => src.Amount)) // map Debit or Credit to Amount
               .ForMember(dest => dest.BankStatementDate, opt => opt.MapFrom(src => src.Date))
               .ForMember(dest => dest.Text1, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.Text2, opt => opt.MapFrom(src => src.Narrative1))
               .ForMember(dest => dest.Text3, opt => opt.MapFrom(src => src.Narrative2))
               .ForMember(dest => dest.BankStatementItemID, opt => opt.MapFrom(src => src.StatementId))
               .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => false))
               .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Type))
               .ForMember(dest => dest.CreatedByUserID, opt => opt.MapFrom(src => user.Id))
               .ForMember(dest => dest.UpdatedByUserID, opt => opt.MapFrom(src => user.Id))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.BankStatementID, opt => opt.MapFrom(src => bankStatementID))
               );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var statementItems = new List<BankStatementItem>(); // Instantiaze output Statements List

            mapper.Map(statementImportTestModels, statementItems); // Map CSV to Output List
            return statementItems;
        }

        public List<BankStatementItemNominalLedgerEntry> MapTempBankStatementIteNominalLedgerEntry(ref List<TempBankStatementItemNominalLedgerEntry> statementImportModels, ref BankReconciliationHeaderItem bankStatementHeader)
        {
            var user = User.Identity.GetApplicationUser();
            var bankStatementID = bankStatementHeader.BankStatementID;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TempBankStatementItemNominalLedgerEntry, BankStatementItemNominalLedgerEntry>()

               .ForMember(dest => dest.CreatedByUserID, opt => opt.MapFrom(src => user.Id))
               .ForMember(dest => dest.UpdatedByUserID, opt => opt.MapFrom(src => user.Id))
               .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.BankStatementItemID, opt => opt.MapFrom(src => src.BankStatementItemID))
               .ForMember(dest => dest.BankStatementItemNominalLedgerEntryID, opt => opt.MapFrom(src => src.BankStatementItemNominalLedgerEntryID))
               .ForMember(dest => dest.NominalLedgerEntryID, opt => opt.MapFrom(src => src.NominalLedgerEntryID))
               );
            var mapper = config.CreateMapper(); // Instantiaze AutoMapper
            var statementItems = new List<BankStatementItemNominalLedgerEntry>(); // Instantiaze output Statements List

            mapper.Map(statementImportModels, statementItems); // Map CSV to Output List
            return statementItems;
        }

        public List<StatementImportTestModel> GetCSVImport(ref BankStatement bankStatementHeader)
        {

            // Get Existing or new Bank Statement Item Records
            var bankStatementID = bankStatementHeader.BankStatementID;
            var bankStatementItems = _bankStatementOrchestra.GetBankStatementItems(bankStatementID);
            var genericStatements = new List<StatementImportTestModel>(); // Instantiaze output Statements List
            var accountNumber = "32149689"; // temporarily need to get from Header
            var uploadFilePath = "/Uploads/" + bankStatementHeader.BankStatementFileName;

            if (bankStatementItems.Count == 0)
            {
                StreamReader sr = new StreamReader(Server.MapPath(uploadFilePath));
                //  StreamReader sr = new StreamReader(Server.MapPath(@"/UploadFolder/natwest.csv"));

                //Csv reader reads the stream
                CsvReader csvreadnatwest = new CsvReader(sr);
                NatWestClassMap natwestmap = new NatWestClassMap(); // mapping to Columns
                csvreadnatwest.Configuration.RegisterClassMap(natwestmap); // register mappings

                //CsvReader csvreadnatwest2 = new CsvReader(sr2);
                //NatWestClassMap natwestmap2 = new NatWestClassMap(); // mapping to Columns
                //csvreadnatwest2.Configuration.RegisterClassMap(natwestmap2); // register mappings

                //csvread will fetch all record in one go to the IEnumerable object 
                var importedRecords = csvreadnatwest.GetRecords<StatementImportNatWestModel>();
                // var importedRecords2 = csvreadnatwest2.GetRecords<StatementImportNatWestModel>();
                // do the import of the CSV

                // Auto Mapper
                var config =
                    new MapperConfiguration(
                        cfg => cfg.CreateMap<StatementImportNatWestModel, StatementImportTestModel>()
                            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
                            .ForMember(dest => dest.Amount,
                                opt => opt.MapFrom(src => src.Credit > 0 ? src.Credit : src.Debit))
                            // map Debit or Credit to Amount
                            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
                            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                            .ForMember(dest => dest.Narrative1, opt => opt.MapFrom(src => src.Narrative1))
                            .ForMember(dest => dest.Narrative2, opt => opt.MapFrom(src => src.Narrative2))
                            .ForMember(dest => dest.StatementId, opt => opt.MapFrom(src => Guid.NewGuid()))
                            .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => false))
                            .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false))
                            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                        );
                var mapper = config.CreateMapper(); // Instantiaze AutoMapper

                //var genericStatements = new List<StatementImportTestModel>(); // Instantiaze output Statements List

                mapper.Map(importedRecords, genericStatements); // Map CSV to Output List

                // Get Statements to save to Database
                var statementItems = InsertBankStatementItems(ref genericStatements, ref bankStatementHeader);
                _bankStatementItemService.InsertGraphRange(statementItems);
                _unitofWork.SaveChanges();
            }
            else // map StatementDB Items to Viewmodel
            {
                var config =
                    new MapperConfiguration(
                        cfg => cfg.CreateMap<BankStatementItem, StatementImportTestModel>()
                            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => accountNumber)) // Need to get
                            .ForMember(dest => dest.Amount,
                                opt => opt.MapFrom(src => src.TransactionAmount))
                            // map Debit or Credit to Amount
                            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.BankStatementDate.ToShortDateString()))
                            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Text1))
                            .ForMember(dest => dest.Narrative1, opt => opt.MapFrom(src => src.Text2))
                            .ForMember(dest => dest.Narrative2, opt => opt.MapFrom(src => src.Text3))
                            .ForMember(dest => dest.StatementId, opt => opt.MapFrom(src => src.BankStatementItemID))
                            .ForMember(dest => dest.IsReconciled, opt => opt.MapFrom(src => src.IsReconciled))
                            .ForMember(dest => dest.IsSelected, opt => opt.MapFrom(src => false)) // add to temp table
                            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TransactionType))
                        );
                var mapper = config.CreateMapper(); // Instantiaze AutoMapper

                genericStatements = new List<StatementImportTestModel>(); // Instantiaze output Statements List

                mapper.Map(bankStatementItems, genericStatements);
            }

            return genericStatements;
        }
    }
}