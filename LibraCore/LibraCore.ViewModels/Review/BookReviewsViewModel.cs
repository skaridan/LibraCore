namespace LibraCore.ViewModels.Review
{
    public class BookReviewsViewModel
    {
        public Guid BookId { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; } 
            = new List<ReviewViewModel>();
    }
}
