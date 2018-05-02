#region

using System;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierEditModel
    {
        public Guid SupplierID { get; set; }

        public string SupplierCompanyName { get; set; }
        public string SupplierCode { get; set; }
        public string Comments { get; set; }
        public Guid? ParentSupplierID { get; set; }


        public Guid? CompanyID { get; set; }

        public bool? IsActive { get; set; }
        public virtual Domain.Company Company { get; set; }
    }

    public class SupplierViewModel : SupplierEditModel
    {
    
    }
    


    public class SupplierDeptViewModel : SupplierEditModel
    {
        public string SupplierDepartmentName { get; set; }
        public string Commision { get; set; }
        public string Handling { get; set; }
        public Guid SupplierDeptID { get; set; }
    }
}


      
        