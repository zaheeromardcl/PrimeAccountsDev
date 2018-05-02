using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    class AuditMap : EntityTypeConfiguration<PrimeActs.Domain.Audit>
    {
        public AuditMap()
        {
            // Primary Key
            this.HasKey(t => t.AuditID);

            // Properties
            this.Property(t => t.JsonDataBefore);

            this.Property(t => t.JsonDataAfter);

            this.Property(t => t.ContentType).IsRequired();

            this.Property(t => t.UserID).IsRequired();

            this.Property(t => t.DivisionID);
            this.Property(t => t.DepartmentID);

            this.Property(t => t.EditDate)
                .IsRequired();
           
            this.Property(t => t.ReferenceID);
            this.Property(t => t.Reference);            

            // Table & Column Mappings
            this.ToTable("tblAudit");
            this.Property(t => t.AuditID).HasColumnName("AuditID");
            this.Property(t => t.JsonDataBefore).HasColumnName("JsonDataBefore");
            this.Property(t => t.JsonDataAfter).HasColumnName("JsonDataAfter");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.CompanyID).HasColumnName("CompanyID");
            this.Property(t => t.DivisionID).HasColumnName("DivisionID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.EditDate).HasColumnName("EditDate");
            this.Property(t => t.ReferenceID).HasColumnName("ReferenceID");
            this.Property(t => t.Reference).HasColumnName("Reference");            
        }
    }
}
