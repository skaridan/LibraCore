namespace LibraCore.ViewModels.Review
{
    public class ReviewViewModel
    {
        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string BookTitle { get; set; } = null!;

        public string UserName { get; set; } = null!;

        public int Rating { get; set; }

        public string Comment { get; set; } = null!;
    }
}
