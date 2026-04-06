namespace LibraCore.ViewModels.Book
{
    public class BookDetailsViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string ReleaseDate { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
