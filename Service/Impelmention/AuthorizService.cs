using Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class AuthorizService : IAuthorizService
    {
        #region Fields
        private readonly RoleManager<RoleSys> _roleManager;
        #endregion
        #region Constructors
        public AuthorizService(RoleManager<RoleSys> roleManager)
        {
            _roleManager = roleManager;


        }
        #endregion
        #region Handle Function
        public async Task<string> AddRoleAsync(string roleName)
        {
            var identityRole = new RoleSys();
            identityRole.Name = roleName;
            var Result = await _roleManager.CreateAsync(identityRole);
            if (Result.Succeeded)
                return "Success";
            return "Failed";
        }

        public async Task<bool> IsRoleExist(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        #endregion

    }
}
