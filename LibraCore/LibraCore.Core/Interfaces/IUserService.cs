using LibraCore.ViewModels.User;

namespace LibraCore.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllUsersAsync();
    }
}
