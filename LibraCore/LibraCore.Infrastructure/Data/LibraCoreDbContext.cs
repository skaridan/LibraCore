using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Data
{
    public class LibraCoreDbContext : IdentityDbContext
    {
        public LibraCoreDbContext(DbContextOptions<LibraCoreDbContext> options)
            : base(options)
        {
        }
    }
}
