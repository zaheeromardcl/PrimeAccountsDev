using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Users;

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierEditModel
    {
        public SupplierEditModel()
        {
            SupplierItems = new List<SupplierItemEditModel>();
        }

        public Guid SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierCompanyName { get; set; }
        public string Comments { get; set; }
        public bool? IsFactor { get; set; }
        public bool? IsHaulier { get; set; }
        public Guid? ParentSupplierID { get; set; }
        public string ParentSupplierName { get; set; }
        public Guid? NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        public Guid? CompanyID { get; set; }
        public string CompanyName { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string FullName { get; set; }
        public List<SupplierLocationModel> SupplierLocations { get; set; }
        public List<SupplierContactEditModel> SupplierContacts { get; set; }
        public List<SupplierDepartmentEditModel> SupplierDepartments { get; set; }
        public List<SupplierItemEditModel> SupplierItems { get; set; }
    }

    public class SupplierPagingModel
    {
        public ResultList<SupplierEditModel> SupplierEditModels { get; set; }
        public SearchObject SearchObject { get; set; }
    }

    public class SupplierItemPagingModel
    {
        public ResultList<SupplierItemEditModel> SupplierItemEditModels { get; set; }
    }

    public class SupplierItemEditModel
    {
        public Guid SupplierID { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierCompanyName { get; set; }
        public string ParentSupplierID { get; set; }
        public string IsHaulier { get; set; }
        public string IsFactor { get; set; }
        public string CompanyID { get; set; }
        public string NoteID { get; set; }
        public Guid UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDirty { get; set; }
    }
}

//public class SupplierDeptViewModel : SupplierEditModel
//{
//    public string SupplierDepartmentName { get; set; }
//    public string Commision { get; set; }
//    public string Handling { get; set; }
//    public Guid SupplierDeptID { get; set; }
//}
/*
public partial class SearchObject
{
    public string SupplierCode { get; set; }
    public string SupplierCompanyName { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
*/
