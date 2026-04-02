using LibraCore.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class UserBookConfiguration : IEntityTypeConfiguration<UserBook>
    {
        public void Configure(EntityTypeBuilder<UserBook> entity)
        {
            entity.HasData(SeedUserBooks());
        }

        private static IEnumerable<UserBook> SeedUserBooks()
        {
            return new List<UserBook>()
            {
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                    BookId = Guid.Parse("d1b8e8b0-3001-4b2f-8a0a-000000000301") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                    BookId = Guid.Parse("d1b8e8b0-3004-4b2f-8a0a-000000000304") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                    BookId = Guid.Parse("d1b8e8b0-3006-4b2f-8a0a-000000000306") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                    BookId = Guid.Parse("d1b8e8b0-3007-4b2f-8a0a-000000000307") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                    BookId = Guid.Parse("d1b8e8b0-300a-4b2f-8a0a-00000000030a") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                    BookId = Guid.Parse("d1b8e8b0-3009-4b2f-8a0a-000000000309") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201"),
                    BookId = Guid.Parse("d1b8e8b0-3002-4b2f-8a0a-000000000302") 
                },
                new UserBook
                {
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202"),
                    BookId = Guid.Parse("d1b8e8b0-3002-4b2f-8a0a-000000000302") 
                }
            };
        }
    }
}
