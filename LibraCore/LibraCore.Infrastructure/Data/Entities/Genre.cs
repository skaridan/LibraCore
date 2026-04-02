using System.ComponentModel.DataAnnotations;

using static LibraCore.GCommon.EntityValidation.Genre;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(GenreNameMaxLength)]
        public string Name { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
