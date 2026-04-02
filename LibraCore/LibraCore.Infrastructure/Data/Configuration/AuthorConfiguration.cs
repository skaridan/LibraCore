using LibraCore.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraCore.Infrastructure.Data.Configuration
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> entity)
        {
            entity.HasData(SeedAuthors());
        }

        private static IEnumerable<Author> SeedAuthors()
        {
            return new List<Author>()
            {
                new Author { Id = Guid.Parse("b1d8e8b0-1001-4b2f-8a0a-000000000101"), Name = "Jane Austen" },
                new Author { Id = Guid.Parse("b1d8e8b0-1002-4b2f-8a0a-000000000102"), Name = "Charles Dickens" },
                new Author { Id = Guid.Parse("b1d8e8b0-1003-4b2f-8a0a-000000000103"), Name = "Mark Twain" },
                new Author { Id = Guid.Parse("b1d8e8b0-1004-4b2f-8a0a-000000000104"), Name = "George Orwell" },
                new Author { Id = Guid.Parse("b1d8e8b0-1005-4b2f-8a0a-000000000105"), Name = "Virginia Woolf" },
                new Author { Id = Guid.Parse("b1d8e8b0-1006-4b2f-8a0a-000000000106"), Name = "F. Scott Fitzgerald" },
                new Author { Id = Guid.Parse("b1d8e8b0-1007-4b2f-8a0a-000000000107"), Name = "J.K. Rowling" },
                new Author { Id = Guid.Parse("b1d8e8b0-1008-4b2f-8a0a-000000000108"), Name = "Stephen King" },
                new Author { Id = Guid.Parse("b1d8e8b0-1009-4b2f-8a0a-000000000109"), Name = "Agatha Christie" },
                new Author { Id = Guid.Parse("b1d8e8b0-100A-4b2f-8a0a-00000000010A"), Name = "J.R.R. Tolkien" },
                new Author { Id = Guid.Parse("b1d8e8b0-100B-4b2f-8a0a-00000000010B"), Name = "Ernest Hemingway" },
                new Author { Id = Guid.Parse("b1d8e8b0-100C-4b2f-8a0a-00000000010C"), Name = "Leo Tolstoy" },
                new Author { Id = Guid.Parse("b1d8e8b0-100D-4b2f-8a0a-00000000010D"), Name = "Isabel Allende" },
                new Author { Id = Guid.Parse("b1d8e8b0-100E-4b2f-8a0a-00000000010E"), Name = "Toni Morrison" },
                new Author { Id = Guid.Parse("b1d8e8b0-100F-4b2f-8a0a-00000000010F"), Name = "Haruki Murakami" }
            };
        }
    }
}
