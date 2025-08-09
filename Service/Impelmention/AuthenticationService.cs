using Data.Entites.Identity;
using Data.Helpers;
using Infrastructure.AbstractRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Service.Abstruct;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service.Impelmention
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;

        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<UserIdentity> _userManager;

        #endregion
        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
            UserManager<UserIdentity> userManager, IRefreshTokenRepository refreshTokenRepository, ConcurrentDictionary<string, RefreshToken> UserRefresh)
        {
            _jwtSettings = jwtSettings;

            _refreshTokenRepository = refreshTokenRepository;

            _userManager = userManager;

        }
        #endregion
        #region Handle Function
        public async Task<JwtAuthResult> GetJWTToken(UserIdentity user)
        {

            //  var JwtToken = GenerateJWTToken(user);

            var (JwtToken, AccessToken) = GenerateJWTToken(user);
            var refreshtoken = GetRefreshToken(user.UserName);
            var UserRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                Token = AccessToken,

                IsUsed = true,
                IsRevoked = false,
                JwtId = JwtToken.Id,
                RefreshToken = refreshtoken.TokenSting,
                UserId = user.Id,
            };
            await _refreshTokenRepository.AddAsync(UserRefreshToken);

            var response = new JwtAuthResult();
            response.refreshToken = refreshtoken;
            response.AccessToken = AccessToken;
            return response;

        }
        private (JwtSecurityToken, string) GenerateJWTToken(UserIdentity user)
        {
            var claims = GetClaims(user);
            var JwtToken = new JwtSecurityToken(
             _jwtSettings.Issuer,
             _jwtSettings.audience,
             claims,
             expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate)
             , signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
             SecurityAlgorithms.HmacSha256Signature));
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);
            return (JwtToken, AccessToken);
        }
        private RefreshToken GetRefreshToken(string username)
        {
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                UserName = username,
                TokenSting = GenerateRefreshToken(),

            };

            return refreshToken;
        }
        private string GenerateRefreshToken()
        {
            var reandomNumber = new byte[32];
            var reandomNumberGenerate = RandomNumberGenerator.Create();
            reandomNumberGenerate.GetBytes(reandomNumber);
            return Convert.ToBase64String(reandomNumber);
        }
        public List<Claim> GetClaims(UserIdentity user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserCliamModels.UserName),user.UserName),
                new Claim(nameof(UserCliamModels.Email),user.Email),
                new Claim(nameof(UserCliamModels.PhoneNumber),user.PhoneNumber),
                new Claim(nameof(UserCliamModels.id),user.Id.ToString()),



            };
            return claims;

        }

        public async Task<JwtAuthResult> GetRefreshToken(UserIdentity user, JwtSecurityToken Jwttoken, DateTime? expireDate, string refreshtoken)
        {
            //read Token To get Clims



            //Generate RefreshToken
            var (JwtSecurityToken, NewToken) = GenerateJWTToken(user);

            var response = new JwtAuthResult();
            response.AccessToken = NewToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = Jwttoken.Claims.FirstOrDefault(x => x.Type == nameof(UserCliamModels.id)).Value;
            refreshTokenResult.TokenSting = refreshtoken;
            refreshTokenResult.ExpireAt = (DateTime)expireDate;
            response.refreshToken = refreshTokenResult;
            return response;

        }

        public JwtSecurityToken ReadJwtToken(string accesstoken)
        {
            if (string.IsNullOrEmpty(accesstoken))
            {
                throw new ArgumentNullException(nameof(accesstoken));

            }
            var handler = new JwtSecurityTokenHandler();
            var respons = handler.ReadJwtToken(accesstoken);
            return respons;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.validateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.validateIssuerSigninKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.audience,
                ValidateAudience = _jwtSettings.validateAudience,
                ValidateLifetime = _jwtSettings.validateLifetime,
            };
            var validaor = handler.ValidateToken(accessToken, parameters, out SecurityToken validatetoken);
            try
            {
                if (validaor == null)
                {
                    return "InvalidToken";
                }
                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<(string, DateTime?)> VAlidatDetalis(JwtSecurityToken JwtToken, string refreshToken, string accessToken)
        {
            if (JwtToken == null || !JwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {

                return ("AlgorithmIsWrong", null);
            }
            if (JwtToken.ValidTo > DateTime.UtcNow)
            {

                return ("TokenisNotExpired", null);
            }
            //Get User 

            var userId = JwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserCliamModels.id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(c => c.Token == refreshToken &&
                                     c.RefreshToken == accessToken &&
                                     c.UserId == int.Parse(userId));

            if (userRefreshToken == null)
            {

                return ("RefreshTokenisNotFound", null);

            }
            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
            {
                userRefreshToken.IsRevoked = true;
                userRefreshToken.IsUsed = false;

                await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                return ("RefreshTokenisExpired", null);

            }
            var expiredate = userRefreshToken.ExpiryDate;
            return (userId, expiredate);
        }

        #endregion

    }
}
