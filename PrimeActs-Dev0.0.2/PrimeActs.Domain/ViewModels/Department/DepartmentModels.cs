#region

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace PrimeActs.Domain.ViewModels.Department
{
    public class DepartmentEditModel
    {
        public Guid DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentCode { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public Guid RelatedDivisionId { get; set; }
        public string RelatedDivisionName { get; set; }
        

        public override bool Equals(object obj)
        {
            DepartmentEditModel fooItem = obj as DepartmentEditModel;

            return fooItem != null && fooItem.DepartmentId == this.DepartmentId;
        }

        public override int GetHashCode()
        {
            return this.DepartmentId.GetHashCode();
        }
    }

    public class DepartmentPagingModel
    {
        public ResultList<DepartmentEditModel> DepartmentEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SearchObject
    {
        public string DepartmentName { get; set; }
    }
}