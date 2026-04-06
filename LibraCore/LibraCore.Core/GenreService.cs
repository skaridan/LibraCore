using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.Book;

namespace LibraCore.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }
        public async Task<IEnumerable<BookGenreViewModel>> FetchGenresAsync()
        {
            IEnumerable<Genre> genres = await genreRepository.GetAllGenresAsync();

            IEnumerable<BookGenreViewModel> bookGenreViewModels = genres
                .Select(b => new BookGenreViewModel
                {
                    Id = b.Id,
                    Name = b.Name
                })
                .ToArray();

            return bookGenreViewModels;
        }
    }
}
