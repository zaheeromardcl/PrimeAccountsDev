#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels.BankAccount;

#endregion

namespace PrimeActs.Domain.ViewModels
{
    public class SupplierDepartmentEditModel
    {
        public Guid SupplierDepartmentID { get; set; }
        public string SupplierDepartmentName { get; set; }
        public Guid SupplierID { get; set; }
        public string EmailAddress { get; set; }
        public string CreditTerms { get; set; }
        public decimal Commission { get; set; }
        public decimal Handling { get; set; }
        public Guid FactorSupplierDepartmentID { get; set; }
        public Guid CountryID { get; set; }
        public string CountryName { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }

    public class SupplierDepartmentViewModel : SupplierDepartmentEditModel
    {
        public IEnumerable<SupplierLocationModel> SupplierLocations { get; set; }
        public IEnumerable<SupplierContactModel> SupplierContacts { get; set; }
        public IEnumerable<BankAccountModel> BankAccounts { get; set; }
    }

    public class SupplierContactModel
    {
        public ContactModel Contact { get; set; }
    }

    public class ContactModel
    {
        public string FirstName{ get; set; }
        public string LastName{ get; set; }
        public string Title{ get; set; }
        public string EmailAddress { get; set; }
    }
}