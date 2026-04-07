using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(LibraCoreDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await DbContext
                .Users
                .ToArrayAsync();
        }
    }
}
