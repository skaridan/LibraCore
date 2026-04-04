using LibraCore.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasQueryFilter(b => b.IsDeleted == false);

            entity.HasData(SeedBooks());
        }

        private static IEnumerable<Book> SeedBooks()
        {
            return new List<Book>()
           {
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3001-4b2f-8a0a-000000000301"),
                    Title = "Pride and Prejudice",
                    Description = "A classic romantic novel about manners and marriage.",
                    ReleaseDate = new DateOnly(1813, 1, 28),
                    ImageUrl = "https://s3.amazonaws.com/nightjarprod/content/uploads/sites/192/2025/12/02152113/v5gShop7147X33ytbcC2u05KDuc.jpg",
                    Price = 9.99m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1001-4b2f-8a0a-000000000101"),
                    GenreId = Guid.Parse("a1f8e8b0-0005-4b2f-8a0a-000000000005")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3002-4b2f-8a0a-000000000302"),
                    Title = "Great Expectations",
                    Description = "The growth and personal development of an orphan named Pip.",
                    ReleaseDate = new DateOnly(1861, 8, 1),
                    ImageUrl = "https://m.media-amazon.com/images/I/815ZFQcdRcL._AC_UF1000,1000_QL80_.jpg",
                    Price = 11.50m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1002-4b2f-8a0a-000000000102"),
                    GenreId = Guid.Parse("a1f8e8b0-0001-4b2f-8a0a-000000000001")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3003-4b2f-8a0a-000000000303"),
                    Title = "Adventures of Huckleberry Finn",
                    Description = "A young boy's adventures along the Mississippi River.",
                    ReleaseDate = new DateOnly(1884, 12, 10),
                    ImageUrl = "https://m.media-amazon.com/images/I/91sBZnKzEfL._AC_UF1000,1000_QL80_.jpg",
                    Price = 10.00m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1003-4b2f-8a0a-000000000103"),
                    GenreId = Guid.Parse("a1f8e8b0-0001-4b2f-8a0a-000000000001")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3004-4b2f-8a0a-000000000304"),
                    Title = "1984",
                    Description = "A dystopian novel about surveillance and totalitarianism.",
                    ReleaseDate = new DateOnly(1949, 6, 8),
                    ImageUrl = "https://m.media-amazon.com/images/I/71wANojhEKL._AC_UF1000,1000_QL80_.jpg",
                    Price = 12.99m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1004-4b2f-8a0a-000000000104"),
                    GenreId = Guid.Parse("a1f8e8b0-0007-4b2f-8a0a-000000000007")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3005-4b2f-8a0a-000000000305"),
                    Title = "Mrs Dalloway",
                    Description = "A day in the life of Clarissa Dalloway in post‑war London.",
                    ReleaseDate = new DateOnly(1925, 5, 14),
                    ImageUrl = "https://www.book.store.bg/lrgimg/779190437/mrs-dalloway.jpg",
                    Price = 8.75m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1005-4b2f-8a0a-000000000105"),
                    GenreId = Guid.Parse("a1f8e8b0-0001-4b2f-8a0a-000000000001")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3006-4b2f-8a0a-000000000306"),
                    Title = "The Great Gatsby",
                    Description = "A story of the Jazz Age and the decline of the American Dream.",
                    ReleaseDate = new DateOnly(1925, 4, 10),
                    ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/7/7a/The_Great_Gatsby_Cover_1925_Retouched.jpg",
                    Price = 9.50m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1006-4b2f-8a0a-000000000106"),
                    GenreId = Guid.Parse("a1f8e8b0-0001-4b2f-8a0a-000000000001")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3007-4b2f-8a0a-000000000307"),
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "The first book in the Harry Potter series; a young wizard begins his journey.",
                    ReleaseDate = new DateOnly(1997, 6, 26),
                    ImageUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/9df78eab33525d08d6e5fb8d27136e95/h/a/a19438e622aa321a0e73f360f1f3f855/harry-potter-and-the-philosopher-s-stone-30.jpg",
                    Price = 14.99m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1007-4b2f-8a0a-000000000107"),
                    GenreId = Guid.Parse("a1f8e8b0-0006-4b2f-8a0a-000000000006")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3008-4b2f-8a0a-000000000308"),
                    Title = "The Shining",
                    Description = "A chilling horror novel set in an isolated hotel.",
                    ReleaseDate = new DateOnly(1977, 1, 28),
                    ImageUrl = "https://m.media-amazon.com/images/I/91U7HNa2NQL._AC_UF1000,1000_QL80_.jpg",
                    Price = 13.50m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1008-4b2f-8a0a-000000000108"),
                    GenreId = Guid.Parse("a1f8e8b0-000c-4b2f-8a0a-00000000000c")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-3009-4b2f-8a0a-000000000309"),
                    Title = "Murder on the Orient Express",
                    Description = "A Hercule Poirot mystery on a snowbound train.",
                    ReleaseDate = new DateOnly(1934, 1, 1),
                    ImageUrl = "https://m.media-amazon.com/images/I/81aY+Fk-g8L._UF1000,1000_QL80_.jpg",
                    Price = 10.99m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-1009-4b2f-8a0a-000000000109"),
                    GenreId = Guid.Parse("a1f8e8b0-0003-4b2f-8a0a-000000000003")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-300A-4b2f-8a0a-00000000030A"),
                    Title = "The Hobbit",
                    Description = "A fantasy adventure that precedes the Lord of the Rings.",
                    ReleaseDate = new DateOnly(1937, 9, 21),
                    ImageUrl = "https://m.media-amazon.com/images/I/81uEDUfKBZL.jpg",
                    Price = 12.00m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-100a-4b2f-8a0a-00000000010a"),
                    GenreId = Guid.Parse("a1f8e8b0-0006-4b2f-8a0a-000000000006")
                },
                new Book
                {
                    Id = Guid.Parse("d1b8e8b0-300B-4b2f-8a0a-00000000030B"),
                    Title = "War and Peace",
                    Description = "An epic novel following several families during the Napoleonic era.",
                    ReleaseDate = new DateOnly(1869, 1, 1),
                    ImageUrl = "https://m.media-amazon.com/images/I/81W6BFaJJWL._AC_UF1000,1000_QL80_.jpg",
                    Price = 15.00m,
                    IsDeleted = false,
                    AuthorId = Guid.Parse("b1d8e8b0-100c-4b2f-8a0a-00000000010c"),
                    GenreId = Guid.Parse("a1f8e8b0-0009-4b2f-8a0a-000000000009")
                }
            };
        }
    }
}
