using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ProduceMap : EntityTypeConfiguration<PrimeActs.Domain.Produce>
    {
        public ProduceMap()
        {
            // Primary Key
            this.HasKey(t => t.ProduceID);

            // Properties
            this.Property(t => t.ProduceCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.ProduceName)
                .IsRequired()
                .HasMaxLength(50);


            this.Property(t => t.UpdatedBy);

            this.Property(t => t.CreatedBy);
            // Table & Column Mappings
            this.ToTable("tblProduce");
            this.Property(t => t.ProduceID).HasColumnName("ProduceID");
            
            this.Property(t => t.ProduceCode).HasColumnName("ProduceCode");
            this.Property(t => t.ProduceName).HasColumnName("ProduceName");
            this.Property(t => t.ProduceGroupID).HasColumnName("ProduceGroupID");
            this.Property(t => t.MasterGroupID).HasColumnName("MasterGroupID");
            
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
   
            this.HasRequired(t => t.MasterGroup)
                .WithMany(t => t.Produces)
                .HasForeignKey(d => d.MasterGroupID);
            this.HasRequired(t => t.ProduceGroup)
                .WithMany(t => t.Produces)
                .HasForeignKey(d => d.ProduceGroupID);


        }
    }
}
