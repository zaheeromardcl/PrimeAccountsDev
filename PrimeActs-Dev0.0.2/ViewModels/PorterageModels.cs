#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class PorterageEditModel
    {
        public Guid PorterageID { get; set; }
        public string PorterageName { get; set; }
        public string PorterageCode { get; set; }
        public bool? IsActive { get; set; }
    }

    public class PorterageViewModel : PorterageEditModel
    {
    }
}