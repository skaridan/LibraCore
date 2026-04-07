namespace LibraCore.ViewModels.User
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
