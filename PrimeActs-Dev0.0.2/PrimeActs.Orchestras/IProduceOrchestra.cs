#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Produce;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Validation;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IProduceOrchestra
    {
        ProduceEditModel GetModel(Guid id);
        List<ProduceEditModel> GetProduceModelsForAC(string search);
        List<ProduceQuantityForTicket> GetProduceForAcForProduceQuantityForTickets(string search, Guid? userDivisionID);
        
        List<ProduceViewModel> GetAllModels(string IsActive, int page = 1, int pageSize = 5, string searchString = "", string sortColumn = "", string sortOrder = "");
        List<ProduceEditModel> GetProduceModelsForACByConsignmentItemDepartmentID(string search, Guid ciDepartmentID);
        ProduceEditModel CreateFrom(Produce entity);
        ProduceViewModel Rebuild(ProduceEditModel model);
        Produce ApplyChanges(ProduceEditModel model);
        ProduceEditModel Insert(ProduceEditModel model);
        void Initialize(IValidationDictionary validationDictionary);
        void Initialize1(ApplicationUser principal);
        void Update(ProduceEditModel model);
        bool Validate(ProduceEditModel model);
        //void RefreshCache();

        ProducePagingModel GetProducePagingModel(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Produce.SearchObject searchObject);

        ResultList<ProduceEditModel> GetProduces(QueryOptions queryOptions, Domain.ViewModels.Produce.SearchObject searchObject);
        List<ProduceQuantityForTicket> GetProduceForAcForProduceQuantityForPurchaseInvoice(string search, Guid? userDivisionID);
    }
}