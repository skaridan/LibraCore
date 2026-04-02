using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static LibraCore.GCommon.EntityValidation.Book;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        public DateOnly ReleaseDate { get; set; }

        [Url]
        public string? ImageUrl { get; set; }

        [Column(TypeName = BookPriceFormat)]
        public decimal Price { get; set; }

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }

        public virtual Author Author { get; set; } = null!;

        public Guid GenreID { get; set; }

        public virtual Genre Genre { get; set; } = null!;

        public virtual ICollection<UserBook> UsersBooks { get; set; }
            = new HashSet<UserBook>();
    }
}
