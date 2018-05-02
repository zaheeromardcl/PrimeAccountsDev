using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ProduceGroupMap : EntityTypeConfiguration<PrimeActs.Domain.ProduceGroup>
    {
        public ProduceGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.ProduceGroupID);

            // Properties
         
            // Table & Column Mappings
            this.ToTable("tblProduceGroup");
            this.Property(t => t.ProduceGroupID).HasColumnName("ProduceGroupID");
            
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.ProduceGroupCode).HasColumnName("ProduceGroupCode");
            this.Property(t => t.ProduceGroupName).HasColumnName("ProduceGroupName");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
      

        }
    }
}
