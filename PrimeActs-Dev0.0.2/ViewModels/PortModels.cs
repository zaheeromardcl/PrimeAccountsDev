#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class PortEditModel
    {
        public Guid PortID { get; set; }
        public string PortName { get; set; }
        public string PortCode { get; set; }
    }

    public class PortViewModel : PortEditModel
    {
    }
}