using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace PrimeActs.Data.Mapping
{
    public class DepartmentPrinterMap : EntityTypeConfiguration<PrimeActs.Domain.DepartmentPrinter>
    {
        public DepartmentPrinterMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentPrinterID);
            
            // Properties
            this.Property(t => t.DepartmentID);
            

            this.Property(t => t.PrinterID);
            this.Property(t => t.Preference);
            //this.Property(t => t.PrinterStationeryID);
            this.Property(t => t.DefaultPrinterStationeryID);
               

            // Table & Column Mappings
            this.ToTable("tblDepartmentPrinter");
            this.Property(t => t.DepartmentPrinterID).HasColumnName("DepartmentPrinterID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.PrinterID).HasColumnName("PrinterID");
            this.Property(t => t.Preference).HasColumnName("Preference");
            //this.Property(t => t.PrinterStationeryID).HasColumnName("PrinterStationeryID");
            this.Property(t => t.DefaultPrinterStationeryID).HasColumnName("DefaultPrinterStationeryID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
        }
    }
}
