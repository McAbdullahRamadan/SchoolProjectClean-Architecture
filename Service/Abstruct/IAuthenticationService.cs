using Data.Entites.Identity;
using Data.Helpers;
using System.IdentityModel.Tokens.Jwt;

namespace Service.Abstruct
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTToken(UserIdentity user);
        public JwtSecurityToken ReadJwtToken(string accessToken);
        public Task<(string, DateTime?)> VAlidatDetalis(JwtSecurityToken JwtToken, string rerreshToken, string accessToken);
        public Task<JwtAuthResult> GetRefreshToken(UserIdentity user, JwtSecurityToken Jwttoken, DateTime? expiryDate, string refreshtoken);
        public Task<string> ValidateToken(string accessToken);
        public Task<string> ConfirmEmail(int? userId, string? code);
        public Task<string> SendResetPasswordCode(string Email);
        public Task<string> ResetPasswordCode(string code, string email);




    }
}
