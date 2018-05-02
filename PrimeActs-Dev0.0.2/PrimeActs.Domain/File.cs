using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class File :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public File()
        {
           // this.AuditFiles = new List<AuditFile>();
        // this.ConsignmentFiles = new List<ConsignmentFile>();
        }

        public System.Guid FileID { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileContent { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
        //public System.DateTime CreatedDate { get; set; }
    //    public virtual ICollection<AuditFile> AuditFiles { get; set; }
       // public virtual ICollection<ConsignmentFile> ConsignmentFiles { get; set; }
    }
}
