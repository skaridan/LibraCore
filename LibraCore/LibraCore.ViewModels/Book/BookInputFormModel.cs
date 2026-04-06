using System.ComponentModel.DataAnnotations;

using static LibraCore.GCommon.EntityValidation.Book;
using static LibraCore.GCommon.OutputMessages.Book;
using static LibraCore.GCommon.EntityValidation.Author;

namespace LibraCore.ViewModels.Book
{
    public class BookInputFormModel
    {
        [Required(ErrorMessage = TitleRequired)]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = GenreRequired)]
        public Guid GenreId { get; set; }

        [Required(ErrorMessage = DescriptionRequired)]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = AuthorRequired)]
        [MinLength(AuthorNameMinLength)]
        [MaxLength(AuthorNameMaxLength)]
        public string Author { get; set; } = null!;

        [Required]
        [Range(0.01, 1000.00, ErrorMessage = PriceRange)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ReleaseDateRequired)]
        public DateTime ReleaseDate { get; set; }

        [Url(ErrorMessage = ImageUrlInvalid)]
        public string? ImageUrl { get; set; }

        public ICollection<BookGenreViewModel> Genres { get; set; }
            = new List<BookGenreViewModel>();
    }
}
