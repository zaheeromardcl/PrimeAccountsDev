#region

using PrimeActs.Domain.ViewModels.Consignment;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public static class Helpers
    {
        public static ConsignmentViewModel CreateConsignmentViewModelFromConsignment(Domain.Consignment consignment)
        {
            var consignmentViewModel = new ConsignmentViewModel();
            //consignmentViewModel.ConsignmentID = consignment.ConsignmentID;
            //consignmentViewModel.ConsignmentReference = consignment.ConsignmentReference;
            //consignmentViewModel.ConsignmentReference = consignment.ConsignmentReference;
            //consignmentViewModel.ConsignmentDescription = consignment.ConsignmentReference;


            return consignmentViewModel;
        }
    }
}