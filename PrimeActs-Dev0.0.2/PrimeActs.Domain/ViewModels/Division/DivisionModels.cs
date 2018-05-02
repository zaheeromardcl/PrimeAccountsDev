#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PrimeActs.Domain.ViewModels.Department;

#endregion

namespace PrimeActs.Domain.ViewModels.Division
{
    public class DivisionEditModel
    {
        public Guid DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid RelatedCompanyId { get; set; }
        public string RelatedCompanyName { get; set; }
    }

    public class DivisionModel : DivisionEditModel
    {
        public HashSet<DepartmentEditModel> Departments { get; set; }

        public DivisionModel()
        {
            Departments = new HashSet<DepartmentEditModel>();
        }
    }

    public class DivisionPagingModel
    {
        public ResultList<DivisionEditModel> DivisionEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string DivisionName { get; set; }
    }
}