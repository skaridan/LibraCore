using LibraCore.Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Data
{
    public class LibraCoreDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public LibraCoreDbContext(DbContextOptions<LibraCoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;

        public virtual DbSet<Author> Authors { get; set; } = null!;

        public virtual DbSet<Genre> Genres { get; set; } = null!;

        public virtual DbSet<Review> Reviews { get; set; } = null!;

        public virtual DbSet<Order> Orders { get; set; } = null!;

        public virtual DbSet<OrderItem> OrdersItems { get; set; } = null!;

        public virtual DbSet<UserBook> UsersBooks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
