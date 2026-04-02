using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityRole<Guid>> entity)
        {
            entity.HasData(SeedRoles());
        }

        private static IEnumerable<IdentityRole<Guid>> SeedRoles()
        {
            return new List<IdentityRole<Guid>>()
           {
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("e1d8e8b0-4001-4b2f-8a0a-000000000401"),
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "CONC-ROLE-0001"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("e1d8e8b0-4002-4b2f-8a0a-000000000402"),
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "CONC-ROLE-0002"
                }
            };
        }
    }
}
