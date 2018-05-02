using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Version :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public decimal MasterVersion { get; set; }
        public decimal LocalVersion { get; set; }
    
[System.ComponentModel.DataAnnotations.Schema.NotMapped]
public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
