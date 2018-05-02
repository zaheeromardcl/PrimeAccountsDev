using System;
using System.Collections.Generic;

namespace PrimeActs.Domain.ViewModels
{
    public class CustomerDepartmentEditModel
    {
        public Guid CustomerDepartmentID { get; set; }
        public string x_CustomerDepartmentID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public Guid CustomerID { get; set; }
        public string CustomerCompanyName { get; set; }
        public string CustomerDepartmentEmailAddress { get; set; }
        public string EmailAddress { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public string InvoiceFrequency { get; set; }
        public Guid? SalesPersonUserID { get; set; }
        public string SalesPersonName { get; set; }
        public Guid CustomerTypeID { get; set; }
        public string CustomerTypeName { get; set; }
        public int? CreditTerms { get; set; }
        public decimal? CreditLimit { get; set; }
        public Guid? RebateCustomerDepartmentID { get; set; }
        public string RebateCustomerCompany_DepartmentName { get; set; }
        public int RebateType { get; set; }
        public decimal? RebateRate { get; set; }
        public Nullable<Guid> NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        public string FactorRef { get; set; }
        public string TransactionTaxReference { get; set; }
        public Guid InvoiceCustomerLocationID { get; set; }
        public string InvoiceCustomerLocation_Name { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public List<string> SelectedLocationIds { get; set; }
        public List<LbxViewModel> LbxLocationOptions { get; set; }
        public bool ItemAdding { get; set; }
        public bool ItemDeleting { get; set; }
    }

    public class CustomerDepartmentViewModel : CustomerDepartmentEditModel
    {
        public CustomerDepartmentEditModel CustomerDepartmentEditModel { get; set; }
        public List<CustomerDepartmentEditModel> CustomerDepartmentEditModels { get; set; }
    }

    public class CustomerDepartmentEditModelModels
    {
        public List<CustomerDepartmentEditModel> CustomerDepartments { get; set; }
    }
}
