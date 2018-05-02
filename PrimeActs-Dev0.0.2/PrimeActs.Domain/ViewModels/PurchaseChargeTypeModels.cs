using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class PurchaseChargeTypeEditModel
    {
        public Guid PurchaseChargeTypeID { get; set; }
        public string PurchaseChargeTypeName { get; set; }
        public string PurchaseChargeTypeCode { get; set; }
    }

    public class PurchaseChargeTypeViewModel : PurchaseChargeTypeEditModel
    {
    }
}
