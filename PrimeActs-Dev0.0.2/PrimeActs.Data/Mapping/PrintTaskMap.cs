using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Data.Mapping
{
    class PrintTaskMap : EntityTypeConfiguration<PrimeActs.Domain.PrintTask>
    {
        public PrintTaskMap()
        {
            // Primary Key
            this.HasKey(t => t.PrintTaskID);
            // Properties
            this.Property(t => t.PrintTaskName)
               .IsRequired()
               .HasMaxLength(50);
            this.Property(t => t.HasColour);
            this.Property(t => t.RequireColour);
            this.Property(t => t.HasRaw);
            this.Property(t => t.RequireRaw);
            this.Property(t => t.HasTractor);
            this.Property(t => t.RequireTractor);
                
            //Table & Column Mappings
            this.ToTable("tblPrintTask");
            this.Property(t => t.PrintTaskID).HasColumnName("PrintTaskID");
            this.Property(t => t.PrintTaskName).HasColumnName("PrintTaskName");
            this.Property(t => t.HasColour).HasColumnName("HasColour");
            this.Property(t => t.RequireColour).HasColumnName("RequireColour"); 
            this.Property(t => t.HasRaw).HasColumnName("HasRaw");
            this.Property(t => t.RequireRaw).HasColumnName("RequireRaw"); 
            this.Property(t => t.HasTractor).HasColumnName("HasTractor"); 
            this.Property(t => t.RequireTractor).HasColumnName("RequireTractor"); 

            //Relationships
            
            //1 print task has many departmentprinttasks
            //this.HasOptional(t => t.DepartmentPrintTasks)
            //    .WithMany()
            //    .HasForeignKey(t => t.PrintTaskID);

            // this.HasOptional(t => t.DepartmentPrintTasks);
            //this.HasOptional(t => t.DepartmentPrintTasks)
            //    .WithMany(t => t.PrintTasks)
            //    .HasForeignKey(d => d.PrintTaskID);
            //this.HasOptional(t => t.DepartmentPrintTasks).WithMany(t => t.)

            //this.HasOptional(t => t.DepartmentPrintTasks).WithMany( c => c.)
            //Many to many relationship
            //this.HasRequired(t => t.DepartmentPrintTasks)
            //     .WithMany(t => t.PrintTasks)
            //     .HasForeignKey(d => d.DepartmentPrintTaskID);

        }
    }
}
