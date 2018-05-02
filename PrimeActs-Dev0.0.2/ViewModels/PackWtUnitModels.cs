#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class PackWtUnitEditModel
    {
        public Guid PackWtUnitID { get; set; }
        public string WtUnit { get; set; }
        public decimal? KgMultiple { get; set; }
    }

    public class PackWtUnitViewModel : PackWtUnitEditModel
    {
    }
}