using Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<UserIdentity> _userManager)
        {
            var userCount = await _userManager.Users.CountAsync();
            if (userCount <= 0)
            {
                var DefultUser = new UserIdentity()
                {
                    FullName = "Admin",
                    UserName = "Admin2",
                    Email = "Admin@Proj.com",
                    Address = "Giza",
                    Country = "Eg",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(DefultUser, "Admin123");
                await _userManager.AddToRoleAsync(DefultUser, "Admin");
            }

        }
    }
}
