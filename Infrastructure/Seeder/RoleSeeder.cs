using Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<RoleSys> _RoleManager)
        {
            var RolesCount = await _RoleManager.Roles.CountAsync();
            if (RolesCount <= 0)
            {
                await _RoleManager.CreateAsync(new RoleSys()
                {
                    Name = "Admin"
                });
                await _RoleManager.CreateAsync(new RoleSys()
                {
                    Name = "User"
                });
            }

        }
    }
}
