using PrimeActs.Domain;
using System.Collections.Generic;
using PrimeActs.Domain.ViewModels;
using System;

//TODO: amend to make into bank account orchestra
namespace PrimeActs.Orchestras
{
    public interface IContactOrchestra
    {
        List<Contact> GetAllCustomerDepartmentContacts(Guid CustomerDepartmentID);
    }
}



