using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraCoreDbContext dbContext;

        public BookRepository(LibraCoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await dbContext
                .Books
                .Include(b => b.Author)
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
