#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using System.Security.Principal;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Produce;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

#endregion

namespace PrimeActs.Orchestras
{
    public class ProduceOrchestra : IProduceOrchestra
    {
        private readonly IMasterGroupService _masterGroupService;
        private readonly IProduceGroupService _produceGroupService;
        private readonly IProduceService _produceService;
        private readonly ITransactionTaxCodeService _transactionTaxCodeService;
        private IValidationDictionary _validationDictionary;
        private ApplicationUser _principal;
        private readonly string _serverCode;
       
        //private IProduceIntraStatService _produceIntraStatService;


        public ProduceOrchestra(ISetupLocalService setupLocalService, IProduceService produceService, IMasterGroupService masterGroupService,IProduceGroupService produceGroupService, ITransactionTaxCodeService transactionTaxCodeService)
            // ,IProduceIntraStatService produceIntraStatService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _produceService = produceService;
            _masterGroupService = masterGroupService;
            //_produceIntraStatService = produceIntraStatService;
            _produceGroupService = produceGroupService;
            _transactionTaxCodeService = transactionTaxCodeService;
             
        }

        public void Initialize1(ApplicationUser principal)
        {
            _principal = principal;
        }
        public void Initialize(IValidationDictionary validationDictionary)
        {
            _validationDictionary = validationDictionary;
        }


        public ProduceEditModel GetModel(Guid id)
        {
            return CreateFrom(_produceService.ProduceById(id));
        }

        public List<ProduceViewModel> GetAllModels(string IsActive, int page = 1, int pageSize = 5, string searchString ="", string sortColumn = "", string sortOrder = "")
        {
            var produceViewModels = new List<ProduceViewModel>();
            var produces = new List<Produce>();
            Func<Produce, string> funcSortColumn;
            switch (sortColumn)
            {
                case "ProduceName":
                    funcSortColumn = x => x.ProduceName;
                    break;
                case "ProduceCode":
                    funcSortColumn = x => x.ProduceCode;
                    break;
                case "MasterGroupName":
                    funcSortColumn = x => x.MasterGroup.MasterGroupName;
                    break;
                case "ProduceGroupName":
                    funcSortColumn = x => x.ProduceGroup.ProduceGroupName;
                    break;
                case "Active":
                    funcSortColumn = x => x.IsActive.ToString();
                    break;
                default:
                    funcSortColumn = x => x.ProduceName;

                    break;
            }
            if ((IsActive == null) || IsActive == "3")
            {
                produces =
                    _produceService.GetAllProduces()
                        .Where(
                            m =>
                                m.ProduceName.ToLower().Contains(searchString) |
                                m.ProduceCode.ToLower().Contains(searchString) | m.ProduceName.Contains(searchString) |
                                m.ProduceCode.Contains(searchString))
                        .OrderBy(funcSortColumn)
                        .ToList();
            }
            else
            {
                var active = false;
                if (IsActive == "1")
                {
                    active = true;
                }
                else if (IsActive == "2")
                {
                    active = false;
                }
                produces =
                    _produceService.GetAllProduces()
                        .Where(
                            m =>
                                m.ProduceName.ToLower().Contains(searchString) |
                                m.ProduceCode.ToLower().Contains(searchString) | m.ProduceName.Contains(searchString) |
                                m.ProduceCode.Contains(searchString))
                        .Where(m => m.IsActive == active)
                        .OrderBy(funcSortColumn)
                        .ToList();
            }

            foreach (var produce in produces)
            {
                produceViewModels.Add(new ProduceViewModel
                {
                    ProduceID = produce.ProduceID,
                    ProduceName = produce.ProduceName,
                    ProduceCode = produce.ProduceCode,
                    IsActive = produce.IsActive,
                    RelatedProduceGroup = produce.ProduceGroup != null ? CreateFrom(produce.ProduceGroup) : null,
                    RelatedMasterGroup = produce.MasterGroup != null ? CreateFrom(produce.MasterGroup) : null
                });
            }
            return produceViewModels;
        }

        public ProduceEditModel Insert(ProduceEditModel model)
        {
            var produce = ApplyChanges(model);
            produce.IsActive = true;
            produce.ObjectState = ObjectState.Added;
            _produceService.Insert(produce);
            //_produceService.RefreshCache();
            model.ProduceID = produce.ProduceID;
            return model;
        }

        public bool Validate(ProduceEditModel model)
        {
            var validator = new ProduceEditModelValidator();
            var result = validator.Validate(model);
            if (!result.IsValid)
            {
                foreach (var failer in result.Errors)
                    _validationDictionary.AddError(failer.PropertyName, failer.ErrorMessage);
            }
            return result.IsValid;
        }

        public ProduceEditModel CreateFrom(Produce entity)
        {
            return new ProduceEditModel
            {
                ProduceID = entity.ProduceID,
                ProduceCode = entity.ProduceCode,
                ProduceName = entity.ProduceName,
                IsActive = entity.IsActive ?? false,
                SelectedMasterGroup =
                    entity.MasterGroup != null ? entity.MasterGroup.MasterGroupID.ToString() : string.Empty,
                SelectedProduceGroup =
                    entity.ProduceGroup != null ? entity.ProduceGroup.ProduceGroupID.ToString() : string.Empty,
                RelatedProduceGroup = entity.ProduceGroup != null ? CreateFrom(entity.ProduceGroup) : null,
                RelatedMasterGroup = entity.MasterGroup != null ? CreateFrom(entity.MasterGroup) : null,
                
            };
        }

