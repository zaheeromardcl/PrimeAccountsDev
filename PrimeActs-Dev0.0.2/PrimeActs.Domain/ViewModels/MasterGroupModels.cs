#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeActs.Domain.ViewModels.Department;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class MasterGroupEditModel
    {
        public Guid MasterGroupID { get; set; }

        [Display(Name = "Related Division")]
        public DepartmentEditModel RelatedDivision { get; set; }

        public string SelectedDivision { get; set; }


        public string MasterGroupName { get; set; }
        public string MasterGroupCode { get; set; }

        public bool IsActive { get; set; }
    }

    public class MasterGroupViewModel : MasterGroupEditModel
    {
        [Display(Name = "Division")]
        public ICollection<dropdownlistModel> DivisionList { get; set; }
    }
}