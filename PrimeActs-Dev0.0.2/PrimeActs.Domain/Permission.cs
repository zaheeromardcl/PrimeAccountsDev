using PrimeActs.Infrastructure.BaseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class Permission :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        private string _name;

        public Permission()
        {
            //this.ApplicationRoles = new List<ApplicationRole>();
        }

        [Key]
        public System.Guid PermissionID { get; set; }
        
        //public System.Guid PermissionGroupId { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //obsolete
        [NotMapped]
        public string PermissionName {
            get
            {
                if (String.IsNullOrEmpty(_name))
                    _name = PermissionController + " / " + PermissionAction;
                return _name;
            }
            set { _name = value; }
        }

        public string PermissionController { get; set; }
        public string PermissionAction { get; set; }
        public string PermissionDescription { get; set; }
        //public int BitNumber { get; set; }
        //public virtual ICollection<ApplicationRole> ApplicationRoles { get; set; }
        [NotMapped]
        public ObjectState ObjectState { get; set; }

    }
}