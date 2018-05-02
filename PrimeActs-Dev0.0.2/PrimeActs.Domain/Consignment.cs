using System;
using System.Collections.Generic;

namespace PrimeActs.Domain
{
    public partial class Consignment :  PrimeActs.Infrastructure.BaseEntities.IObjectState
    {
        public Consignment()
        {
          
            this.ConsignmentFiles = new List<ConsignmentFile>();
            this.ConsignmentItems = new List<ConsignmentItem>();
            //this.SupplierItems = new List<SupplierItem>(); //////////////
        }

        public System.Guid ConsignmentID { get; set; }
        public Nullable<System.Guid> DivisionID { get; set; }
        public string ConsignmentDescription { get; set; }
        public string ConsignmentReference { get; set; }
        public string ServerCode { get; set; }
        public Nullable<System.Guid> PortID { get; set; }
        public System.Guid PurchaseTypeID { get; set; }
        public decimal Handling { get; set; }
        public decimal Commission { get; set; }
        public bool ShowVehicleOnInvoice { get; set; }
        public string Vehicle { get; set; }
        public string VehicleDetail { get; set; }
        public System.Guid SupplierDepartmentID { get; set; }
        public string SupplierReference { get; set; }
        public Nullable<System.Guid> FK1 { get; set; }
        public Nullable<System.Guid> FK2 { get; set; }
        public Nullable<bool> Bit1 { get; set; }
        public Nullable<bool> Bit2 { get; set; }
        public string String1 { get; set; }
        public string String2 { get; set; }
        public Nullable<decimal> Numeric1 { get; set; }
        public Nullable<decimal> Numeric2 { get; set; }
        public Nullable<int> Int1 { get; set; }
        public Nullable<int> Int2 { get; set; }
        
        public Nullable<System.Guid> DespatchLocationID { get; set; }
        public System.DateTime DespatchDate { get; set; }
        public Nullable<System.Guid> NoteID { get; set; }
        
        public Nullable<System.DateTime> ContractDate { get; set; }
     //  public bool IsSaved { get; set; }
        public bool IsHistory { get; set; }
        public Nullable<System.DateTime> WhenActivated { get; set; }
        public bool IsDeleted { get; set; }
      
        public virtual DespatchLocation DespatchLocation { get; set; }
        public virtual Division Division { get; set; }
        public virtual Note Note { get; set; }
        public virtual Port Port { get; set; }
        public virtual PurchaseType PurchaseType { get; set; }
        public virtual SupplierDepartment SupplierDepartment { get; set; }
        public virtual ICollection<ConsignmentFile> ConsignmentFiles { get; set; }
        public virtual ICollection<ConsignmentItem> ConsignmentItems { get; set; }
        //public virtual ICollection<SupplierItem> SupplierItems { get; set; } ///////////
        public Nullable<System.Guid> UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Nullable<System.Guid> CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }

        public ApplicationUser CreatedByUser { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public PrimeActs.Infrastructure.BaseEntities.ObjectState ObjectState { get; set; }
    }
}
