using System;
using System.Collections.Generic;
using PrimeActs.Domain;

//TODO: amend to build bank account service.
namespace PrimeActs.Data.Service
{
    public interface IContactService : IService<Contact>
    {

        //List<BankAccount> GetAllBankAccounts();
        List<Contact> GetAllCustomerDepartmentContacts(Guid customerDepartmentID);
        Contact GetContactById(Guid contactID);
        //void  RefreshCache();
        //void CheckCache();
    }
}
