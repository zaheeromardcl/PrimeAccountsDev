#region

using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Rules.ValidationRules;
using System.Threading;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IEventOrchestra
    {
        PrimeEvent GetCurrentRunning(PrimeEvent primeEvent);
        bool IsRunning(PrimeEvent primeEvent);
        void StartEvent(PrimeEvent primeEvent);
        void EndEvent(PrimeEvent primeEvent);
    }

    public class EventOrchestra : IEventOrchestra
    {
        private static readonly object eventLock = new object();
        private IPrincipal _principal;
        private readonly List<PrimeEvent> primeEvents;

        public EventOrchestra()
        {
            primeEvents = new List<PrimeEvent>();
        }

        public bool IsRunning(PrimeEvent primeEvent)
        {
            var currentPrimeEvent = primeEvents.FirstOrDefault(x => x.EventName == primeEvent.EventName);
            if (currentPrimeEvent == null)
                return false;
            lock (eventLock)
            {
                foreach (var division in currentPrimeEvent.Events)
                {
                    if (primeEvent.Events.Any(x => x.Division.DivisionID == division.Division.DivisionID))
                        return true;
                }
            }
            return false;
        }


        public PrimeEvent GetCurrentRunning(PrimeEvent primeEvent)
        {
            return primeEvents.SingleOrDefault(x => x.EventName == primeEvent.EventName);
        }


        public void StartEvent(PrimeEvent primeEvent)
        {
            if (IsRunning(primeEvent))
                return;
            lock (eventLock)
            {
                var currentPrimeEvent = primeEvents.FirstOrDefault(x => x.EventName == primeEvent.EventName);
                if (currentPrimeEvent == null)
                {
                    var pe = new PrimeEvent{EventName = primeEvent.EventName,EventDescription = primeEvent.EventDescription};
                    
                    foreach (var item in primeEvent.Events)
	                {
                        Division division = new Division {DivisionID = item.Division.DivisionID,DivisionName = item.Division.DivisionName};
                        pe.Events.Add(new InvoiceEvent { Division=division,InvoiceType=item.InvoiceType,Period =item.Period,Username = item.Username});
	                }
                    primeEvents.Add(pe);
                    return;
                }
                if (!IsDivisionRunningInvoice(primeEvent, currentPrimeEvent))
                    currentPrimeEvent.Events.AddRange(primeEvent.Events);
            }            
        }

        public void EndEvent(PrimeEvent primeEvent)
        {
            if (IsRunning(primeEvent))
            {
                lock (eventLock)
                {
                    foreach (var item in primeEvent.Events)
                    {
                        if (primeEvents.SingleOrDefault(x => x.EventName == primeEvent.EventName).Events.Any(x => x.Division.DivisionID == item.Division.DivisionID))
                            primeEvents.SingleOrDefault(x => x.EventName == primeEvent.EventName).Events.RemoveAll(x => x.Division.DivisionID == item.Division.DivisionID);
                    }
                }
            }
        }

        private static bool IsDivisionRunningInvoice(PrimeEvent primeEvent, PrimeEvent currentPrimeEvent)
        {
            var invoiceDivisionValidator = new InvoiceDivisionValidator(currentPrimeEvent.Events);

            foreach (var division in primeEvent.Events)
            {
                var validator = invoiceDivisionValidator.Validate(primeEvent.Events.First());
                if (validator.IsValid)
                    return false;
            }
            return true;
        }
    }
}