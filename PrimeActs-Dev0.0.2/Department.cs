using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Department : PrimeActs.Domain.Abstract.AbstractObjectState, PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Department()
        {
            
            this.ConsignmentItems = new List<ConsignmentItem>();
            this.DepartmentStockLocations = new List<DepartmentStockLocation>();
            this.Printers = new List<Printer>();
            this.RoleContexts = new List<RoleContext>();
            this.SetupGlobals = new List<SetupGlobal>();
            this.TicketRanges = new List<TicketRange>();
            this.ApplicationUsers = new List<ApplicationUser>();
        }

        public System.Guid DepartmentID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public virtual Address Address { get; set; }
       
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<DepartmentStockLocation> DepartmentStockLocations { get; set; }
        public virtual ICollection<Printer> Printers { get; set; }
        public virtual ICollection<RoleContext> RoleContexts { get; set; }
        public virtual ICollection<SetupGlobal> SetupGlobals { get; set; }
        public virtual ICollection<TicketRange> TicketRanges { get; set; }
        public virtual ICollection<TicketItem> TicketItems { get; set; }
        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
