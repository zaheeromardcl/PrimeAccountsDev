using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Repository :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public decimal VersionID { get; set; }
        public byte[] CodeZipFile { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
}
}
