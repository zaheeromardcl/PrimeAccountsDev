#region

using FluentValidation;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class ApplicationUserEditModelValidator : AbstractValidator<AspNetUserEditModel>
    {
    }
}