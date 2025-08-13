using Core.Features.Authorization.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Authorization.Command.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SheardResource> _localizer;
        private readonly IAuthorizService _authorizService;

        #endregion
        #region Constructors
        public DeleteRoleValidator(IStringLocalizer<SheardResource> localizer, IAuthorizService authorizService)
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
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(_localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_localizer[KeySharedResource.NotNull]);


        }
        public void ApplyCustomeValidatioonrules()
        {
            //RuleFor(x => x.Id)
            //   .MustAsync(async (key, CancellationToken) => await _authorizService.IsRoleExistById(key))
            //   .WithMessage(_localizer[KeySharedResource.RoleNotExist]);

        }
        #endregion

    }
}
