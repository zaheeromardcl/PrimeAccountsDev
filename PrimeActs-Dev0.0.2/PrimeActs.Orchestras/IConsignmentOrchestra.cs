#region

using System;
using System.Security.Principal;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;

#endregion

namespace PrimeActs.Orchestras
{
    //public interface IConsignmentOrchestra
    //{
    //    void Initialize(ApplicationUser principal);
    //    bool Validate(ConsignmentEditModel model);
    //    bool Validate(ConsignmentItemEditModel model);
    //    bool InsertFile(File postedFile);
    //    bool InsertConsignmentFile(ConsignmentFile file);
    //    ConsignmentViewModel GetConsignmentViewModel(Guid id);
    //    ConsignmentDetailsViewModel GetConsignmentDetailsViewModel(Guid id);
    //    ConsignmentItemViewModel GetConsignmentItemViewModel(Guid id);
    //    ConsignmentPagingModel GetConsignmentPagingModel(QueryOptions queryOptions, SearchObject searchObject);
    //    ResultList<ConsignmentEditModel> GetConsignments(QueryOptions queryOptions, SearchObject searchObject);
    //    // List<ConsignmentEditModel> GetConsignmentUserNames(QueryOptions queryOptions);
    //    ConsignmentEditModel CreateConsignment(ConsignmentEditModel model);
    //    ConsignmentEditModel UpdateConsignment(ConsignmentEditModel model);
    //    ConsignmentItemEditModel CreateConsignmentItem(ConsignmentItemEditModel model);
    //    ConsignmentItemEditModel UpdateConsignmentItem(ConsignmentItemEditModel model);
    //}
}