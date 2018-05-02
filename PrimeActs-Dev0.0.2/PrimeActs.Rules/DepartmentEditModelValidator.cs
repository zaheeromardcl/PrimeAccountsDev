#region

using FluentValidation;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class DepartmentEditModelValidator : AbstractValidator<DepartmentEditModel>
    {
        public DepartmentEditModelValidator()
        {
            RuleFor(x => x.DepartmentName).NotNull().NotEmpty();
            //RuleFor(x => x.SelectedDivision).NotNull().NotEmpty();
        }
    }
}