using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.Orchestras
{
    public class ContactOrchestra : IContactOrchestra
    {
        private readonly IContactService _contactService;


        public ContactOrchestra(IContactService contactService)
        {
            _contactService = contactService;
        
        }

        //To do: add Get GetGetAllCustomerDepartmentBankAccounts
        public List<Contact> GetAllCustomerDepartmentContacts(Guid CustomerDepartmentID)
        {
            var Contacts = _contactService.GetAllCustomerDepartmentContacts(CustomerDepartmentID);
            return Contacts;
        }
        
    }
}
