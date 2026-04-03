using LibraCore.Infrastructure.Data;

namespace LibraCore.Infrastructure.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private bool isDisposed = false;
        private readonly LibraCoreDbContext dbContext;

        protected BaseRepository(LibraCoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        protected LibraCoreDbContext DbContext => dbContext;

        protected async Task<int> SaveChangesAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }

            isDisposed = true;
        }
    }
}
