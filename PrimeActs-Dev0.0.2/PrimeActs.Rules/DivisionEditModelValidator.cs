#region

using FluentValidation;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Division;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class DivisionEditModelValidator : AbstractValidator<DivisionEditModel>
    {
        public DivisionEditModelValidator()
        {
            RuleFor(x => x.DivisionName).NotNull().NotEmpty();
            //RuleFor(x => x.SelectedCompany).NotNull().NotEmpty();
        }
    }
}