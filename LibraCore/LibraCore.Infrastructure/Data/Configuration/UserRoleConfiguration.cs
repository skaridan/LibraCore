using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> entity)
        {
            entity.HasData(SeedUserRoles());
        }

        private static IEnumerable<IdentityUserRole<Guid>> SeedUserRoles()
        {
            return new List<IdentityUserRole<Guid>>()
            {
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                    RoleId = Guid.Parse("e1d8e8b0-4001-4b2f-8a0a-000000000401")
                },
                new IdentityUserRole<Guid>
                {
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                    RoleId = Guid.Parse("e1d8e8b0-4002-4b2f-8a0a-000000000402")
                }
            };
        }
    }
}
