using Core.Features.Authorization.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Command.Validators
{
    public class AddValidatorRole : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;
        #endregion
        #region Constructors
        public AddValidatorRole(IStringLocalizer<SheardResource> localizer, IAuthorizService authorizService)
        {
            _localizer = localizer;
            _authorizService = authorizService;
            ApplayValidationRule();
            ApplyCustomeValidatioonrules();
        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage(_localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_localizer[KeySharedResource.NotNull]);


        }
        public void ApplyCustomeValidatioonrules()
        {
            RuleFor(x => x.RoleName)
                  .MustAsync(async (key, CancellationToken) => !await _authorizService.IsRoleExist(key))
                  .WithMessage(_localizer[KeySharedResource.IsExist]);
        }
        #endregion

    }
}
