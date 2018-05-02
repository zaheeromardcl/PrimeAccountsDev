using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Customer
{
    public class CustomerDepartmentModel
    {
        public Guid CustomerDepartmentID { get; set; }
        public string x_CustomerDepartmentID { get; set; }
        public string CustomerDepartmentName { get; set; }
        public Guid CustomerID { get; set; }
        public string EmailAddress { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public Nullable<Guid> FactorSupplierDepartmentID { get; set; }
        public bool GivesRebate { get; set; }
        public decimal RebateAmount { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public bool IsTransactionTaxExempt { get; set; }
        public string TransactionTaxReference { get; set; }
        public int CreditTerm { get; set; }
        public decimal CreditLimit { get; set; }
        public Nullable<Guid> NoteID { get; set; }
        public string Notes { get; set; }
        public string NoteDescription { get; set; }
        public bool IsActive { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public List<string> SelectedLocationIds { get; set; }
        public List<LbxViewModel> LbxLocationOptions { get; set; }
    }

    public class CustomerDepartmentModelList
    {
        public List<CustomerDepartmentModel> CustomerDepartments { get; set; }
    }
}
