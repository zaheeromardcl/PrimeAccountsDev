using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain
{
    public class PaymentType :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Guid PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeCode { get; set; }
        public int Order { get; set; }
        public bool Default { get; set; }
        public bool IsActive { get; set; }


        [NotMapped]
        public ObjectState ObjectState { get; set; }
       
    }
}
