using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationUserLoginMap : EntityTypeConfiguration<PrimeActs.Domain.ApplicationUserLogin>
    {
        public ApplicationUserLoginMap()
        {
            // Primary Key
            this.HasKey(t => t.ApplicationUserLoginId);

            // Properties
            this.Property(t => t.LoginProvider)
                .IsRequired()
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("tblApplicationUserLogin");
            this.Property(t => t.ApplicationUserLoginId).HasColumnName("ApplicationUserLoginId");
            this.Property(t => t.LoginProvider).HasColumnName("LoginProvider");
            this.Property(t => t.ProviderKey).HasColumnName("ProviderKey");
            this.Property(t => t.UserID).HasColumnName("ApplicationUserId");

            // Relationships
            //this.HasRequired(t => t.AspNetUser)
            //    .WithMany(t => t.AspNetUserLogins)
            //    .HasForeignKey(d => d.UserId);

        }
    }
}
