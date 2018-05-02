using System;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels.BankAccount;

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierDepartmentEditModel
    {
        public Guid SupplierDepartmentID { get; set; }
        public string x_SupplierDepartmentID { get; set; }
        public string SupplierDepartmentName { get; set; }
        public Guid SupplierID { get; set; }
        public string EmailAddress { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public Nullable<Guid> FactorSupplierDepartmentID { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
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
        public List<string> SelectedLocationIds { get; set; }
        public List<LbxViewModel> LbxLocationOptions { get; set; }
        public bool ItemAdding { get; set; }
        public bool ItemDeleting { get; set; }
    }

    public class SupplierDepartmentViewModel : SupplierDepartmentEditModel
    {
        public IEnumerable<SupplierLocationModel> SupplierLocations { get; set; }
        public IEnumerable<SupplierContactModel> SupplierContacts { get; set; }
        public IEnumerable<BankAccountModel> BankAccounts { get; set; }
    }

    public class SupplierDepartmentWithConsigmentViewModel : SupplierDepartmentEditModel
    {
        public IEnumerable<ConsignmentBasicViewModel> Consignments { get; set; }
    }

    public class SupplierDepartmentSearch
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }

    public class ConsignmentBasicViewModel
    {
        public string ConsignmentReference { get; set; }
        public decimal? TotalEstitamedPurcahseCost { get; set; }
        public string DepatchedDate { get; set; }
        public Guid ConsignmentID { get; set; }
    }

    public class SupplierDepartmentEditModelModels
    {
        public List<SupplierDepartmentEditModel> SupplierDepartments { get; set; }
    }

    public class SupplierContactModel
    {
        public ContactModel Contact { get; set; }
    }

    public class ContactModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string EmailAddress { get; set; }
    }
}
