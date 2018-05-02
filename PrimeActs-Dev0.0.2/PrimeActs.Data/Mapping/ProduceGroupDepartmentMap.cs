using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ProduceGroupDepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.ProduceGroupDepartment>
    {
        public ProduceGroupDepartmentMap()
        {
            // Primary Key
            this.HasKey(t => t.ProduceGroupDepartmentID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblProduceGroupDepartment");
            this.Property(t => t.ProduceGroupID).HasColumnName("ProduceGroupID");
            this.Property(t => t.DepartmentID).HasColumnName("DepartmentID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");


            //relationships
            
            

        }
    }
}
