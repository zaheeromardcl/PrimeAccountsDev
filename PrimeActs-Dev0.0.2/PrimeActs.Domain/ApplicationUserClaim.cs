using System;
using System.Collections.Generic;

using PrimeActs.Infrastructure.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;
namespace PrimeActs.Domain
{
    public partial class ApplicationUserClaim :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public ApplicationUserClaim()
        {
            
        }

        public System.Guid ClaimID { get; set; }
        public System.Guid UserID { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }


        [NotMapped]
        public ObjectState ObjectState { get; set; }
      
     
    }
}
