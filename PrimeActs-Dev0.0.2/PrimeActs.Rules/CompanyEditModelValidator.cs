#region

using FluentValidation;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class CompanyEditModelValidator : AbstractValidator<CompanyEditModel>
    {
        public CompanyEditModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty();
        }
    }
}