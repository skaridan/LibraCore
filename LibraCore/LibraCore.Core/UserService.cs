using LibraCore.Infrastructure.Data.Entities;
using LibraCore.Infrastructure.Repositories.Interfaces;
using LibraCore.Services.Interfaces;
using LibraCore.ViewModels.User;

namespace LibraCore.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            IEnumerable<ApplicationUser> users = await userRepository
                .GetAllUsersAsync();

            IEnumerable<UserViewModel> userViewModels = users
               .Select(u => new UserViewModel
               {
                   Id = u.Id,
                   UserName = u.UserName!,
                   Email = u.Email!
               })
               .OrderBy(u => u.UserName)
               .ToArray();

            return userViewModels;
        }
    }
}
