using Core.Features.Students.Commands.Models;
using Core.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Service.Abstruct;

namespace Core.Features.Students.Commands.Validators
{
    public class AddStudentValidator : AbstractValidator<AddStudentComand>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SheardResource> _Localizer;
        private readonly IDepartmentService _departmentservice;

        #endregion

        #region Constructors
        public AddStudentValidator(IStudentService studentService, IStringLocalizer<SheardResource> Localizer,
            IDepartmentService departmentservice)
        {
            _studentService = studentService;
            _Localizer = Localizer;
            _departmentservice = departmentservice;
            ApplayValidationRule();
            ApplyCustomeValidatioonrules();

        }
        #endregion
        #region Actions
        public void ApplayValidationRule()
        {
            RuleFor(x => x.NameAR)
                .NotEmpty().WithMessage(_Localizer[KeySharedResource.NotEmtpy])
                .NotNull().WithMessage(_Localizer[KeySharedResource.NotNull])
                .MaximumLength(15).WithMessage("Max Length is 15");
            RuleFor(x => x.Adreess)
            .NotEmpty().WithMessage("{PropertyName}Name is Not Empty")
            .NotNull().WithMessage("Name is Not Null")
            .MaximumLength(15).WithMessage("Max Length is 15");

        }
        public void ApplyCustomeValidatioonrules()
        {
            RuleFor(x => x.NameAR)
                   .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
                   .WithMessage(_Localizer[KeySharedResource.IsExist]);
            RuleFor(x => x.NameEn)
              .MustAsync(async (key, CancellationToken) => !await _studentService.IsNameExist(key))
              .WithMessage(_Localizer[KeySharedResource.IsExist]);
            RuleFor(x => x.DepartmentId)
                .MustAsync(async (key, CancellationToken) => await _departmentservice.IsDEpartmentIdExist(key))
                .WithMessage(_Localizer[KeySharedResource.DepartmentIsNotExist]);

        }
        #endregion
    }
}
