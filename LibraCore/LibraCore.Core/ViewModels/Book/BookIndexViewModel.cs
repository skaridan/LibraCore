using LibraCore.Infrastructure.Data.Entities;

namespace LibraCore.Services.ViewModels.Book
{
    public class BookIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public Author Author { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }
    }
}
