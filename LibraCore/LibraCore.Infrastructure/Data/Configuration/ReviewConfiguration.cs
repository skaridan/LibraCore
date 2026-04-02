using LibraCore.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> entity)
        {
            entity.HasData(SeedReviews());
        }

        private static IEnumerable<Review> SeedReviews()
        {
            return new List<Review>()
           {
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5001-4b2f-8a0a-000000000501"),
                    Rating = 5,
                    Comment = "A timeless classic — thoroughly enjoyed it.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3001-4b2f-8a0a-000000000301"),
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201")
                },
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5002-4b2f-8a0a-000000000502"),
                    Rating = 4,
                    Comment = "Well written with memorable characters.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3002-4b2f-8a0a-000000000302"),
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202")
                },
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5003-4b2f-8a0a-000000000503"),
                    Rating = 5,
                    Comment = "A brilliant and thought‑provoking dystopia.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3004-4b2f-8a0a-000000000304"),
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201")
                },
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5004-4b2f-8a0a-000000000504"),
                    Rating = 3,
                    Comment = "Good ideas but the pacing felt slow to me.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3006-4b2f-8a0a-000000000306"),
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202")
                },
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5005-4b2f-8a0a-000000000505"),
                    Rating = 5,
                    Comment = "Magical, enchanting — perfect for all ages.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3007-4b2f-8a0a-000000000307"),
                    UserId = Guid.Parse("c1d8e8b0-2002-4b2f-8a0a-000000000202")
                },
                new Review
                {
                    Id = Guid.Parse("f1d8e8b0-5006-4b2f-8a0a-000000000506"),
                    Rating = 4,
                    Comment = "Suspenseful and eerie — kept me up at night.",
                    IsDeleted = false,
                    BookId = Guid.Parse("d1b8e8b0-3008-4b2f-8a0a-000000000308"),
                    UserId = Guid.Parse("c1d8e8b0-2001-4b2f-8a0a-000000000201")
                }
            };
        }
    }
}
