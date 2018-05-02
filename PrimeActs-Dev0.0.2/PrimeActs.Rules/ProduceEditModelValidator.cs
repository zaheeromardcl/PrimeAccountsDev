#region

using FluentValidation;
using PrimeActs.Domain.ViewModels.Produce;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class ProduceEditModelValidator : AbstractValidator<ProduceEditModel>
    {
        public ProduceEditModelValidator()
        {
            RuleFor(x => x.ProduceName).NotNull().NotEmpty();
            RuleFor(x => x.ProduceCode).NotNull().NotEmpty().Length(4);
            RuleFor(x => x.SelectedMasterGroup).NotNull().NotEmpty().WithMessage("Please choose Master group");
            RuleFor(x => x.SelectedProduceGroup).NotNull().NotEmpty().WithMessage("Please choose Produce group");
        }
    }
}