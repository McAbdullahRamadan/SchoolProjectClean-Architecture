using Core.Features.Authentication.Queries.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authentication.Queries.Validation
{
    public class ConfirmEmailQueryValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion

        #region Constructors
        public ConfirmEmailQueryValidator(IStringLocalizer<SheardResource> Localizer)
        {

            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.userId)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);

            RuleFor(x => x.code)
                 .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                 .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);

        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}
