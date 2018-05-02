using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class Department : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Department()
        {
            
            this.ConsignmentItems = new List<ConsignmentItem>();
            this.DepartmentStockLocations = new List<DepartmentStockLocation>();
          this.DepartmentPrinters = new List<DepartmentPrinter>();

          this.ProduceGroupDepartments = new List<ProduceGroupDepartment>();
            this.RoleContexts = new List<RoleContext>();
            this.SetupGlobals = new List<SetupGlobal>();
            this.TicketRanges = new List<TicketRange>();
           // this.ApplicationUsers = new List<ApplicationUser>();
           // this.ApplicationUserRoles = new List<ApplicationUserRole>();
        }

        public System.Guid DepartmentID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public Nullable<System.Guid> RebateNominalAccountID { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

        public virtual Address Address { get; set; }
       
        [ForeignKey("DepartmentID")]
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<DepartmentStockLocation> DepartmentStockLocations { get; set; }
        [ForeignKey("DepartmentID")]
        public virtual ICollection<ProduceGroupDepartment> ProduceGroupDepartments { get; set; }
       //public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<DepartmentPrinter> DepartmentPrinters { get; set; }
        public virtual ICollection<RoleContext> RoleContexts { get; set; }
        public virtual ICollection<SetupGlobal> SetupGlobals { get; set; }
        public virtual ICollection<TicketRange> TicketRanges { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
       // public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
      //  public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
