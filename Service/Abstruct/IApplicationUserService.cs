using Data.Entites.Identity;

namespace Service.Abstruct
{
    public interface IApplicationUserService
    {
        public Task<string> AddUserAsync(UserIdentity user, string password);
    }
}
