using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LibraCore.GCommon.EntityValidation.Review;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Range(MinRating, MaxRating)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(CommentMaxLength)]
        public string Comment { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;
    }
}
