using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class ApplicationUserLogin : PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Guid ApplicationUserLoginId { get; set; }
        public string LoginProvider { get; set; }
        public System.Guid ProviderKey { get; set; }
        public System.Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

       
    }
}
