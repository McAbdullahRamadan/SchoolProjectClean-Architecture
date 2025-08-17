using Core.Besec;
using Core.Features.Authorization.Command.Models;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Command.Handle
{
    public class RoleCommandHandler : ResponseHadlar,
        IRequestHandler<AddRoleCommand, Response<string>>,
        IRequestHandler<EditRoleCommand, Response<string>>,
        IRequestHandler<DeleteRoleCommand, Response<string>>,
        IRequestHandler<UpdateRoleUserCommand, Response<string>>



    {
        #region Fields
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;
        #endregion 
        #region Constructors
        public RoleCommandHandler(IStringLocalizer<SheardResource> localizer, IAuthorizService authorizService) : base(localizer)
        {
            _localizer = localizer;
            _authorizService = authorizService;

        }


        #endregion
        #region Handle Function
        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizService.AddRoleAsync(request.RoleName);
            if (result == "Success")
                return Success("");
            return BadRequst<string>(_localizer[KeySharedResource.AddFailed]);
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizService.EditRoleAsync(request);
            if (result == "NotFound")
                return NotFound<string>();
            else if (result == "Success")
                return Success<string>(_localizer[KeySharedResource.editRoleSuccess]);
            else
                return BadRequst<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizService.DeleteRoleAsync(request.Id);
            if (result == "NotFound")
                return NotFound<string>();
            if (result == "Used")
                return BadRequst<string>(_localizer[KeySharedResource.RoleIsUsed]);
            else if (result == "Success")
                return Success<string>(_localizer[KeySharedResource.Deleted]);
            else
                return BadRequst<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateRoleUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizService.UpdateUserRole(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_localizer[KeySharedResource.userIsNotFound]);
                case "FialedToRemoveRoles": return BadRequst<string>(_localizer[KeySharedResource.FailedToRemoveRoles]);
                case "FialedToAddNewRoles": return BadRequst<string>(_localizer[KeySharedResource.FialedToAddNewRoles]);
                //case "Success": return Success<string>(_localizer[KeySharedResource.Success]);
                case "FialedToAddUserRole": return BadRequst<string>(_localizer[KeySharedResource.FialedToAddUserRole]);
            }
            return Success<string>(_localizer[KeySharedResource.Success]);
        }
        #endregion

    }
}
