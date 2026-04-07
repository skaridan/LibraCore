namespace LibraCore.ViewModels.Book
{
    public class AllBooksQueryModel
    {
        public const int BooksPerPage = 5; 

        public string? SearchTerm { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalBooksCount { get; set; }

        public IEnumerable<BookIndexViewModel> Books { get; set; } 
            = new List<BookIndexViewModel>();
    }
}
