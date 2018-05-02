using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using PrimeActs.Domain.ViewModels;
namespace PrimeActs.Rules.ValidationRules
{
    public class CompanyEditModelValidator : AbstractValidator<CompanyEditModel>
    {
        public CompanyEditModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty();
            RuleFor(x => x.SelectedCompany).NotNull().NotEmpty();
        }
    }
}
