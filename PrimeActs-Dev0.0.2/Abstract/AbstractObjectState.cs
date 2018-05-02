using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain.Abstract
{
    public abstract class AbstractObjectState
    {
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        [NotMapped]
        public Guid AspNetUser_Id { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }
    }
}
