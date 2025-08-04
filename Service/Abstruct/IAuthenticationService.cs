using Data.Entites.Identity;

namespace Service.Abstruct
{
    public interface IAuthenticationService
    {
        public Task<string> GetJWTToken(UserIdentity user);
    }
}
