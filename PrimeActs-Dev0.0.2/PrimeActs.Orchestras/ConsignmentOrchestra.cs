#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Infrastructure.BaseEntities;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;
using PrimeActs.Domain.ViewModels.Produce;

#endregion

namespace PrimeActs.Orchestras
//Adding comment
{
    public interface IConsignmentOrchestra
    {
        void Initialize(ApplicationUser principal);
        bool Validate(ConsignmentEditModel model);
        bool Validate(ConsignmentItemEditModel model);
        bool InsertFile(File postedFile);
        bool InsertConsignmentFile(ConsignmentFile file);
        ConsignmentViewModel GetConsignmentViewModel(Guid id);
        ConsignmentDetailsViewModel GetConsignmentDetailsViewModel(Guid id);
        ConsignmentItemViewModel GetConsignmentItemViewModel(Guid id);
        ConsignmentPagingModel GetConsignmentPagingModel(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<ConsignmentEditModel> GetConsignments(QueryOptions queryOptions, SearchObject searchObject);
        OrderPagingModel GetOrderPagingModel(QueryOptions queryOptions, SearchObject searchObject);
        OrderViewModel GetOrderViewModel(Guid id);
        // List<ConsignmentEditModel> GetConsignmentUserNames(QueryOptions queryOptions);
        ConsignmentEditModel CreateConsignment(ConsignmentEditModel model);
        ConsignmentEditModel UpdateConsignment(ConsignmentEditModel model);
        ConsignmentItemEditModel CreateConsignmentItem(ConsignmentItemEditModel model);
        ConsignmentItemEditModel UpdateConsignmentItem(ConsignmentItemEditModel model);
        ConsignmentEditModel GetModel(Guid id);
        ConsignmentEditModel GetLastConsignmentByUser(Guid userID);
        ConsignmentItemEditModel UpdateConsignmentItemForEdit(ConsignmentItemEditModel model, bool allowedToModifyCost);
        void UpdateConsignmentDates(ConsignmentEditModel consignmentEditModel);
        PackWtUnitAndPorterageList GetPackWtUnitAndPorterageList();
        CompletedConsignmentPagingModel GetConsignmentPagingModelSimplified(QueryOptions queryOptions, SearchObject searchObject);
        ResultList<CompletedConsignment> GetConsignmentsSimplified(QueryOptions queryOptions, SearchObject searchObject);
        ConsignmentItemBasicModel GetConsignmentItemBasic(Guid id);
        ConsignmentEditModel UpdateConsignmentHeader(ConsignmentEditModel model);
        ConsignmentItemReturns EditConsignmentItemPriceReturns(ConsignmentItemReturns consignmentItemPriceReturnModels, string getUserId);
    }

    public class ConsignmentOrchestra : IConsignmentOrchestra
    {
        private readonly IConsignmentFileService _consignmentfileService;
        private readonly IConsignmentItemService _consignmentItemService;
        private readonly IConsignmentItemPriceReturnService _consignmentItemPriceReturnService;
        private readonly IConsignmentItemArrivalService _consignmentItemArrivalService;
        private readonly IConsignmentService _consignmentService;
        private readonly IFileService _fileService;
        private readonly INoteService _noteService;
        private readonly IPackWtUnitService _packWtUnitService;
        private readonly IPorterageService _porterageService;
        private readonly IPurchaseTypeService _purchaseTypeService;
        private readonly ISupplierService _supplierService;
        private readonly ICountryService _countryService;
        private readonly IDepartmentService _departmentService;
        private readonly IDivisionService _divisionService;
        private IDespatchService _despatchService;
        private IPortService _portService;
        private ApplicationUser _principal;
        private readonly IProduceService _produceService;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly ISetupLocalService _setupLocalService;
        private readonly ISetupGlobalService _setupGlobalService;
        private readonly string _serverCode;
        private const string DEFAULT_COUNTRY = "UK";

        // Need a check table setup service
        public ConsignmentOrchestra(ISetupLocalService setupLocalService, ISetupGlobalService setupGlobalService,
            IConsignmentFileService consignmentfileService, INoteService noteService,
            IConsignmentService consignmentService, IConsignmentItemService consignmentItemService, ISupplierService supplierService, 
            IDivisionService divisionService, IDepartmentService departmentService, IPortService portService, IPurchaseTypeService purchaseTypeService,
            IProduceService produceService, ICountryService countryService, IPorterageService porterageService,
            IPackWtUnitService packWtUnitService, IDespatchService despatchService, IFileService fileService, IConsignmentItemArrivalService consignmentArrivalService,
            IApplicationUserOrchestra applicationUserOrchestra, IConsignmentItemPriceReturnService consignmentItemPriceReturnService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "|";

            _consignmentItemService = consignmentItemService;
            _consignmentService = consignmentService;
            _setupLocalService = setupLocalService;
            _setupGlobalService = setupGlobalService;
            _supplierService = supplierService;
            _noteService = noteService;
            _departmentService = departmentService;
            _divisionService = divisionService;
            _portService = portService;
            _purchaseTypeService = purchaseTypeService;
            _produceService = produceService;
            _countryService = countryService;
            _porterageService = porterageService;
            _packWtUnitService = packWtUnitService;
            _despatchService = despatchService;
            _fileService = fileService;
            _consignmentfileService = consignmentfileService;
            _consignmentItemArrivalService = consignmentArrivalService;
            _applicationUserOrchestra = applicationUserOrchestra;
            _consignmentItemPriceReturnService = consignmentItemPriceReturnService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public bool Validate(ConsignmentEditModel model)
        {
            //var validator = new ConsignmentEditModelValidator();
            //var result = validator.Validate(model);
            //if (result.IsValid) return result.IsValid;
            //foreach (var failer in result.Errors)
            //    _validationDictionary.AddError(failer.PropertyName, failer.ErrorMessage);
            //return result.IsValid;
            return true;
        }

        public bool Validate(ConsignmentItemEditModel model)
        {
            return true;
        }

        public ConsignmentDetailsViewModel GetConsignmentDetailsViewModel(Guid id)
        {
            var consignmentViewModel = new ConsignmentDetailsViewModel
            {

                ConsignmentEditModel =
                    id == Guid.Empty
                        ? new ConsignmentEditModel
                        {
                            ConsignmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                            DisplayDespatchLocation =
                                (bool)_setupLocalService.GetDisplayOption("DisplayDespatchLocation").SetupValueBit,
                            DisplayPort = (bool)_setupLocalService.GetDisplayOption("DisplayPort").SetupValueBit,
                            purchaseTypeList = _purchaseTypeService.GetAllPurchaseTypes(),
                            ConsignmentReference = "",
                            ContractDate = DateTime.Today.ToShortDateString(),
                            ConsignmentItemEditModels =
                                new List<ConsignmentItemEditModel> { new ConsignmentItemEditModel() },
                            ConsignmentFileEditModels =
                                new List<ConsignmentFileEditModel> { new ConsignmentFileEditModel() },
                            FileEditModels = new List<FileEditModel> { new FileEditModel() }
                        }
                        : BuildConsignmentEditModel(_consignmentService.ConsignmentById(id))
            };
            return consignmentViewModel;
        }


        public ConsignmentViewModel GetConsignmentViewModel(Guid id)
        {
            ConsignmentViewModel consignmentViewModel;

            //Country testgetdcountry = new Country();
            // Wrap Service Calls & Object Creations
            //=======================================
            try
            {
                var country_by_name = _countryService.CountryByName("United Kingdom");
                //var department_for_user = _departmentService.DepartmentById(_principal.DepartmentId.GetValueOrDefault());
                var division_for_user = _divisionService.DivisionById(_principal.DivisionId.GetValueOrDefault());
                var svc_consignment_id = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                var svc_display_despatch_location = (bool)_setupLocalService.GetDisplayOption("DisplayDespatchLocation").SetupValueBit;
                var svc_purchase_type_list = _purchaseTypeService.GetAllPurchaseTypes();
                var svc_display_port = (bool)_setupLocalService.GetDisplayOption("DisplayPort").SetupValueBit;
                //var obj_consignment_item_edit_model = new List<ConsignmentItemEditModel> { new ConsignmentItemEditModel() };
                var obj_consignment_item_edit_model = new List<ConsignmentItemEditModel>();
                //var obj_consignment_file_edit_model = new List<ConsignmentFileEditModel> { new ConsignmentFileEditModel() };
                var obj_consignment_file_edit_model = new List<ConsignmentFileEditModel>();
                var obj_file_edit_model = new List<FileEditModel> { new FileEditModel() };
                var svc_multiple_consignment_items = (bool)_setupLocalService.GetDisplayOption("MultipleConsignmentItems").SetupValueBit;
                //var svc_default_department_id = _principal.DepartmentId.GetValueOrDefault();
                var svc_default_division_id = _principal.DivisionId.GetValueOrDefault();

                // Throw Errors if: conditions
                //============================
                //consignmentViewModel = new ConsignmentViewModel
                //{                               
                //    ConsignmentEditModel =
                //        id == Guid.Empty
                //            ? new ConsignmentEditModel
                //            {
                //                ConsignmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                //                DisplayDespatchLocation =
                //                    (bool) _setupLocalService.GetDisplayOption("DisplayDespatchLocation").SetupValueBit,
                //                DisplayPort = (bool) _setupLocalService.GetDisplayOption("DisplayPort").SetupValueBit,
                //                purchaseTypeList = _purchaseTypeService.GetAllPurchaseTypes(),                            
                //                ConsignmentReference = RandomString(),
                //                CountryName = country_by_name.CountryName,
                //                CountryID = country_by_name.CountryID,
                //                ContractDate = DateTime.Today.ToShortDateString(),
                //                ConsignmentItemEditModels =
                //                    new List<ConsignmentItemEditModel> {new ConsignmentItemEditModel()},
                //                ConsignmentFileEditModels =
                //                    new List<ConsignmentFileEditModel> {new ConsignmentFileEditModel()},
                //                FileEditModels = new List<FileEditModel> {new FileEditModel()},
                //                MultipleConsignmentItems = (bool)_setupLocalService.GetDisplayOption("MultipleConsignmentItems").SetupValueBit,
                //               // CountryID = _countryService.CountryByName("United Kingdon").CountryID
                //                 DefaultDepartmentID = _principal.DepartmentId.GetValueOrDefault(),
                //                 DefaultDepartmentName = department_for_user.DepartmentName
                //            }
                //            : BuildConsignmentEditModel(_consignmentService.ConsignmentById(id))
                //};
                //}

                if (id == Guid.Empty)
                {
                    consignmentViewModel = new ConsignmentViewModel
                    {
                        ConsignmentEditModel =
                                new ConsignmentEditModel
                                {
                                    ConsignmentID = svc_consignment_id,
                                    DisplayDespatchLocation = svc_display_despatch_location,
                                    DisplayPort = svc_display_port,
                                    purchaseTypeList = svc_purchase_type_list,
                                    ConsignmentReference = "",
                                    CountryName = country_by_name.CountryName,
                                    CountryID = country_by_name.CountryID,
                                    ContractDate = DateTime.Today.ToShortDateString(),
                                    ConsignmentItemEditModels = obj_consignment_item_edit_model,
                                    ConsignmentFileEditModels = obj_consignment_file_edit_model,
                                    FileEditModels = obj_file_edit_model,
                                    MultipleConsignmentItems = svc_multiple_consignment_items,
                                    DefaultDivisionID = svc_default_division_id,
                                    DefaultDivisionName = division_for_user.DivisionName,
                                    //DefaultDepartmentID = svc_default_department_id,
                                    //DefaultDepartmentName = department_for_user.DepartmentName,
                                    //DepartmentCode = department_for_user.DepartmentCode,
                                    DespatchDate = DateTime.Today.AddDays(-1).ToString()
                                }
                    };
                }
                else
                {
                    consignmentViewModel = new ConsignmentViewModel();
                    consignmentViewModel.ConsignmentEditModel = BuildConsignmentEditModel(_consignmentService.ConsignmentById(id));
                }
            }


            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method: GetConsignmentViewModel: for Id {0} ", id);
                throw new ApplicationException(err_detail, ex);
            }

            return consignmentViewModel;
        }

        //Order methods
        public OrderViewModel GetOrderViewModel(Guid id)
        {
            var orderViewModel = new OrderViewModel
            {
                OrderEditModel =
                    id == Guid.Empty
                        ? new ConsignmentEditModel
                        {
                            ConsignmentID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                            DisplayDespatchLocation =
                                (bool)_setupLocalService.GetDisplayOption("DisplayDespatchLocation").SetupValueBit,
                            DisplayPort = (bool)_setupLocalService.GetDisplayOption("DisplayPort").SetupValueBit,
                            purchaseTypeList = _purchaseTypeService.GetAllPurchaseTypes(),
                            ConsignmentReference = "",
                            ContractDate = DateTime.Today.ToShortDateString(),
                            ConsignmentItemEditModels =
                                new List<ConsignmentItemEditModel> { new ConsignmentItemEditModel() },
                            ConsignmentFileEditModels =
                                new List<ConsignmentFileEditModel> { new ConsignmentFileEditModel() },
                            FileEditModels = new List<FileEditModel> { new FileEditModel() }
                        }
                        : BuildConsignmentEditModel(_consignmentService.ConsignmentById(id))
            };
            return orderViewModel;
        }

        public OrderPagingModel GetOrderPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var orderPagingModel = new OrderPagingModel();
            var ordersList = _consignmentService.GetConsignments(queryOptions, searchObject, out totalCount);
            var result = new ResultList<ConsignmentEditModel>(ordersList.Select(BuildConsignmentEditModel).ToList(), queryOptions);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);

            orderPagingModel.Orders = result;
            orderPagingModel.SearchObject = new SearchObject
            {
                ConsignmentReference = searchObject.ConsignmentReference,
                SupplierCode = searchObject.SupplierCode,
                SupplierName = searchObject.SupplierName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null,
                RecordsInDays = searchObject.RecordsInDays
            };
            return orderPagingModel;
        }

        public PackWtUnitAndPorterageList GetPackWtUnitAndPorterageList()
        {
            var porterages = _porterageService.GetAllPorterages();
            var packWithUnits = _packWtUnitService.GetAllPackWtUnits();

            return new PackWtUnitAndPorterageList() { PackWtUnit = packWithUnits, Porterage = porterages };
        }

        public ConsignmentItemViewModel GetConsignmentItemViewModel(Guid id)
        {
            string err_for_id = GetErrForId(id);

            ConsignmentItemViewModel consignmentItemViewModel;
            try
            {
                var svc_porterage = _porterageService.GetAllPorterages();
                var svc_pack_with_unit = _packWtUnitService.GetAllPackWtUnits();
                var svc_consignment_item_id = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                var svc_department_id = _principal.DepartmentId.GetValueOrDefault();
                var svc_consignment = BuildConsignmentEditModel(_consignmentService.ConsignmentById(id));

                consignmentItemViewModel = new ConsignmentItemViewModel
                {
                    Porterage = svc_porterage,
                    PackWtUnit = svc_pack_with_unit,
                    ConsignmentItemEditModel =
                        new ConsignmentItemEditModel
                        {
                            ConsignmentID = id,
                            ConsignmentItemID = svc_consignment_item_id,
                            Consignment = svc_consignment,
                            DepartmentID = svc_department_id // DC 0905 default
                        },

                    ConsignmentItemEditModels =
                                     new List<ConsignmentItemEditModel> { new ConsignmentItemEditModel() },
                };
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:GetConsignmentItemViewModel: ApplyChanges: Note Service Insert for Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }

            return consignmentItemViewModel;
        }

        private static string GetErrForId(Guid id)
        {
            string err_for_id = "No Consignment ID";
            if (id != Guid.Empty) err_for_id = id.ToString();
            return err_for_id;
        }

        public CompletedConsignmentPagingModel GetConsignmentPagingModelSimplified(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var consignmentPagingModel = new CompletedConsignmentPagingModel();
            var consignments = _consignmentService.GetConsignments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //This line gets rid of items!! Fix the error
            var result = new ResultList<CompletedConsignment>(consignments.Select(BuildConsignmentEditModelSimplified).ToList(),
                queryOptions);
            consignmentPagingModel.ConsignmentEditModels = result;
            consignmentPagingModel.SearchObject = new SearchObject
            {
                ConsignmentReference = searchObject.ConsignmentReference,
                SupplierCode = searchObject.SupplierCode,
                SupplierName = searchObject.SupplierName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                FromDateStr = searchObject.FromDate.HasValue ? searchObject.FromDate.ToString() : "",
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null
            };
            return consignmentPagingModel;
        }

        public ResultList<CompletedConsignment> GetConsignmentsSimplified(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var consignments = _consignmentService.GetConsignments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<CompletedConsignment>(
                    consignments != null ? consignments.Select(BuildConsignmentEditModelSimplified).ToList() : null, queryOptions);
        }

        public ConsignmentItemBasicModel GetConsignmentItemBasic(Guid id)
        {
            ConsignmentItem item = _consignmentItemService.ConsignmentItemByIDSimple(id);
            return new ConsignmentItemBasicModel() { ConsignmentItemID = item.ConsignmentItemID, EstimatedPurchaseCost = item.EstimatedPurchaseCost, QuantityExpected = item.QuantityExpected, PurchaseTypeID = item.Consignment.PurchaseTypeID };
        }

        public ConsignmentPagingModel GetConsignmentPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var consignmentPagingModel = new ConsignmentPagingModel();
            var consignments = _consignmentService.GetConsignments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //This line gets rid of items!! Fix the error
            var result = new ResultList<ConsignmentEditModel>(consignments.Select(BuildConsignmentEditModel).ToList(),
                queryOptions);
            consignmentPagingModel.ConsignmentEditModels = result;
            consignmentPagingModel.SearchObject = new SearchObject
            {
                ConsignmentReference = searchObject.ConsignmentReference,
                SupplierCode = searchObject.SupplierCode,
                SupplierName = searchObject.SupplierName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                FromDateStr = searchObject.FromDate.HasValue ? searchObject.FromDate.ToString() : "",
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null
            };
            
            return consignmentPagingModel;
        }

        public ConsignmentEditModel GetModel(Guid id)
        {
            return BuildConsignmentEditModel((_consignmentService.ConsignmentById(id)));
        }

        public ConsignmentEditModel GetLastConsignmentByUser(Guid userID)
        {
            var entity = _consignmentService.LastConsigmentByUserId(userID);
            if (entity != null)
                return new ConsignmentEditModel() { SupplierDepartmentID = entity.SupplierDepartmentID, SupplierDepartmentName = entity.SupplierDepartment.Supplier.SupplierCode + " - " + entity.SupplierDepartment.Supplier.SupplierCompanyName + " - " + entity.SupplierDepartment.SupplierDepartmentName };
            else
            {
                return new ConsignmentEditModel() { SupplierDepartmentID = Guid.Empty, SupplierDepartmentName = "" };
            }
        }

        public ConsignmentItemEditModel UpdateConsignmentItemForEdit(ConsignmentItemEditModel model, bool allowedToModifyCost)
        {
            var consignmentItem = ApplyChangesForUpdate(model, allowedToModifyCost);
            consignmentItem.ObjectState = ObjectState.Modified;
            _consignmentItemService.Update(consignmentItem);
            return model;
        }

        public void UpdateConsignmentDates(ConsignmentEditModel consignmentEditModel)
        {
            var consignment = _consignmentService.GetConsignment(consignmentEditModel.ConsignmentID);
            consignment.ObjectState = ObjectState.Modified;
            consignment.UpdatedBy = _principal.Id;
            consignment.UpdatedDate = DateTime.Now;
            consignment.ServerCode = "L";
            _consignmentService.Update(consignment);
        }

        public ResultList<ConsignmentEditModel> GetConsignments(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var consignments = _consignmentService.GetConsignments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<ConsignmentEditModel>(
                    consignments != null ? consignments.Select(BuildConsignmentEditModel).ToList() : null, queryOptions);
        }

        public ConsignmentEditModel CreateConsignment(ConsignmentEditModel model)
        {
            string err_for_id = GetIdForModel(model);
            Consignment consignment;

            try
            {
                model.NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                model.ConsignmentReference = GetNewConsignmentReference(model.DivisionID);
                model.PurchaseTypeID = Guid.Parse(model.SelectPurchaseType);
                // Need to bring the below GUID from the orchestra
                model.SupplierDepartmentID = model.SupplierDepartmentID;
                //var consignment = ApplyChanges(model);
                consignment = ApplyChanges(model);
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:CreateConsignment:Execute ApplyChanges:For Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }

            try
            {
                model.ConsignmentID = consignment.ConsignmentID;
                consignment.DivisionID = _principal.DivisionId;
                consignment.ObjectState = ObjectState.Added;
                consignment.CreatedBy = _principal.Id;
                consignment.CreatedDate = DateTime.Now;

                consignment.ServerCode = "L";

                //Note 
                var note = new Note();
                note.NoteID = model.NoteID.Value != null ? model.NoteID.Value : Guid.Empty;
                note.NoteText = model.NoteText != null ? model.NoteText : string.Empty;
                note.NoteDescription = model.NoteText != null ? model.NoteText : string.Empty;
                _noteService.Insert(note);
                consignment.Note = note;
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:CreateConsignment:Execute Note Service:For Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }
            // End of Note
            // File
            try
            {
                for (var i = 0; i < consignment.ConsignmentFiles.Count; i++)
                {
                    // DC PLACEHOLDER - Only do if FileName != null ; until logic fully understood otherwise error on Order
                    if (model.FileEditModels[i].FileName != null)
                    {
                        _consignmentfileService.Insert(consignment.ConsignmentFiles.ElementAt(i));
                        var file = new File
                        {
                            FileID = consignment.ConsignmentFiles.ElementAt(i).FileID.Value,
                            FileName = model.FileEditModels[i].FileName,
                            FileContent = model.FileEditModels[i].FileContent,
                            ObjectState = ObjectState.Added,

                        };
                        _fileService.Insert(file);
                    }
                }
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:CreateConsignment:Execute File Service Insert:For Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }

            //End of File
            consignment.IsHistory = false;

            try
            {
                _consignmentService.Insert(consignment);
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:CreateConsignment:Execute _ConsignmentServiceInsert:For Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }
            return model;

        }

        private static string GetIdForModel(ConsignmentEditModel model)
        {
            string err_for_id = "No Consignment ID";
            if (model.ConsignmentID != Guid.Empty) err_for_id = model.ConsignmentID.ToString();
            return err_for_id;
        }
        private static string GetIdForModel(ConsignmentItemEditModel model) // overload for Item
        {
            string err_for_id = "No Consignment ID";
            if (model.ConsignmentID != Guid.Empty) err_for_id = model.ConsignmentID.ToString();
            return err_for_id;
        }

        //public void StoreConsignmentFile(Guid consID, Guid fileID)
        //{

        //    PrimeActs.Domain.ConsignmentFile f = new PrimeActs.Domain.ConsignmentFile();
        //    f.ConsignmentFileID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
        //    f.FileID = fileID;
        //    f.ConsignmentID = consID;
        //    InsertConsignmentFile(f);

        //}

        public ConsignmentEditModel UpdateConsignmentHeader(ConsignmentEditModel model)
        {
            var fileID = Guid.Empty;
            if (!string.IsNullOrEmpty(model.FileID))
            {
                //  var file = _fileService.FileByName(model.FileName);
                // fileID = file.FileID != null ? file.FileID : Guid.Empty;
            }
            var consignment = ApplyHeaderChanges(model);
            consignment.ObjectState = ObjectState.Modified;
            consignment.UpdatedBy = _principal.Id;
            consignment.UpdatedDate = DateTime.Now;
            consignment.ServerCode = "L";
            _consignmentService.Update(consignment);
            return model;
        }

        public ConsignmentItemReturns EditConsignmentItemPriceReturns(ConsignmentItemReturns returns, string userId)
        {
            foreach (var item in returns.ConsignmentItemPriceReturnModels)
            {
                if (item.ConsignmentItemPriceReturnID != Guid.Empty)
                {
                    // update existing one
                    var obj = _consignmentItemPriceReturnService.ConsignmentItemPriceReturnByID(
                        item.ConsignmentItemPriceReturnID);
                    obj.ReturnQuantity = item.ReturnQuantity;
                    obj.ReturnUnitPrice = item.ReturnUnitPrice;
                    obj.UpdatedDate = DateTime.Now;
                    obj.UpdatedByUserID = Guid.Parse(userId);
                    obj.ObjectState= ObjectState.Modified;
                    _consignmentItemPriceReturnService.Update(obj);
                }
                else
                {
                    // create new one
                    var obj = new ConsignmentItemPriceReturn()
                    {
                        ConsignmentItemPriceReturnID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                        ConsignmentItemID = returns.ConsignmentItemID,
                        ReturnUnitPrice = item.ReturnUnitPrice,
                        ReturnQuantity = item.ReturnQuantity,
                        ReturnDate = DateTime.Parse(returns.ConsignmentCreatedDate).AddDays(1),
                        CreatedDate = DateTime.Now,
                        CreatedByUserID = Guid.Parse(userId),
                        ObjectState = ObjectState.Added
                    };
                    _consignmentItemPriceReturnService.Insert(obj);

                    item.ConsignmentItemPriceReturnID = obj.ConsignmentItemPriceReturnID;
                }
            }

            foreach (var deleted in returns.ConsignmentItemPriceReturnModelsDeleted)
            {
                //todo   
            }

            return returns;
        }

        // The below model needs altering due to new file service requirements...!
        public ConsignmentEditModel UpdateConsignment(ConsignmentEditModel model)
        {
            var fileID = Guid.Empty;
            if (!string.IsNullOrEmpty(model.FileID))
            {
                //  var file = _fileService.FileByName(model.FileName);
                // fileID = file.FileID != null ? file.FileID : Guid.Empty;
            }
            var consignment = ApplyChanges(model);
            consignment.ObjectState = ObjectState.Modified;
            consignment.UpdatedBy = _principal.Id;
            consignment.UpdatedDate = DateTime.Now;
            consignment.ServerCode = "L";
            _consignmentService.Update(consignment);
            return model;
        }

        public ConsignmentItemEditModel CreateConsignmentItem(ConsignmentItemEditModel model)
        {
            string err_for_id = GetIdForModel(model);
            try
            {
                var consignmentItem = ApplyChanges(model);
                consignmentItem.ObjectState = ObjectState.Added;
                consignmentItem.CreatedBy = _principal.Id;
                consignmentItem.CreatedDate = DateTime.Now;
                //consignmentItem.UpdatedBy = _principal.Id;
                //AK removed is active from table
                // consignmentItem.IsActive = true;
                consignmentItem.EstimatedPurchaseCost = model.EstimatedPurchaseCostPerPack;
                consignmentItem.Country = _countryService.CountryById(model.OriginCountryID);
                //consignmentItem.NoteID = model.NoteID;
                //var cons = _consignmentService.ConsignmentById(model.ConsignmentID);
                ////cons.IsSaved = true;
                //var consignment = ApplyChanges(BuildConsignmentEditModel(cons));
                ////consignment.ObjectState = ObjectState.Added;
                ////consignment.ObjectState = ObjectState.Modified;
                //consignment.UpdatedBy = _principal.Id;
                //consignment.UpdatedDate = DateTime.Now;
                //consignment.ServerCode = "L";
                //_consignmentService.Update(consignment);
                consignmentItem.NoteID = consignmentItem.Note.NoteID;

                _consignmentItemService.Insert(consignmentItem);
                if (consignmentItem.ConsignmentItemArrivals.Count > 0)
                {

                    consignmentItem.ConsignmentItemArrivals.ToList().ForEach(x => _consignmentItemArrivalService.Insert(x));
                    //onsignmentItemArrivalService.Insert(consignmentItem.ConsignmentItemArrivals[0]);
                }
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:CreateConsignmentItem:Execute _ConsignmentServiceInsert:For Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }
            return model;
        }

        public ConsignmentItemEditModel UpdateConsignmentItem(ConsignmentItemEditModel model)
        {
            var consignmentItem = ApplyChanges(model);
            consignmentItem.ObjectState = ObjectState.Modified;
            consignmentItem.UpdatedBy = _principal.Id;
            consignmentItem.UpdatedDate = DateTime.Now;
            _consignmentItemService.Update(consignmentItem);
            return model;
        }

        public bool InsertFile(File postedFile)
        {
            if (postedFile != null)
            {
                _fileService.Insert(postedFile);

                return true;
            }
            return false;
        }

        public bool InsertConsignmentFile(ConsignmentFile file)
        {
            if (file != null)
            {
                _consignmentfileService.Insert(file);

                return true;
            }
            return false;
        }

        private Consignment ApplyHeaderChanges(ConsignmentEditModel model)
        {
            var consignment = _consignmentService.ConsignmentById(model.ConsignmentID);
            consignment.SupplierReference = model.SupplierReference;
            consignment.DespatchDate = model.DespatchDate != null ? DateTime.Parse(model.DespatchDate) : DateTime.Now;
            consignment.PurchaseTypeID = model.PurchaseTypeID;
            
            return consignment;
        }

        private Consignment ApplyChanges(ConsignmentEditModel model)
        {
            // save id incase error
            string err_for_id = "No Consignment ID";
            if (model.ConsignmentID != Guid.Empty) err_for_id = model.ConsignmentID.ToString();

            var consignment = new Consignment();

            try
            {
                consignment.ConsignmentID = Guid.Empty != model.ConsignmentID ? model.ConsignmentID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                consignment.ConsignmentDescription = model.ConsignmentDescription;
                consignment.ConsignmentReference = model.ConsignmentReference;
                consignment.PortID = model.PortID != null ? model.PortID : null;
                //consignment.DivisionID = _principal.Identity.Name;
                consignment.PurchaseTypeID = model.PurchaseTypeID;
                consignment.Commission = model.Commission;
                consignment.Handling = model.Handling;
                consignment.ShowVehicleOnInvoice = true;
                consignment.Vehicle = model.Vehicle;
                consignment.VehicleDetail = model.VehicleDetail;
                consignment.SupplierDepartmentID = Guid.Parse(model.SupplierDepartmentID.ToString());
                consignment.SupplierReference = model.SupplierReference;
                consignment.DespatchLocationID = model.DespatchLocationID != null ? model.DespatchLocationID : null;
                consignment.DespatchDate = model.DespatchDate != null ? DateTime.Parse(model.DespatchDate) : DateTime.Now;
                //if (model.ReceivedDate != null) consignment.ReceivedDate = DateTime.Parse(model.ReceivedDate);
                //consignment. = Guid.Parse(model.CountryID.ToString());
                if (model.ContractDate != null) consignment.ContractDate = DateTime.Parse(model.ContractDate);

                //consignment.IsSaved = model.IsSaved;
                //consignment.CreatedDate = DateTime.Now; // !string.IsNullOrEmpty(model.CreatedDate)
                //? DateTime.Parse(model.CreatedDate)
                //: (DateTime?)null;
                //consignment.CreatedBy = _principal.Id;
                //consignment.UpdatedDate = !string.IsNullOrEmpty(model.UpdatedDate)
                //    ? DateTime.Parse(model.UpdatedDate)
                //    : (DateTime?)null;
                //consignment.UpdatedBy = _principal.Id;
                consignment.NoteID = model.NoteID;
                //consignment.Note.NoteText = model.NoteText;
                var nLst = new List<ConsignmentFile>();

                for (var i = 0; i < model.ConsignmentFileEditModels.Count; i++)
                {
                    nLst.Add(BuildConsignmentFileEditModel(model.ConsignmentFileEditModels[i], consignment.ConsignmentID));
                }

                consignment.ConsignmentFiles = nLst;
            }
            catch (Exception ex)
            {
                var err_detail = string.Format("Error in Consignment Orchestra: Method:Consignment: ApplyChanges: Note Service Insert for Id {0} ", err_for_id);
                throw new ApplicationException(err_detail, ex);
            }
            return consignment;
        }

        private ConsignmentItem ApplyChanges(ConsignmentItemEditModel model)
        {
            // save id incase error
            string err_for_id = "No Consignment ID";
            if (model.ConsignmentID != Guid.Empty) err_for_id = model.ConsignmentID.ToString();

            Note note = new Note();
            note.NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            note.NoteText = model.NoteText;
            note.NoteDescription = model.NoteText;

            // note.IsActive = true;
            try
            {
                _noteService.Insert(note);
            }
            catch (Exception ex)
            {
                var err_detail =
                    string.Format(
                        "Error in Consignment Orchestra: Method:Consignment Item: ApplyChanges: Note Service Insert for Id {0} ",
                        err_for_id);
                throw new ApplicationException(err_detail, ex);
            }

            ConsignmentItem newConsignmentItem;
            //return new ConsignmentItem
            try
            {
                newConsignmentItem = new ConsignmentItem
                {
                    ConsignmentItemID =
                        Guid.Empty != model.ConsignmentItemID
                            ? model.ConsignmentItemID
                            : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                    Brand = model.Brand,
                    BestBeforeDate = model.BestBeforeDate,
                    ConsignmentID = Guid.Parse(model.ConsignmentID.ToString()),
                    EstimatedChargeCost = model.EstimatedChargeCostPerPack,
                    EstimatedPurchaseCost = model.EstimatedPurchaseCostPerPack,
                    EstimatedProfit = model.EstimatedPercentageProfit,
                    NoteID = note.NoteID,
                    Note = note,
                    QuantityExpected = model.QuantityExpected,
                    PackPall = model.PackPall,
                    OriginCountryID = model.OriginCountryID,
                    PackSize = model.PackSize,
                    PackWeight = model.PackWeight,
                    PackType = model.PackType,
                    PackWtUnitID = Guid.Parse(model.PackWtUnitID.ToString()),
                    PorterageID = Guid.Parse(model.PorterageID.ToString()),
                    ProduceID = Guid.Parse(model.ProduceID.ToString()),
                    // Autocomplete is correct, there is an issue with the data here
                    // Empty GUID does not work, hardcoded guid in case dept is null!
                    DepartmentID = model.DepartmentID != Guid.Empty
                        ? model.DepartmentID
                        : Guid.Parse("00760000-0000-0001-0006-817528069450"), // found this code needs removed
                    //ReceivedQuantity = model.ReceivedQuantity,
                    //  IsActive = model.IsActive != null ? model.IsActive : false,
                    CreatedDate = DateTime.Now,
                    //!string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : (DateTime?) null,
                    CreatedBy = _principal.Id
                    //UpdatedDate = !string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : (DateTime?)null,
                    //UpdatedBy = _principal.Id
                };
                List<ConsignmentItemArrival> arrivalItemList = new List<ConsignmentItemArrival>();

                foreach (var arrivalItem in model.ConsignmentItemArrivals)
                {
                    var consignmentItemArrival = new ConsignmentItemArrival();
                    consignmentItemArrival.ConsignmentItemID = newConsignmentItem.ConsignmentItemID;
                    consignmentItemArrival.ConsignmentItemArrivalDate =
                        DateTime.Parse(arrivalItem.ConsignmentArrivalDate);
                    consignmentItemArrival.ConsignmentItemArrivalID = arrivalItem.ConsignmentItemArrivalID;

                    consignmentItemArrival.QuantityReceived = arrivalItem.Quantity;
                    consignmentItemArrival.StockLocationID = arrivalItem.StockLocationID;
                    //consignmentItemArrival.UpdatedBy = _principal.Id;
                    //consignmentItemArrival.UpdatedDate = DateTime.Now;
                    consignmentItemArrival.CreatedBy = _principal.Id;
                    consignmentItemArrival.CreatedDate = DateTime.Now;
                    //    consignmentItemArrival.IsActive = arrivalItem.IsActive;
                    arrivalItemList.Add(consignmentItemArrival);
                    // bodge
                    //newConsignmentItem.ExpectedQuantity = arrivalItem.Quantity;
                    //newConsignmentItem.ReceivedQuantity = arrivalItem.Quantity;
                }
                newConsignmentItem.ConsignmentItemArrivals = arrivalItemList;
            }
            catch (Exception ex)
            {
                var err_detail =
                    string.Format(
                        "Error in Consignment Orchestra: Method: ApplyChanges (ConsignmentItem): new Consignment Item {0} ",
                        err_for_id);
                throw new ApplicationException(err_detail, ex);
            }
            return newConsignmentItem;

        }

        private ConsignmentItem ApplyChangesForUpdate(ConsignmentItemEditModel model, bool allowedToModifyCost)
        {
            var c = _consignmentItemService.ConsignmentItemByIDSimple(model.ConsignmentItemID);

            if (c.CreatedDate >= DateTime.Now.AddDays(-1) || allowedToModifyCost)
                c.EstimatedPurchaseCost = model.EstimatedPurchaseCostPerPack;

            c.Brand = model.Brand;
            c.QuantityExpected = model.QuantityExpected;
            c.PackSize = model.PackSize;
            c.PackType = model.PackType;
            c.OriginCountryID = model.OriginCountryID;
            c.PackWeight = model.PackWeight;
            c.PackWtUnitID = model.PackWtUnitID;
            c.PorterageID = model.PorterageID;

            foreach (var arrivalItem in model.ConsignmentItemArrivals)
            {
                var consignmentItemArrival = _consignmentItemArrivalService.ConsignmentItemArrivalByIDSimple(arrivalItem.ConsignmentItemArrivalID);

                if (consignmentItemArrival == null)
                {
                    var newConsignmentItemArrival = new ConsignmentItemArrival();
                    newConsignmentItemArrival.ConsignmentItemID = model.ConsignmentItemID;
                    newConsignmentItemArrival.ConsignmentItemArrivalDate = DateTime.Parse(arrivalItem.ConsignmentArrivalDate);
                    newConsignmentItemArrival.ConsignmentItemArrivalID = arrivalItem.ConsignmentItemArrivalID;
                    newConsignmentItemArrival.QuantityReceived = arrivalItem.Quantity;
                    newConsignmentItemArrival.StockLocationID = arrivalItem.StockLocationID;
                    newConsignmentItemArrival.CreatedBy = _principal.Id;
                    newConsignmentItemArrival.CreatedDate = DateTime.Now;
                    _consignmentItemArrivalService.Insert(newConsignmentItemArrival);
                }
                else
                {
                    consignmentItemArrival.ConsignmentItemArrivalDate =
                        DateTime.Parse(arrivalItem.ConsignmentArrivalDate);
                    consignmentItemArrival.QuantityReceived = arrivalItem.Quantity;
                    consignmentItemArrival.UpdatedBy = _principal.Id;
                    consignmentItemArrival.UpdatedDate = DateTime.Now;
                    _consignmentItemArrivalService.Update(consignmentItemArrival);
                }
            }

            c.UpdatedDate = DateTime.Now;
            c.UpdatedBy = _principal.Id;

            return c;
        }

        private CompletedConsignment BuildConsignmentEditModelSimplified(Consignment entity)
        {
            CompletedConsignment consEditModel = new CompletedConsignment();
            if (entity != null)
            {
                consEditModel.ConsignmentID = entity.ConsignmentID;
                consEditModel.ConsignmentReference = entity.ConsignmentReference.Trim();
                consEditModel.SupplierCompanyName = entity.SupplierDepartment.Supplier.SupplierCompanyName;
                consEditModel.SupplierDepartmentName = entity.SupplierDepartment != null ? entity.SupplierDepartment.SupplierDepartmentName : string.Empty;
                consEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
                var createdByName = _applicationUserOrchestra.GetUsernameById(entity.CreatedBy);
                consEditModel.CreatedByName = createdByName;
                var completedConsignmentItems = new List<CompletedConsignmentItem>();
                foreach (var consignmentItem in entity.ConsignmentItems)
                {
                    var completedConsignmentItem = new CompletedConsignmentItem();
                    var tickets = new List<CompletedConsignmentsTicket>();
                    var ticketItems = new List<CompletedConsignmentsTicketItem>();
                    
                    foreach (var ticketItem in consignmentItem.TicketItems)
                    {
                        if (tickets.All(t => t.TicketReference != ticketItem.Ticket.TicketReference))
                        {
                            tickets.Add(new CompletedConsignmentsTicket()
                            {
                                TicketReference = ticketItem.Ticket.TicketReference,
                                TicketID = ticketItem.Ticket.TicketID,
                                TicketItems = ticketItems
                            }); //TicketTotal = ticketItem.Ticket.TicketItems.Sum(x => x.TicketItemTotalPrice) });
                        }
                        ticketItems.Add(new CompletedConsignmentsTicketItem(){TicketItemID = ticketItem.TicketItemID, TicketItemQuantity = ticketItem.TicketItemQuantity, TicketItemTotalPrice = ticketItem.TicketItemTotalPrice});
                    }

                    var consigmenItemArrivals = consignmentItem.ConsignmentItemArrivals.Select(arrival => new ConsignmentItemArrivalEditModel() {ConsignmentArrivalDateString = arrival.ConsignmentItemArrivalDate.ToString(), Quantity = arrival.QuantityReceived}).ToList();

                    completedConsignmentItem.ProduceName = consignmentItem.Produce.ProduceName;
                    completedConsignmentItem.EstimatedPurchaseCost = consignmentItem.EstimatedPurchaseCost;
                    completedConsignmentItem.Tickets = tickets;
                    completedConsignmentItem.ConsignmentItemArrivals = consigmenItemArrivals;
                    completedConsignmentItems.Add(completedConsignmentItem);
                }

                consEditModel.ConsignmentItems = completedConsignmentItems;
            }
            
            return consEditModel;
        }

        private ConsignmentEditModel BuildConsignmentEditModel(Consignment entity)
        {
            ConsignmentEditModel consEditModel = new ConsignmentEditModel();
            if (entity != null)
            {
                var fileConv = new List<FileEditModel>();

                if (entity.ConsignmentFiles != null && entity.ConsignmentFiles.Count > 0)
                {
                    for (var i = 0; i < entity.ConsignmentFiles.Count; i++)
                    {
                        fileConv.Add(ConsFileConv(entity.ConsignmentFiles.ElementAt(i)));
                    }
                }

                consEditModel.ConsignmentID = entity.ConsignmentID;
                consEditModel.ConsignmentDescription = entity.ConsignmentDescription;
                consEditModel.ConsignmentItemEditModels = entity.ConsignmentItems.Select(BuildConsignmentItemEditModel).ToList();
                consEditModel.ConsignmentFileEditModels = entity.ConsignmentFiles.Select(BuildConsignmentFileConvEditModel).ToList();
                consEditModel.NoteID = entity.Note != null && entity.Note.NoteID != Guid.Empty ? entity.NoteID : null;
                consEditModel.NoteText = entity.Note != null ? entity.Note.NoteText : string.Empty;
                consEditModel.FileEditModels = fileConv;
                //consEditModel.IsSaved = entity.IsSaved;
                consEditModel.ItemCount = entity.ConsignmentItems != null ? entity.ConsignmentItems.Count : 0;
                consEditModel.FileCount = entity.ConsignmentFiles != null ? entity.ConsignmentFiles.Count : 0;
                consEditModel.ConsignmentReference = entity.ConsignmentReference.Trim();
                consEditModel.DespatchDate = entity.DespatchDate.ToString();
                consEditModel.Commission = entity.Commission;
                consEditModel.Handling = entity.Handling;
                consEditModel.Vehicle = entity.Vehicle;
                consEditModel.VehicleDetail = entity.Vehicle;
                consEditModel.SupplierDepartmentID = entity.SupplierDepartment != null ? entity.SupplierDepartment.SupplierDepartmentID : Guid.Empty;
                consEditModel.SupplierDepartmentName = entity.SupplierDepartment != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.SupplierDepartment.SupplierDepartmentName.ToLower()) : string.Empty;
                consEditModel.SupplierReference = entity.SupplierReference;
                consEditModel.SupplierID = entity.SupplierDepartment != null && entity.SupplierDepartment.Supplier != null ? entity.SupplierDepartment.Supplier.SupplierID : Guid.Empty;
                consEditModel.SupplierCompanyName = entity.SupplierDepartment != null && entity.SupplierDepartment.Supplier != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.SupplierDepartment.Supplier.SupplierCompanyName.ToLower()) : string.Empty;
                consEditModel.PortID = entity.Port != null ? entity.PortID : null;
                consEditModel.PortName = entity.Port != null ? entity.Port.PortCode + "-" + entity.Port.PortName : null;
                consEditModel.PurchaseTypeID = entity.PurchaseType != null ? entity.PurchaseType.PurchaseTypeID : Guid.Empty;
                consEditModel.PurchaseTypeName = entity.PurchaseType != null ? entity.PurchaseType.PurchaseTypeName + "-" + entity.PurchaseType.PurchaseTypeCode : string.Empty;
                consEditModel.DespatchLocationID = entity.DespatchLocation != null ? entity.DespatchLocation.DespatchLocationID : Guid.Empty;
                consEditModel.DespatchName = entity.DespatchLocation != null ? entity.DespatchLocation.DespatchLocationName + "-" + entity.DespatchLocation.DespatchLocationCode : string.Empty;
                //consEditModel.ReceivedDate = entity.ReceivedDate.HasValue ? entity.ReceivedDate.Value.ToString() : string.Empty;
                consEditModel.ContractDate = entity.ContractDate.HasValue ? entity.ContractDate.Value.ToString() : string.Empty;

                //consEditModel.UpdatedBy = entity.UpdatedBy;
                consEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
                // consEditModel.CreatedBy = entity.CreatedBy;
                consEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
                var createdByName = _applicationUserOrchestra.GetUsernameById(entity.CreatedBy);
                consEditModel.CreatedByName = createdByName;
                var updatedByName = _applicationUserOrchestra.GetUsernameById(entity.UpdatedBy);
                consEditModel.UpdatedByName = updatedByName;
            }

            return consEditModel;
        }

        private ConsignmentItemEditModel BuildConsignmentItemEditModel(ConsignmentItem entity)
        {
            var consignmentItemEditModel = new ConsignmentItemEditModel();
            consignmentItemEditModel.ConsignmentItemID = entity.ConsignmentItemID;
            consignmentItemEditModel.ConsignmentID = entity.ConsignmentID ?? Guid.Empty;
            consignmentItemEditModel.ProduceID = entity.Produce != null ? entity.Produce.ProduceID : Guid.Empty;
            consignmentItemEditModel.Produce = entity.Produce != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Produce.ProduceName.ToLower()) + " [" + entity.Produce.ProduceCode + "] " : string.Empty;

            consignmentItemEditModel.PorterageID = entity.PorterageID;
            consignmentItemEditModel.PorterageCode = entity.Porterage != null ? entity.Porterage.PorterageCode : string.Empty;
            consignmentItemEditModel.PackWtUnitID = entity.PackWtUnitID ?? Guid.Empty;
            consignmentItemEditModel.WtUnit = entity.PackWtUnit != null ? entity.PackWtUnit.WtUnit : string.Empty;
            try
            {
                var testId = entity.PackWtUnitID;
                var testpackWtUnit = _packWtUnitService.PackWtUnitById(testId.GetValueOrDefault());
                consignmentItemEditModel.WtUnit = testpackWtUnit.WtUnit != null ? testpackWtUnit.WtUnit : string.Empty;
                var testcountrycode = _countryService.CountryById(entity.Country.CountryID);
                consignmentItemEditModel.CountryCode = testcountrycode.CountryCode;
            }
            catch (Exception ex) { }
            //consignmentItemEditModel.WtUnit = entity.PackWtUnit != null ? entity.PackWtUnit.WtUnit : string.Empty;
            //consignmentItemEditModel.WtUnit = packWtUnit.WtUnit != null ? packWtUnit.WtUnit : string.Empty;
            consignmentItemEditModel.BestBeforeDate = entity.BestBeforeDate;
            consignmentItemEditModel.EstimatedChargeCostPerPack = entity.EstimatedChargeCost;
            consignmentItemEditModel.EstimatedPurchaseCostPerPack = entity.EstimatedPurchaseCost;
            consignmentItemEditModel.EstimatedPercentageProfit = entity.EstimatedProfit;
            consignmentItemEditModel.OriginCountryID = entity.Country != null ? entity.Country.CountryID : GetDefaultCountry().CountryID;
            consignmentItemEditModel.CountryName = entity.Country != null ? entity.Country.CountryName : GetDefaultCountry().CountryName;
            consignmentItemEditModel.DepartmentID = entity.Department != null ? entity.DepartmentID.Value : Guid.Empty;
            consignmentItemEditModel.DepartmentName = entity.Department != null ? entity.Department.DepartmentName : string.Empty;
            consignmentItemEditModel.DepartmentCode = entity.Department != null ? entity.Department.DepartmentCode : string.Empty;
            consignmentItemEditModel.NoteID = entity.Note != null ? entity.Note.NoteID : Guid.Empty;
            consignmentItemEditModel.NoteText = entity.Note != null ? entity.Note.NoteText : string.Empty;
            consignmentItemEditModel.Brand = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Brand.ToLower());
            consignmentItemEditModel.PackType = entity.PackType;
            consignmentItemEditModel.PackSize = entity.PackSize;
            consignmentItemEditModel.PackWeight = entity.PackWeight;
            consignmentItemEditModel.PackPall = entity.PackPall ?? 0;
            consignmentItemEditModel.QuantityExpected = entity.QuantityExpected;

            //consignmentItemEditModel.ReceivedQuantity = entity.ReceivedQuantity;
            //consignmentItemEditModel.ExpectedQuantity = entity.ExpectedQuantity;
            // consignmentItemEditModel.IsActive = entity.IsActive ?? false;
            //consignmentItemEditModel.UpdatedBy = entity.UpdatedBy;
            consignmentItemEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
            // consignmentItemEditModel.CreatedBy = entity.CreatedBy;
            consignmentItemEditModel.IsCostDisabled = entity.CreatedDate < DateTime.Now.AddDays(-1);
            consignmentItemEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;

            // Get Item Arrival
            //consignmentItemEditModel.ConsignmentItemArrivals =
            List<ConsignmentItemArrival> consignmentItemArrivals = new List<ConsignmentItemArrival>();
            consignmentItemArrivals = _consignmentItemArrivalService.ConsignmentItemArrivalsOnlyByConsignmentItemID(entity.ConsignmentItemID);
            consignmentItemEditModel.ConsignmentItemArrivals = BuildConsignmentItemArrivalEditModelList(consignmentItemArrivals);

            consignmentItemEditModel.ReturnsCollection = new ConsignmentItemReturns(){ConsignmentItemID = entity.ConsignmentItemID, ConsignmentItemPriceReturnModels = BuildConsignmentItemPriceReturns(entity.ConsignmentItemPriceReturns)};

            return consignmentItemEditModel;
        }

        private List<ConsignmentItemPriceReturnModel> BuildConsignmentItemPriceReturns(ICollection<ConsignmentItemPriceReturn> consignmentItemPriceReturns)
        {
            var list = new List<ConsignmentItemPriceReturnModel>();
            foreach (var c in consignmentItemPriceReturns)
            {
                list.Add(new ConsignmentItemPriceReturnModel() { ReturnQuantity = c.ReturnQuantity, ReturnUnitPrice = c.ReturnUnitPrice, ConsignmentItemPriceReturnID = c.ConsignmentItemPriceReturnID });
            }

            return list;
        }

        private List<ConsignmentItemArrivalEditModel> BuildConsignmentItemArrivalEditModelList(List<ConsignmentItemArrival> consignmentItemArrivalList)
        {
            var consignmentItemArrivalEditModelList = new List<ConsignmentItemArrivalEditModel>();
            foreach (var c in consignmentItemArrivalList.OrderBy(a => a.ConsignmentItemArrivalDate))
            {
                consignmentItemArrivalEditModelList.Add(BuildConsignmentItemArrivalEditModel(c));
            }

            return consignmentItemArrivalEditModelList;
        }

        private ConsignmentItemArrivalEditModel BuildConsignmentItemArrivalEditModel(ConsignmentItemArrival consignmentItemArrival)
        {
            var consignmentItemArrivalEditModel = new ConsignmentItemArrivalEditModel();
            consignmentItemArrivalEditModel.ConsignmentArrivalDate = consignmentItemArrival.ConsignmentItemArrivalDate.ToString();
            consignmentItemArrivalEditModel.ConsignmentArrivalDateString = consignmentItemArrival.ConsignmentItemArrivalDate.ToShortDateString();
            consignmentItemArrivalEditModel.ConsignmentItemArrivalID = consignmentItemArrival.ConsignmentItemArrivalID;
            consignmentItemArrivalEditModel.ConsignmentItemID = consignmentItemArrival.ConsignmentItemID.GetValueOrDefault();
            consignmentItemArrivalEditModel.IsActive = true;
            consignmentItemArrivalEditModel.IsExpected = true;
            consignmentItemArrivalEditModel.NoteID = consignmentItemArrival.NoteID;
            consignmentItemArrivalEditModel.Quantity = consignmentItemArrival.QuantityReceived;
            consignmentItemArrivalEditModel.StockLocationID = consignmentItemArrival.StockLocationID;
            return consignmentItemArrivalEditModel;
        }

        private Country GetDefaultCountry()
        {
            var country = _countryService.GetDefaultCountry();
            return country;
        }

        private ConsignmentFileEditModel BuildConsignmentFileConvEditModel(ConsignmentFile entity)
        {
            var consignmentFileEditModel = new ConsignmentFileEditModel();
            consignmentFileEditModel.ConsignmentFileID = entity.ConsignmentFileID;
            consignmentFileEditModel.FileID = entity.FileID.Value;
            consignmentFileEditModel.ConsignmentID = entity.ConsignmentID.Value;

            return consignmentFileEditModel;
        }

        private ConsignmentFile BuildConsignmentFileEditModel(ConsignmentFileEditModel entity, Guid ConsignmentID)
        {
            var consignmentFile = new ConsignmentFile();
            consignmentFile.ConsignmentFileID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            consignmentFile.FileID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]); //entity.FileID != null ? entity.FileID :
            consignmentFile.ConsignmentID = ConsignmentID;

            return consignmentFile;
        }

        public FileEditModel ConsFileConv(ConsignmentFile entity)
        {
            var FileEditModel = new FileEditModel();
            //var File = new File();
            //// File = _fileService.FileById(entity.FileID.Value);
            // //Just for testing
            // FileEditModel.FileContent = null; // File.FileContent;
            // var pos = File.FileName.IndexOf("\\");
            // var length = File.FileName.Length;
            // if (pos == -1)
            // {
            //     pos = 0;
            // }
            // var fName = File.FileName.Substring(pos, length);
            // FileEditModel.FileName = fName;
            // FileEditModel.FileID = File.FileID;

            return FileEditModel;
        }

        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            var result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        private FileEditModel BuildFileEditModel(File entity)
        {
            var FileEditModel = new FileEditModel();
            FileEditModel.FileContent = entity.FileContent;
            var pos = entity.FileName.IndexOf("\\");
            var length = entity.FileName.Length;
            if (pos == -1)
            {
                pos = 0;
            }
            var fName = entity.FileName.Substring(pos, length);
            FileEditModel.FileName = fName;
            FileEditModel.FileID = entity.FileID;

            return FileEditModel;
        }

        private string ConvertIntToStr(int input)
        {
            lock (this)
            {
                var output = "";
                while (input > 0)
                {
                    var current = input % 10;
                    input /= 10;

                    if (current == 0)
                        current = 10;

                    output = (char)('A' + (current - 1)) + output;
                }
                return output;
            }
        }
        private string GetNewConsignmentReference(Guid DivisionID)
        {
            string strNewConsignmentReference;
            //To do: Cache these settings
            //Find the company for this division
            Guid CompanyID = _divisionService.GetCompanyIDForDivisionID(DivisionID);
            bool? ConsignmentNumberUniqueForCompany = _setupGlobalService.GetSetupBooleanByNameAndCompanyID("ConsignmentNumberUniqueForCompany", CompanyID);
            if(ConsignmentNumberUniqueForCompany == null)
            {
                throw new Exception("ConsignmentNumberUniqueForCompany not set for this company. Please contact technical support.");
            }
            bool? AutogenerateConsignmentNumber = _setupGlobalService.GetSetupBooleanByNameAndDivisionID("AutogenerateConsignmentNumber", DivisionID);
            if (AutogenerateConsignmentNumber == null)
            {
                throw new System.Exception("AutogenerateConsignmentNumber not set for this division. Please contact technical support.");
            }
            string ConsignmentNumberPrefix = _setupLocalService.GetSetupStringByNameAndCompanyID("ConsignmentNumberPrefix", CompanyID);
            if (ConsignmentNumberPrefix == null)
            {
                throw new System.Exception("ConsignmentNumberPrefix not set for this company. Please contact technical support.");
            }

            //End of To do cache settings
            if (!(bool)AutogenerateConsignmentNumber)
            {
                return null;
            }
            if ((bool)ConsignmentNumberUniqueForCompany)
            {
                strNewConsignmentReference = ConsignmentNumberPrefix + _setupGlobalService.GetSetupIntByNameAndCompanyID("NextConsignmentNumber", CompanyID, true).ToString();
                if (strNewConsignmentReference == null)
                {
                    throw new Exception("NextConsignmentNumber not set for this company. Please contact technical support.");
                }
            }
            else
            {
                strNewConsignmentReference = ConsignmentNumberPrefix + _setupGlobalService.GetSetupIntByNameAndDivisionID("NextConsignmentNumber", DivisionID, true).ToString();
                if (strNewConsignmentReference == null)
                {
                    throw new Exception("NextConsignmentNumber not set for this division. Please contact technical support.");
                }
            }
            return strNewConsignmentReference;
        }
    }
}
