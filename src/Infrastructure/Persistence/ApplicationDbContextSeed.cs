using NanoCell.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace NanoCell.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var defaultUser = new ApplicationUser { UserName = "admin@admin.com",Surname="admin", Email = "jason@clean-architecture",EmailConfirmed=true };
            if (userManager.Users.All(u => u.Email != defaultUser.Email))
            {
                await userManager.CreateAsync(defaultUser, "NanoCell!");
            }
        }
    }
}
