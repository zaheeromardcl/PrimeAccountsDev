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

        public decimal? ExchangeRate { get; set; }

        public string Currency { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? FXTotalPrice { get; set; }
        public decimal? TotalPrice { get; set; }
        public double UnitPrice { get; set; }
        public Guid PurchaseInvoiceItemID { get; set; }
        public Guid PurchaseInvoiceID { get; set; }
        public Guid? ConsignmentItemID { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string CreatedBy { get; set; }
    }
}