        public ProduceViewModel Rebuild(ProduceEditModel model)
        {
            var produceViewModel = new ProduceViewModel
            {
                ProduceID = model.ProduceID,
                ProduceCode = model.ProduceCode,
                ProduceName = model.ProduceName,
                IsActive = model.IsActive,
                SelectedMasterGroup = model.SelectedMasterGroup,
                SelectedProduceGroup = model.SelectedProduceGroup
            };
            produceViewModel.MasterGroupList =
                _masterGroupService.Query()
                    .Select(m => new dropdownlistModel {Id = m.MasterGroupID, Name = m.MasterGroupName})
                    .ToList();
            produceViewModel.ProduceGroupList =
                _produceGroupService.Query()
                    .Select(m => new dropdownlistModel {Id = m.ProduceGroupID, Name = m.ProduceGroupName})
                    .ToList();
            //produceViewModel.TransactionTaxCodeList =
            //    _transactionTaxCodeService.Query()
            //        .Select(m => new dropdownlistModel {Id = m.TransactionTaxCodeID, Name = m.TransactionTaxCodeValue})
            //        .ToList();
            return produceViewModel;
        }

        public Produce ApplyChanges(ProduceEditModel model)
        {
            return new Produce
            {
                ProduceID = Guid.Empty != model.ProduceID ? model.ProduceID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                ProduceCode = model.ProduceCode,
                ProduceName = model.ProduceName,
                IsActive = model.IsActive,
                ProduceGroupID = Guid.Parse(model.SelectedProduceGroup),
                MasterGroupID = Guid.Parse(model.SelectedMasterGroup),
                
            };
        }


        public void Update(ProduceEditModel model)
        {
            var produce = ApplyChanges(model);
            produce.ObjectState = ObjectState.Modified;
            _produceService.Update(produce);
            //_produceService.RefreshCache();
        }


        //public void RefreshCache()
        //{
        //    _produceService.RefreshCache();
        //}

        public List<ProduceEditModel> GetProduceModelsForAC(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _produceService.GetAllProduces()
                    .Where(x => (x.ProduceCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) 
                        | x.ProduceName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)))
                    .Select(entity => BuildProduceEditModelAC(entity))
                    .ToList();
        }

        public List<ProduceEditModel> GetProduceModelsForACByConsignmentItemDepartmentID(string search, Guid ciDepartmentID)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _produceService.GetAllProducesByConsignmentItemDepartmentID(ciDepartmentID)
                    .Where(x => (x.ProduceCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)
                        | x.ProduceName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)))
                    .Select(entity => BuildProduceEditModelAC(entity))
                    .ToList();
        }

        public List<ProduceQuantityForTicket> GetProduceForAcForProduceQuantityForTickets(string search, Guid? userDivisionID)
        {

         
           return new ProduceForTicketService().GetProduceQuantityForTicket(search, search, userDivisionID);    //_principal.DivisionId.Value);
        }

        public List<ProduceQuantityForTicket> GetProduceForAcForProduceQuantityForPurchaseInvoice(string search, Guid? userDivisionID)
        {


            return new ProduceForTicketService().GetProduceQuantityForPurchaseInvoice(search, search, userDivisionID);    //_principal.DivisionId.Value);
        }

        private ProduceGroupEditModel CreateFrom(ProduceGroup entity)
        {
            return new ProduceGroupEditModel
            {
                ProduceGroupID = entity.ProduceGroupID,
                ProduceGroupCode = entity.ProduceGroupCode,
                ProduceGroupName = entity.ProduceGroupName
            };
        }

        private MasterGroupEditModel CreateFrom(MasterGroup entity)
        {
            return new MasterGroupEditModel
            {
                MasterGroupID = entity.MasterGroupID,
                MasterGroupCode = entity.MasterGroupCode,
                MasterGroupName = entity.MasterGroupName
            };
        }

        private ProduceEditModel BuildProduceEditModelAC(Produce entity)
        {
            return new ProduceEditModel
            {
                ProduceID = entity.ProduceID,
                ProduceCode = entity.ProduceCode,
                ProduceName = entity.ProduceName + " [" + entity.ProduceCode +"] ",
                
            };
        }

        public ProducePagingModel GetProducePagingModel(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Produce.SearchObject searchObject) 
        {
            var totalCount = 0;
            var producePagingModel = new ProducePagingModel();
            var produces = _produceService.GetProduces(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //This line gets rid of items!! Fix the error
            var result = new ResultList<ProduceEditModel>(produces.Select(BuildProduceEditModel).ToList(),
                queryOptions);
            producePagingModel.ProduceEditModels = result;
            producePagingModel.SearchObject = new PrimeActs.Domain.ViewModels.Produce.SearchObject
            {
                ProduceCode = searchObject.ProduceCode,
                ProduceName = searchObject.ProduceName
            };
            return producePagingModel;
        }

        public ResultList<ProduceEditModel> GetProduces(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Produce.SearchObject searchObject)
        {
            var totalCount = 0;
            var produces = _produceService.GetProduces(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<ProduceEditModel>(
                    produces != null ? produces.Select(BuildProduceEditModel).ToList() : null, queryOptions);
        }

        private ProduceEditModel BuildProduceEditModel(Produce entity)
        {   
            ProduceEditModel consEditModel = new ProduceEditModel();
            consEditModel.ProduceID = entity.ProduceID;
            consEditModel.ProduceName = entity.ProduceName;
            consEditModel.ProduceCode = entity.ProduceCode;
            consEditModel.IsActive = entity.IsActive ?? false;
           // consEditModel.UpdatedBy = entity.UpdatedBy;
            consEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
          //  consEditModel.CreatedBy = entity.CreatedBy;
            consEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
            
            return consEditModel;
        }
    }
}