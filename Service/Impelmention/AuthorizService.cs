using Data.DTORequset;
using Data.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class AuthorizService : IAuthorizService
    {
        #region Fields
        private readonly RoleManager<RoleSys> _roleManager;
        private readonly UserManager<UserIdentity> _UserManager;

        #endregion
        #region Constructors
        public AuthorizService(RoleManager<RoleSys> roleManager, UserManager<UserIdentity> UserManager)
        {
            _roleManager = roleManager;
            _UserManager = UserManager;


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


        public async Task<bool> IsRoleExistByName(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }
        public async Task<string> EditRoleAsync(EditRoleRequest request)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
                return "NotFound";
            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("", result.Errors);
            return errors;

        }

        public async Task<bool> IsRoleExistById(int roleId)
        {
            var result = await _roleManager.FindByIdAsync(roleId.ToString());
            if (result == null)
                return false;
            else
                return true;
        }

        public async Task<string> DeleteRoleAsync(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return "NotFound";
            var userRole = await _UserManager.GetUsersInRoleAsync(role.Name);
            if (userRole != null && userRole.Count() > 0)
                return "Used";
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Success";
            var errors = string.Join("", result.Errors);
            return errors;

        }

        public async Task<List<RoleSys>> GetRoleAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<RoleSys> GetRoleByIdAsync(int id)
        {
            return await _roleManager.FindByIdAsync(id.ToString());
        }

        #endregion

    }
}
