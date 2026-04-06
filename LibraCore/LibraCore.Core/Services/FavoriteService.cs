using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Services.Interfaces;
using LibraCore.ViewModels;

using LibraCore.GCommon.Exceptions;

namespace LibraCore.Services.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository favoriteRepository;
        private readonly IBookRepository bookRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository, IBookRepository bookRepository)
        {
            this.favoriteRepository = favoriteRepository;
            this.bookRepository = bookRepository;
        }

        public async Task<IEnumerable<FavoriteViewModel>> GetUserBooksOrderedByTitleAsync(string userId)
        {
            IEnumerable<UserBook> userBooks = await favoriteRepository
                .GetAllUserBooksAsync(userId);

            IEnumerable<FavoriteViewModel> favoriteViewModels = userBooks
                .Select(ub => new FavoriteViewModel
                {
                    Id = ub.BookId,
                    Title = ub.Book.Title,
                    Author = ub.Book.Author.Name,
                    Price = ub.Book.Price,
                    ImageUrl = ub.Book.ImageUrl
                })
                .OrderBy(f => f.Title)
                .ToArray();

            return favoriteViewModels;
        }

        public async Task AddToFavoritesAsync(string userId, Guid bookId)
        {
            UserBook? userBook = await favoriteRepository
                .GetUserBookAsync(userId, bookId);
            if (userBook != null && userBook.IsDeleted == false)
            {
                throw new EntityAlreadyExistsException();
            }

            bool bookExists = await bookRepository.ExistsByIdAsync(bookId);
            if (!bookExists)
            {
                throw new EntityNotFoundException();
            }

            bool successPersist = false;
            if (userBook == null)
            {
                UserBook newUserBook = new UserBook
                {
                    UserId = Guid.Parse(userId),
                    BookId = bookId
                };

                successPersist = await favoriteRepository.AddUserBookAsync(newUserBook);
            }
            else
            {
                userBook.IsDeleted = false;

                successPersist = await favoriteRepository
                    .UpdateUserBookAsync(userBook);
            }

            if (!successPersist)
            {
                throw new EntityPersistFailureException();
            }
        }
    }
}
