using Data.DTORequset;
using Data.Entites.Identity;
using Data.Helpers.Results;

namespace Service.Abstruct
{
    public interface IAuthorizService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistById(int RoleId);

        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int RoleId);
        public Task<List<RoleSys>> GetRoleAsync();
        public Task<RoleSys> GetRoleByIdAsync(int id);
        public Task<ManageUserRoleResult> GetManageRoleUser(UserIdentity User);
        public Task<string> UpdateUserRole(UpdateUserRoleRequest request);
        public Task<ManageUserClaimsResult> GetManageUserClaimsData(UserIdentity user);

        public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);








    }
}
