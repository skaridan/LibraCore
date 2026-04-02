using LibraCore.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> entity)
        {
            entity.HasData(SeedGenres());
        }

        private static IEnumerable<Genre> SeedGenres()
        {
            return new List<Genre>()
            {
                new Genre { Id = Guid.Parse("a1f8e8b0-0001-4b2f-8a0a-000000000001"), Name = "Fiction" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0002-4b2f-8a0a-000000000002"), Name = "Non-Fiction" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0003-4b2f-8a0a-000000000003"), Name = "Mystery" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0004-4b2f-8a0a-000000000004"), Name = "Thriller" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0005-4b2f-8a0a-000000000005"), Name = "Romance" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0006-4b2f-8a0a-000000000006"), Name = "Fantasy" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0007-4b2f-8a0a-000000000007"), Name = "Science Fiction" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0008-4b2f-8a0a-000000000008"), Name = "Biography" },
                new Genre { Id = Guid.Parse("a1f8e8b0-0009-4b2f-8a0a-000000000009"), Name = "History" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000A-4b2f-8a0a-00000000000A"), Name = "Children's" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000B-4b2f-8a0a-00000000000B"), Name = "Young Adult" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000C-4b2f-8a0a-00000000000C"), Name = "Horror" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000D-4b2f-8a0a-00000000000D"), Name = "Poetry" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000E-4b2f-8a0a-00000000000E"), Name = "Self-Help" },
                new Genre { Id = Guid.Parse("a1f8e8b0-000F-4b2f-8a0a-00000000000F"), Name = "Graphic Novel" }
            };
        }
    }
}
