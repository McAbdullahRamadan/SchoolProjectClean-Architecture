using Data.Entites.Identity;

namespace Service.AuthService.InterFace
{
    public interface ICurrentUserService
    {
        public Task<UserIdentity> GetUserAsync();
        public int GetUserId();
        public Task<List<string>> GetCurrentUserRolesAsync();
    }
}
