using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PrimeActs.Domain
{
    public partial class Division :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Division()
        {
           
            this.BatchNumberLogs = new List<BatchNumberLog>();
            this.Consignments = new List<Consignment>();
            this.Departments = new List<Department>();
            this.MasterGroups = new List<MasterGroup>();
            
           // this.ProduceGroups = new List<ProduceGroup>();
            this.RoleContexts = new List<RoleContext>();
            this.SalesInvoices = new List<SalesInvoice>();
            this.SetupGlobals = new List<SetupGlobal>();
            this.Tickets = new List<Ticket>();
           // this.ApplicationUsers = new List<ApplicationUser>();
           // this.ApplicationUserRoles = new List<ApplicationUserRole>();
        }

        public System.Guid DivisionID { get; set; }
        public System.Guid CompanyID { get; set; }
        public string DivisionName { get; set; }
        public Nullable<System.Guid> AddressID { get; set; }
        public Nullable<System.Guid> TransactionTaxLocationID { get; set; }
        public Nullable<System.Guid> InvoiceNominalAccountID { get; set; }
        
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        [NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }

        
        public virtual Address Address { get; set; }
     
        public virtual ICollection<BatchNumberLog> BatchNumberLogs { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
        public virtual ICollection<Department> Departments { get; set; }        
        public virtual ICollection<MasterGroup> MasterGroups { get; set; }
        //public virtual ICollection<ProduceGroup> ProduceGroups { get; set; }
        public virtual ICollection<RoleContext> RoleContexts { get; set; }
        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SetupGlobal> SetupGlobals { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        //public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
       // public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; }
    }
}
