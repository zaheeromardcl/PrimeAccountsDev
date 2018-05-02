using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationUserClaimMap : EntityTypeConfiguration<PrimeActs.Domain.ApplicationUserClaim>
    {
        public ApplicationUserClaimMap()
        {
            // Primary Key
            this.HasKey(t => t.ClaimID);

            // Properties
            // Table & Column Mappings
            this.ToTable("tblApplicationUserClaim");
            this.Property(t => t.ClaimID).HasColumnName("ApplicationUserClaimId");
            this.Property(t => t.UserID).HasColumnName("ApplicationUserId");
            this.Property(t => t.ClaimType).HasColumnName("ClaimType");
            this.Property(t => t.ClaimValue).HasColumnName("ClaimValue");

            // Relationships
            //this.HasRequired(t => t.AspNetUser)
            //    .WithMany(t => t.AspNetUserClaims)
            //    .HasForeignKey(d => d.UserId);

        }
    }
}
