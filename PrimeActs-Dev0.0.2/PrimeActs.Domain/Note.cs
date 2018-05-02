using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Note :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Note()
        {
            
            this.Consignments = new List<Consignment>();
            this.ConsignmentItems = new List<ConsignmentItem>();
            this.ConsignmentItemArrivals = new List<ConsignmentItemArrival>();
            this.Contacts = new List<Contact>();
            this.Customers = new List<Customer>();
            this.CustomerDepartments = new List<CustomerDepartment>();
            this.CustomerLocations = new List<CustomerLocation>();
         
            this.PurchaseInvoices = new List<PurchaseInvoice>();
          //  this.PurchaseLedgerEntries = new List<PurchaseLedgerEntry>();
        
            this.SalesInvoices = new List<SalesInvoice>();
            this.SalesLedgerEntries = new List<SalesLedgerEntry>();
            this.Suppliers = new List<Supplier>();
            this.SupplierDepartments = new List<SupplierDepartment>();
            this.SupplierLocations = new List<SupplierLocation>();
            this.Tickets = new List<Ticket>();
        }

        public System.Guid NoteID { get; set; }
        public string NoteDescription { get; set; }
        public string NoteText { get; set; }
        public virtual ICollection<Consignment> Consignments { get; set; }
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        public virtual ICollection<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<CustomerDepartment> CustomerDepartments { get; set; }
        public virtual ICollection<CustomerLocation> CustomerLocations { get; set; }
   
        public virtual ICollection<PurchaseInvoice> PurchaseInvoices { get; set; }
    //    public virtual ICollection<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }

        public virtual ICollection<SalesInvoice> SalesInvoices { get; set; }
        public virtual ICollection<SalesLedgerEntry> SalesLedgerEntries { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<SupplierDepartment> SupplierDepartments { get; set; }
        public virtual ICollection<SupplierLocation> SupplierLocations { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
