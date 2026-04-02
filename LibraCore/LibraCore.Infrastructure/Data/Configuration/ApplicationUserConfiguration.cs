using LibraCore.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.HasData(SeedUserRoles());
        }

        private static IEnumerable<ApplicationUser> SeedUserRoles()
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            var admin = new ApplicationUser
            {
                Id = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                UserName = "admin@libra.local",
                NormalizedUserName = "ADMIN@LIBRA.LOCAL",
                Email = "admin@libra.local",
                NormalizedEmail = "ADMIN@LIBRA.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "A1D8E8B0-2001-4B2F-8A0A-000000000201",
                ConcurrencyStamp = "CONC-00000001",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

            var user = new ApplicationUser
            {
                Id = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                UserName = "user@libra.local",
                NormalizedUserName = "USER@LIBRA.LOCAL",
                Email = "user@libra.local",
                NormalizedEmail = "USER@LIBRA.LOCAL",
                EmailConfirmed = true,
                SecurityStamp = "A1D8E8B0-2002-4B2F-8A0A-000000000202",
                ConcurrencyStamp = "CONC-00000002",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0
            };

            user.PasswordHash = hasher.HashPassword(user, "User123!");

            return new List<ApplicationUser>() { admin, user };
        }
    }
}
