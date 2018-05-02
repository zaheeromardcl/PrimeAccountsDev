#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain
{
    public class PrimeEvent
    {
        public PrimeEvent()
        {
            Events = new List<InvoiceEvent>();
        }
        public List<InvoiceEvent> Events { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
    }

    public class InvoiceEvent
    {
        public Division Division { get; set; }
        public string Username { get; set; }
        public string InvoiceType { get; set; }
        public string Period { get; set; }
    }
}