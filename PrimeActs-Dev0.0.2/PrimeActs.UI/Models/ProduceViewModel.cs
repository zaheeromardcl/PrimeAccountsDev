#region

using System;

#endregion

namespace PrimeActs.UI.Models
{
    public class ProduceViewModel
    {
        public Guid ProduceID { get; set; }
        public Guid? MasterGroupID { get; set; }
        public Guid? DepartmentID { get; set; }
        public Guid? ProduceGroupID { get; set; }
        public Guid ProduceMasterID { get; set; }
        public string ProduceName { get; set; }
        public string ProduceCode { get; set; }
        public string DepartmentName { get; set; }
        public string ProduceGroupName { get; set; }
        public string ProduceGroupDescription { get; set; }
        public string MasterGroupDescription { get; set; }
    }
}