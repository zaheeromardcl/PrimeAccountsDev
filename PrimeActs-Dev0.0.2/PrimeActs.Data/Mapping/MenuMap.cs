using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class MenuMap : EntityTypeConfiguration<PrimeActs.Domain.Menu>
    {
        public MenuMap()
        {
            // Primary Key
            this.HasKey(t => t.MenuID);

            // Properties
            this.Property(t => t.MenuDescription)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MenuLinkTo)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);

            // Table & Column Mappings
            this.ToTable("tblMenu");
            this.Property(t => t.MenuID).HasColumnName("MenuID");
            this.Property(t => t.ParentID).HasColumnName("ParentID");
            this.Property(t => t.MenuDescription).HasColumnName("MenuDescription");
            this.Property(t => t.MenuLinkTo).HasColumnName("MenuLinkTo");
            this.Property(t => t.IsCurrent).HasColumnName("IsCurrent");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}
