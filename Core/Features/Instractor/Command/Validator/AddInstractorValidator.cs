using Core.Features.Instractor.Command.Model;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Instractor.Command.Validator
{
    public class AddInstractorValidator : AbstractValidator<AddInstractorCommand>
    {
        #region Fields

        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly IDepartmentService _departmentservice;
        private readonly IInstractorService _instractorService;

        #endregion

        #region Constructors
        public AddInstractorValidator(IStringLocalizer<SheardResource> Localizer,
            IDepartmentService departmentservice,
            IInstractorService instractorService)
        {

            _Localizer = Localizer;
            _departmentservice = departmentservice;
            ApplayValidationRule();
            ApplyCustomeValidatioonrules();
            _instractorService = instractorService;
        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.ENameAr)
                     .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
                     .NotNull().WithMessage("Name is Not Null");
            RuleFor(x => x.ENameEn)
                     .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
                     .NotNull().WithMessage("Name is Not Null");
            RuleFor(x => x.DID)
                  .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
                  .NotNull().WithMessage("Name is Not Null");

        }
        public void ApplyCustomeValidatioonrules()
        {
            RuleFor(x => x.ENameAr)
                   .MustAsync(async (key, CancellationToken) => !await _instractorService.IsNameArExist(key))
                   .WithMessage(_Localizer[KeySharedResource.IsExist]);
            RuleFor(x => x.ENameEn)
              .MustAsync(async (key, CancellationToken) => !await _instractorService.IsNameEnExist(key))
              .WithMessage(_Localizer[KeySharedResource.IsExist]);
            RuleFor(x => x.DID)
                .MustAsync(async (key, CancellationToken) => await _departmentservice.IsDEpartmentIdExist(key))
                .WithMessage(_Localizer[KeySharedResource.DepartmentIsNotExist]);

        }
        #endregion
    }
}
