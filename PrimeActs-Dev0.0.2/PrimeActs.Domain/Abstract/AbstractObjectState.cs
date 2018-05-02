using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain.Abstract
{
    public abstract class AbstractObjectState
    {
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        // public Guid AspNetUser_Id { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}