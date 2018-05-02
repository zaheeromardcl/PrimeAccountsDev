using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.PurchaseInvoice
{
    public class PurchaseInvoiceDetailsViewModel : PurchaseInvoiceModel
    {
        public IEnumerable<PurchaseInvoiceItemModel> PurchaseInvoiceItems { get; set; }
    }

    public class PurchaseInvoiceItemModel
    {
        public string Description { get; set; }

        public decimal? CurrencyAmount { get; set; }

        public string Currency { get; set; }
        public decimal? Quantity { get; set; }
        
        public decimal? TotalPrice { get; set; }
        public double UnitPrice { get; set; }
        public double EstimatedPurchaseCost { get; set; }
        public Guid PurchaseInvoiceItemID { get; set; }
        public Guid PurchaseInvoiceID { get; set; }
        public Guid? ConsignmentItemID { get; set; }
    
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public Guid UpdatedBy { get; set; }
        public Guid CreatedBy { get; set; }
        public string Alias { get; set; }
        public List<string> Notes { get; set; }
        public Guid? PurchaseInvoiceItemChargeTypeID { get; set; }
    }
}
