using Microsoft.AspNetCore.Identity;

namespace LibraCore.Infrastructure.Data.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid();
        }
    }
}
