using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

//AK: preventing duplication removing namespaces
//using System;
//using System.Collections.Generic;
//using System.Data.Entity.ModelConfiguration;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using PrimeActs.Domain;

namespace PrimeActs.Data.Mapping
{
    public class DepartmentPrintTaskMap : EntityTypeConfiguration<PrimeActs.Domain.DepartmentPrintTask>
    {
        public DepartmentPrintTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.DepartmentPrintTaskID);
            // Properties
            this.Property(t => t.DepartmentPrinterID)
                .IsRequired();
            this.Property(t => t.PrintTaskID)
                .IsRequired();
            this.Property(t => t.Preference);
            this.Property(t => t.PrinterStationeryID);

            // Table & Column Mappings
            this.ToTable("tblDepartmentPrintTask");
            this.Property(t => t.DepartmentPrintTaskID).HasColumnName("DepartmentPrintTaskID");
            this.Property(t => t.DepartmentPrinterID).HasColumnName("DepartmentPrinterID");
            this.Property(t => t.PrintTaskID).HasColumnName("PrintTaskID");
            this.Property(t => t.Preference).HasColumnName("Preference");
            this.Property(t => t.PrinterStationeryID).HasColumnName("PrinterStationeryID");

            // Primary Key

            ////Relationships
            //1 Department print tasks have 1 print task
            //this.HasOptional(t => t.PrintTasks).WithMany(c => c.)
            //             .WithMany()
            //             .HasForeignKey(t => t.PrintTaskID);
            //this.HasRequired(t => t.PrintTasks)
            //    .WithMany(t => t.DepartmentPrintTasks)
            //    .HasForeignKey((c => c.PrintTaskID));



            // this.HasRequired(t => t.PrintTasks).WithMany( c => c.)
            //this.HasRequired(t => t.Printers).WithMany(t => t.)
            //AK: to be recommented.
            //this.HasMany(t => t.PrintTasks).WithMany(c => c.DepartmentPrintTasks)
            //   .Map(t => t.ToTable("tblDepartmentPrintTask")
            //       .MapLeftKey("PrintTaskID")
            //       .MapRightKey("DepartmentPrinterID"));
        }

    }
}
