using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ConsignmentMap : EntityTypeConfiguration<PrimeActs.Domain.Consignment>
    {
        public ConsignmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ConsignmentID);

            // Properties
            this.Property(t => t.ConsignmentDescription)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.ConsignmentReference)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.ServerCode)
                .IsRequired()
                .HasMaxLength(1);

            this.Property(t => t.Vehicle)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.VehicleDetail)
                .IsRequired()
                .HasMaxLength(1000);

            this.Property(t => t.SupplierReference)
                .HasMaxLength(20);

            this.Property(t => t.String1)
                .HasMaxLength(1000);

            this.Property(t => t.String2)
                .HasMaxLength(1000);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tblConsignment");
            this.Property(t => t.ConsignmentID).HasColumnName("ConsignmentID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.ConsignmentDescription).HasColumnName("ConsignmentDescription");
            this.Property(t => t.ConsignmentReference).HasColumnName("ConsignmentReference");
            this.Property(t => t.ServerCode).HasColumnName("ServerCode");
            this.Property(t => t.PortID).HasColumnName("PortID");
            this.Property(t => t.PurchaseTypeID).HasColumnName("PurchaseTypeID");
            this.Property(t => t.Handling).HasColumnName("Handling");
            this.Property(t => t.Commission).HasColumnName("Commission");
            this.Property(t => t.ShowVehicleOnInvoice).HasColumnName("ShowVehicleOnInvoice");
            this.Property(t => t.Vehicle).HasColumnName("Vehicle");
            this.Property(t => t.VehicleDetail).HasColumnName("VehicleDetail");
            this.Property(t => t.SupplierDepartmentID).HasColumnName("SupplierDepartmentID");
            this.Property(t => t.SupplierReference).HasColumnName("SupplierReference");
            //this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.FK1).HasColumnName("FK1");
            this.Property(t => t.FK2).HasColumnName("FK2");
            this.Property(t => t.Bit1).HasColumnName("Bit1");
            this.Property(t => t.Bit2).HasColumnName("Bit2");
            this.Property(t => t.String1).HasColumnName("String1");
            this.Property(t => t.String2).HasColumnName("String2");
            this.Property(t => t.Numeric1).HasColumnName("Numeric1");
            this.Property(t => t.Numeric2).HasColumnName("Numeric2");
            this.Property(t => t.Int1).HasColumnName("Int1");
            this.Property(t => t.Int2).HasColumnName("Int2");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.DespatchLocationID).HasColumnName("DespatchLocationID");
            this.Property(t => t.DespatchDate).HasColumnName("DespatchDate");
            this.Property(t => t.NoteID).HasColumnName("NoteID");
            
            this.Property(t => t.ContractDate).HasColumnName("ContractDate");
            //this.Property(t => t.IsSaved).HasColumnName("IsSaved");
            this.Property(t => t.IsHistory).HasColumnName("IsHistory");

            this.Property(t => t.WhenActivated).HasColumnName("WhenActivated");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            
            // Relationships
            this.HasOptional(t => t.DespatchLocation)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.DespatchLocationID);
            this.HasOptional(t => t.Division)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.DivisionID);
            this.HasOptional(t => t.Note)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.NoteID);
            this.HasOptional(t => t.Port)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.PortID);
            this.HasRequired(t => t.PurchaseType)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.PurchaseTypeID);
            this.HasRequired(t => t.SupplierDepartment)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.SupplierDepartmentID);
            this.HasOptional(t => t.CreatedByUser)
                .WithMany(t => t.Consignments)
                .HasForeignKey(d => d.CreatedBy);
        }
    }
}
