#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class PurchaseTypeEditModel
    {
        public Guid PurchaseTypeID { get; set; }
        public string PurchaseTypeName { get; set; }
        public string PurchaseTypeCode { get; set; }
    }

    public class PurchaseTypeViewModel : PurchaseTypeEditModel
    {
    }
}