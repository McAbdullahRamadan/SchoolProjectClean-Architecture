using Data.Entites.Identity;
using Data.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Service.AuthService.InterFace;

namespace Service.AuthService.Impelemention
{
    public class CurrentUserService : ICurrentUserService
    {
        #region Fields
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserIdentity> _userManager;


        #endregion
        #region Constructors
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, UserManager<UserIdentity> userManager)
        {

            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }


        #endregion
        #region Handle function


        public int GetUserId()
        {
            var UserId = _httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(claim => claim.Type == nameof(UserCliamModels.id)).Value;
            if (UserId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return int.Parse(UserId);
        }
        public async Task<UserIdentity> GetUserAsync()
        {
            var GetUserbyID = GetUserId();
            var usermanager = await _userManager.FindByIdAsync(GetUserbyID.ToString());
            if (usermanager == null)

                throw new UnauthorizedAccessException();
            return usermanager;



        }

        public async Task<List<string>> GetCurrentUserRolesAsync()
        {
            var user = await GetUserAsync();
            var roles = await _userManager.GetRolesAsync(user);
            return roles.ToList();
        }
        #endregion
    }
}
