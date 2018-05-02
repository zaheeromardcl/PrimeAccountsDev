using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class CustomerContactDepartmentMap : EntityTypeConfiguration<PrimeActs.Domain.CustomerContactDepartment>
    {
        public CustomerContactDepartmentMap()
        {
            // Primary Key
            //this.HasKey(t => t.CustomerContactDepartmentID);

            //// Properties
            //// Table & Column Mappings
            //this.ToTable("tblCustomerContactDepartment");
            //this.Property(t => t.CustomerContactDepartmentID).HasColumnName("CustomerContactDepartmentID");
            //this.Property(t => t.CustomerContactID).HasColumnName("CustomerContactID");
            //this.Property(t => t.CustomerDepartmentID).HasColumnName("CustomerDepartmentID");

            //// Relationships
            //this.HasRequired(t => t.CustomerContact)
            //    .WithMany(t => t.CustomerContactDepartments)
            //    .HasForeignKey(d => d.CustomerContactID);
            //this.HasOptional(t => t.CustomerDepartment)
            //    .WithMany(t => t.CustomerContactDepartments)
            //    .HasForeignKey(d => d.CustomerDepartmentID);

        }
    }
}
