using AutoMapper;
using Core.Besec;
using Core.Features.UserRegistration.Command.Models;
using Core.Resource;
using Data.Entites.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Core.Features.UserRegistration.Command.Handle
{
    public class UserCommandHandle : ResponseHadlar,
        IRequestHandler<AddUserCommand, Response<string>>,
        IRequestHandler<EditUserCommand, Response<string>>,
        IRequestHandler<DeleteUserCommand, Response<string>>,
        IRequestHandler<ChangePasswordCommand, Response<string>>



    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly UserManager<UserIdentity> _userManager;


        #endregion
        #region Constructors
        public UserCommandHandle(IStringLocalizer<SheardResource> stringLocalizer, IMapper mapper
            , UserManager<UserIdentity> userManage) : base(stringLocalizer)
        {
            _Localizer = stringLocalizer;
            _mapper = mapper;
            _userManager = userManage;

        }

        #endregion
        #region Handle Function

        public async Task<Response<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //if Email Is Exist
            var User = await _userManager.FindByEmailAsync(request.Email);
            // Email Exist
            if (User != null) return BadRequst<string>(_Localizer[KeySharedResource.EmailIsExist]);
            //if UserName Is Exist
            var UserByuserName = await _userManager.FindByNameAsync(request.FullName);
            //UserName Is Exist
            if (UserByuserName != null) return BadRequst<string>(_Localizer[KeySharedResource.UserNameIsExist]);
            //Mapping
            var IdentityUser = _mapper.Map<UserIdentity>(request);
            //Create User
            var CrateResult = await _userManager.CreateAsync(IdentityUser, request.Password);

            //Failed
            if (!CrateResult.Succeeded)

                return BadRequst<string>(CrateResult.Errors.FirstOrDefault().Description);

            await _userManager.AddToRoleAsync(IdentityUser, "User");



            //return
            return Created("");
        }

        public async Task<Response<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //Check if User is Exist
            var Olduser = await _userManager.FindByIdAsync(request.Id.ToString());
            //return NotFuond
            if (Olduser == null)
                return NotFound<string>();
            //Mapping
            var newuser = _mapper.Map(request, Olduser);
            //Update
            //UserName is Exist
            var UserByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newuser.UserName && x.Id != newuser.Id);
            if (UserByUserName != null)
                return BadRequst<string>(_Localizer[KeySharedResource.UserNameIsExist]);
            //Result is Not Success
            var result = await _userManager.UpdateAsync(newuser);
            if (!result.Succeeded)
                return BadRequst<string>(_Localizer[KeySharedResource.UpdateFailed]);
            return Success((string)_Localizer[KeySharedResource.Update]);
        }

        public async Task<Response<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //Check if User is Exist
            var user1 = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist
            if (user1 == null)
                return NotFound<string>();
            //Deleted
            var result = await _userManager.DeleteAsync(user1);
            //check Success Or No
            if (!result.Succeeded)
                BadRequst<string>(_Localizer[KeySharedResource.DeletedFailed]);
            return Success((string)_Localizer[KeySharedResource.Deleted]);
        }

        public async Task<Response<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            //Get Users
            var user1 = await _userManager.FindByIdAsync(request.Id.ToString());
            //if check user is null
            if (user1 == null)
                return NotFound<string>();
            //Change Password 
            var result = await _userManager.ChangePasswordAsync(user1, request.CurrentPassword, request.NewPassword);
            if (!result.Succeeded)
                return BadRequst<string>(_Localizer[KeySharedResource.ChangePassFailed]);
            return Success((string)_Localizer[KeySharedResource.ChangePassSuccess]);
        }
        #endregion
    }
}
