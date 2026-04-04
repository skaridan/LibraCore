using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class BookRepository : BaseRepository, IBookRepository
    {
        public BookRepository(LibraCoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await DbContext
                .Books
                .Include(b => b.Genre)
                .Include(b => b.Author)
                .Where(b => b.IsDeleted == false)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            await DbContext.Books.AddAsync(book);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<Book?> GetBookByIdAsync(Guid id)
        {
            return await DbContext
                .Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .SingleOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> EditBookAsync(Book book)
        {
            DbContext.Books.Update(book);
            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

        public async Task<bool> SoftDeleteBookAsync(Book book)
        {
            book.IsDeleted = true;

            DbContext.Books.Update(book);

            int resultCount = await SaveChangesAsync();
            return resultCount == 1;
        }
    }
}
