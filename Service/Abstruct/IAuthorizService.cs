using Data.DTORequset;

namespace Service.Abstruct
{
    public interface IAuthorizService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExistById(int RoleId);

        public Task<bool> IsRoleExistByName(string roleName);
        public Task<string> EditRoleAsync(EditRoleRequest request);
        public Task<string> DeleteRoleAsync(int RoleId);



    }
}
