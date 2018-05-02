using System;
using System.ComponentModel.DataAnnotations.Schema;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Domain
{
    public partial class ConsignmentItemPriceReturn : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public System.Guid ConsignmentItemPriceReturnID { get; set; }
        public System.Guid ConsignmentItemID { get; set; }

        public int ReturnQuantity { get; set; }
        public decimal ReturnUnitPrice { get; set; }
        public DateTime ReturnDate { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
        
        public Nullable<System.Guid> UpdatedByUserID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedByUserID { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
