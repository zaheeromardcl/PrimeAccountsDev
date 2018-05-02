#region

using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Rules.ValidationRules
{
    public class InvoiceDivisionValidator : AbstractValidator<InvoiceEvent>
    {
        private readonly List<InvoiceEvent> _events;

        public InvoiceDivisionValidator(List<InvoiceEvent> events)
        {
            _events = new List<InvoiceEvent>();
            _events.AddRange(events);
            RuleFor(x => _events.Any(y => y.Division.DivisionID == x.Division.DivisionID));
        }
    }
}