using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class TabPanelMap : EntityTypeConfiguration<PrimeActs.Domain.UserTabPanel>
    {
        public TabPanelMap()
        {
            // Primary Key
            this.HasKey(t => t.PanelID);

           //this.Property(t => t.UpdatedBy)
           //     .HasMaxLength(25);

           // this.Property(t => t.CreatedBy)
           //     .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("tblUserTabPanel");
            this.Property(t => t.HoldingDiv).HasColumnName("HoldingDiv");
            this.Property(t => t.IsSelected).HasColumnName("IsSelected");
            this.Property(t => t.JsonData).HasColumnName("JsonData");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.PanelID).HasColumnName("PanelID");
            this.Property(t => t.UserID).HasColumnName("UserID");
            this.Property(t => t.ContentType).HasColumnName("ContentType");
            this.Property(t => t.UriParam).HasColumnName("UriParam");
            
            //this.Property(t => t.UpdatedBy).HasColumnName("UpdatedBy");
            //this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            //this.Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            //this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            //this.Property(t => t.IsActive).HasColumnName("IsActive");
           
            // Relationships           

        }
    }
}
