using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraCore.Infrastructure.Data.Entities
{
    [PrimaryKey(nameof(UserId), nameof(BookId))]
    public class UserBook
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public virtual ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }

        public virtual Book Book { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
