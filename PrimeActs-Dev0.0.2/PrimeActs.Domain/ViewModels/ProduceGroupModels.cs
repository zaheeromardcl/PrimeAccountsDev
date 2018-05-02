#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeActs.Domain.ViewModels.Department;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class ProduceGroupEditModel
    {
        public Guid ProduceGroupID { get; set; }


        [Display(Name = "Related Division")]
        public DepartmentEditModel RelatedDivision { get; set; }

        public string SelectedDivision { get; set; }

        public string ProduceGroupName { get; set; }
        public string ProduceGroupCode { get; set; }

        public bool IsActive { get; set; }
    }

    public class ProduceGroupViewModel : ProduceGroupEditModel
    {
        [Display(Name = "Division")]
        public ICollection<dropdownlistModel> DivisionList { get; set; }
    }
}