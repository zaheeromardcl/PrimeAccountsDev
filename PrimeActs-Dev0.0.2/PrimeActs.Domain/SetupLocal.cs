using System;
using System.Collections.Generic;
using PrimeActs.Domain.Abstract;

namespace PrimeActs.Domain
{
    public partial class SetupLocal : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public string SetupName { get; set; }
        public int SetupValueType { get; set; }
        public Nullable<int> SetupValueInt { get; set; }
        public Nullable<decimal> SetupValueNumeric { get; set; }
        public Nullable<bool> SetupValueBit { get; set; }
        public string SetupValueNvarchar { get; set; }
        public Nullable<System.Guid> SetupValueUniqueIdentifier { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public System.Guid  SetupID {get; set;}
        public Nullable<System.Guid> CompanyID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public Nullable<System.Guid> DepartmentID { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        
    }
}
