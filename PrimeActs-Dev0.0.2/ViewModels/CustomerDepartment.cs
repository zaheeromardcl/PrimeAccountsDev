#region

using System;
using System.Collections.Generic;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerDepartmentEditModel
    {
        public Guid CustomerDepartmentID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public Guid CustomerID { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerDepartmentEmailAddress { get; set; }
        public string CreditTerms { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public string InvoiceFrequency { get; set; }
        public Guid SalesPersonUserID { get; set;  }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int RebateType { get; set; }
        public decimal? RebateRate { get; set; }
        public Guid? RebateCustomerDepartmentID { get; set; }
    }

    public class CustomerDepartmentViewModel : CustomerDepartmentEditModel
    {
        public CustomerDepartmentEditModel CustomerDepartmentEditModel { get; set; }
        public List<CustomerDepartmentEditModel> CustomerDepartmentEditModels { get; set; }
    }
}