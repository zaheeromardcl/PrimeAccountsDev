using System.Collections.Generic;
using PrimeActs.Domain.ViewModels.Users;

namespace PrimeActs.Domain.ViewModels.PurchaseInvoice
{
    public class PurchaseInvoiceViewModel : PurchaseInvoiceModel
    {
        public UserContextModel UserContextModel { get; set; }
        public List<PurchaseChargeTypeEditModel> PurchaseChargeTypes { get; set; }
    }
}