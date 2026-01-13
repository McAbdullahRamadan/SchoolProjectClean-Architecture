using Core.Features.Authorization.Command.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authorization.Command.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        private readonly IStringLocalizer<SheardResource> _localizer;

        #endregion
        #region Constructors
        public EditRoleValidator(IStringLocalizer<SheardResource> localizer)
        {
            _localizer = localizer;

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
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(_localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_localizer[KeySharedResource.NotNull]);


        }
        public void ApplyCustomeValidatioonrules()
        {
            //RuleFor(x => x.RoleName)
            //      .MustAsync(async (key, CancellationToken) => !await _authorizService.IsRoleExist(key))
            //      .WithMessage(_localizer[KeySharedResource.IsExist]);
        }
        #endregion

    }
}
