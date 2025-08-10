namespace Service.Abstruct
{
    public interface IAuthorizService
    {
        public Task<string> AddRoleAsync(string roleName);
        public Task<bool> IsRoleExist(string roleName);

    }
}
