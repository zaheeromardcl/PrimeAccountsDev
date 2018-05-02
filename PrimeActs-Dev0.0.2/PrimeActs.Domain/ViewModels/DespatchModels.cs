#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class DespatchEditModel
    {
        public Guid DespatchID { get; set; }

        public string DespatchName { get; set; }
        public string DespatchCode { get; set; }

        public Guid? CompanyID { get; set; }

        public virtual Domain.Company Company { get; set; }
    }
}