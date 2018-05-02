using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerTypeEditModel
    {
        public Guid CustomerTypeID { get; set; }
        public string CustomerTypeCode { get; set; }
        public string CustomerTypeDescription { get; set; }
        public Guid CustomerID { get; set; }
        public Guid UpdatedByUserID { get; set; }
        public Guid CreatedByUserID { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CustomerTypeViewModel //: CustomerTypeEditModel
    {
        public CustomerTypeEditModel CustomerTypeItem { get; set; }
        public List<CustomerTypeEditModel> CustomerTypeItemList { get; set; }
    }
}
