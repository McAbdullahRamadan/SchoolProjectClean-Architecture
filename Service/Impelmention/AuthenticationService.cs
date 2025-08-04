using Data.Entites.Identity;
using Data.Helpers;
using Microsoft.IdentityModel.Tokens;
using Service.Abstruct;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Impelmention
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        #endregion
        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }
        #endregion
        #region Handle Function
        public Task<string> GetJWTToken(UserIdentity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserCliamModels.UserName),user.UserName),
                new Claim(nameof(UserCliamModels.Email),user.Email),
                new Claim(nameof(UserCliamModels.PhoneNumber),user.PhoneNumber),


            };
            var JwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(2)
                , signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature));
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return Task.FromResult(AccessToken);

        }
        #endregion

    }
}
