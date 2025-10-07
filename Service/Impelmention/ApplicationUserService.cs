using Data.Entites.Identity;
using Infrastructure.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Abstruct;

namespace Service.Impelmention
{
    public class ApplicationUserService : IApplicationUserService
    {
        #region Feilds

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<UserIdentity> _userManager;
        private readonly IEmailsService _emailsServic;
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUrlHelper _urlHelper;

        #endregion
        #region Constructors
        public ApplicationUserService(
             UserManager<UserIdentity> userManage, IUrlHelper urlHelper, IEmailsService emailsServic, ApplicationDbContext applicationDbContext, IHttpContextAccessor httpContextAccessor)
        {

            _emailsServic = emailsServic;
            _userManager = userManage;
            _httpContextAccessor = httpContextAccessor;
            _applicationDbContext = applicationDbContext;
            _urlHelper = urlHelper;

        }
        #endregion
        #region Handle Function
        public async Task<string> AddUserAsync(UserIdentity user, string password)
        {
            var trans = await _applicationDbContext.Database.BeginTransactionAsync();
            try
            {
                //if Email Is Exist
                var UserIsExist = await _userManager.FindByEmailAsync(user.Email);
                // Email Exist
                if (UserIsExist != null) return "EmailIsExist";
                //if UserName Is Exist
                var UserByuserName = await _userManager.FindByNameAsync(user.UserName);
                //UserName Is Exist
                if (UserByuserName != null) return "UserNameIsExist";

                //Create User
                var CreateResult = await _userManager.CreateAsync(user, password);

                //Failed
                if (!CreateResult.Succeeded)

                    return string.Join(",", CreateResult.Errors.Select(x => x.Description).ToList());

                await _userManager.AddToRoleAsync(user, "User");
                //Send Confirm Email 
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var requestAccessor = _httpContextAccessor.HttpContext.Request;
                var returnUrl = requestAccessor.Scheme + "://" + requestAccessor.Host +
                    _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
                var messsage = $"Click Link ConfirmEmail : <a href='{returnUrl}'></a>";
                //$"/api/Authentication/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
                //Message or body
                await _emailsServic.SendEmails(user.Email, returnUrl, "Confirm Email");

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                return "Falied";
            }

        }
        #endregion

    }
}
