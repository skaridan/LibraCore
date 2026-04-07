using System.ComponentModel.DataAnnotations;

using static LibraCore.GCommon.EntityValidation.Review;

using static LibraCore.GCommon.OutputMessages.Review;


namespace LibraCore.ViewModels.Review
{
    public class ReviewInputModel
    {
        public Guid BookId { get; set; }

        [Required(ErrorMessage = RatingRequired)]
        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        [Required(ErrorMessage = CommentRequired)]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = null!;
    }
}
