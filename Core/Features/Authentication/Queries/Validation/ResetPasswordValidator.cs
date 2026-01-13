using Core.Features.Authentication.Queries.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Core.Features.Authentication.Queries.Validation
{
    public class ResetPasswordValidator : AbstractValidator<ConfirmResetPasswordQuery>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;


        #endregion

        #region Constructors
        public ResetPasswordValidator(IStringLocalizer<SheardResource> Localizer)
        {

            _Localizer = Localizer;

            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {


            RuleFor(x => x.Code)
                 .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                 .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);

            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                 .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull]);


        }
        public void ApplyCustomeValidatioonrules()
        {


        }
        #endregion
    }
}
