#region

using System;
using System.Collections.Generic;
using System.Globalization;

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

    public class PorterageList
    {
        public List<Porterage> Porterage { get; set; }
    }
}