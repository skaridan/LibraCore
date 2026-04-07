using LibraCore.Infrastructure.Data;
using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraCore.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository, IAuthorRepository
    {
        public AuthorRepository(LibraCoreDbContext dbContext)
            : base(dbContext)
        {
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await DbContext
                .Authors
                .Where(a => a.IsDeleted == false)
                .AsNoTracking()
                .ToArrayAsync();
        }

        public async Task<Author?> AuthorExistsByNameAsync(string name)
        {
            Author? author = await DbContext
                .Authors
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Name.ToLower() == name.ToLower() && a.IsDeleted == false);

            return author;
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            await DbContext.Authors.AddAsync(author);

            int resultCount = await SaveChangesAsync();
            return resultCount == 1;
        }
        public async Task<Author?> GetAuthorByIdAsync(Guid id)
        {
            return await DbContext
                .Authors
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<bool> SoftDeleteAuthorAsync(Author author)
        {
            author.IsDeleted = true;

            DbContext.Authors.Update(author);

            int resultCount = await SaveChangesAsync();

            return resultCount == 1;
        }

    }
}
