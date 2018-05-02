using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ConsignmentFile : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ConsignmentFile()
        {
        }

        public System.Guid ConsignmentFileID { get; set; }
        public Nullable<System.Guid> ConsignmentID { get; set; }
        public Nullable<System.Guid> FileID { get; set; }
       
        public virtual Consignment Consignment { get; set; }
        public virtual File File { get; set; }

        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
