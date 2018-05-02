using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PrimeActs.Data.Mapping
{
    public class ApplicationUserMap : EntityTypeConfiguration<PrimeActs.Domain.ApplicationUser>
    {
        public ApplicationUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .HasMaxLength(256);

            this.Property(t => t.UserName)
                .IsRequired()
                .HasMaxLength(256);

            // Table & Column Mappings
            this.ToTable("tblUser");
            this.Property(t => t.Id).HasColumnName("UserID");
            this.Property(t => t.Firstname).HasColumnName("Firstname");
            this.Property(t => t.Lastname).HasColumnName("Lastname");
            this.Property(t => t.Nickname).HasColumnName("Nickname");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.LastLoggedOn).HasColumnName("LastLoggedOn");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.EmailConfirmed).HasColumnName("EmailConfirmed");
            this.Property(t => t.PasswordHash).HasColumnName("PasswordHash");
            this.Property(t => t.AdminPasswordHash).HasColumnName("AdminPasswordHash");
            this.Property(t => t.SecurityStamp).HasColumnName("SecurityStamp");
            this.Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            this.Property(t => t.PhoneNumberConfirmed).HasColumnName("PhoneNumberConfirmed");
            this.Property(t => t.TwoFactorEnabled).HasColumnName("TwoFactorEnabled");
            this.Property(t => t.LockoutEndDateUtc).HasColumnName("LockoutEndDateUtc");
            this.Property(t => t.LockoutEnabled).HasColumnName("LockoutEnabled");
            this.Property(t => t.AccessFailedCount).HasColumnName("AccessFailedCount");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.DepartmentId).HasColumnName("DefaultDepartmentID");
            this.Property(t => t.CompanyId).HasColumnName("DefaultCompanyID");
            this.Property(t => t.DivisionId).HasColumnName("DefaultDivisionID");
            this.Property(t => t.UpdatedBy).HasColumnName("UpdatedByUserID");
            this.Property(t => t.CreatedBy).HasColumnName("CreatedByUserID");
            this.Property(t => t.UpdatedDate).HasColumnName("UpdatedDate");
            this.Property(t => t.CreatedDate).HasColumnName("CreatedDate");

            // Relationships
            //this.HasMany(t => t.ApplicationRoles).WithMany(c => c.ApplicationUsers)
            //    .Map(t => t.ToTable("tblApplicationUserRole")
            //        .MapLeftKey("ApplicationUserId")
            //        .MapRightKey("ApplicationRoleID"));

            //this.HasOptional(t => t.Department)
            //    .WithMany(d => d.ApplicationUsers)
            //    .HasForeignKey(t => t.DepartmentId);

            //this.HasOptional(t => t.Company)
            //    .WithMany(c => c.ApplicationUsers)
            //    .HasForeignKey(t => t.CompanyId);

            //this.HasOptional(t => t.Division)
            //    .WithMany(c => c.ApplicationUsers)
            //    .HasForeignKey(t => t.DivisionId);
        }
    }
}
