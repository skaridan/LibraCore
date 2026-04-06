namespace LibraCore.ViewModels.Book
{
    public class BookIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Author { get; set; } = null!;

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsFavorite { get; set; }
    }
}
