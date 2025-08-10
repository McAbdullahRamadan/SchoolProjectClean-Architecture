using Core.Besec;
using Core.Features.Authorization.Command.Models;
using Core.Resource;
using MediatR;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Command.Handle
{
    public class RoleCommandHandler : ResponseHadlar,
        IRequestHandler<AddRoleCommand, Response<string>>
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
        #endregion

    }
}
