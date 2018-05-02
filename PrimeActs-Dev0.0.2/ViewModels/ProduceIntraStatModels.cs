#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class ProduceIntraStatEditModel
    {
        public Guid ProduceIntraStatID { get; set; }
        public string IntraStatCode { get; set; }
        public bool IsActive { get; set; }
    }

    public class ProduceIntraStatViewModel : ProduceIntraStatEditModel
    {
    }
}