#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
//using SearchObject = PrimeActs.Domain.ViewModels.BankAccount;

#endregion
//API Controller - to get list of customer bank accounts on click of show/hide panel.

namespace PrimeActs.UI.Controllers.API
{
    public class ContactController : ApiController
    {
        private readonly IContactOrchestra _contactOrchestra;
        private readonly IUnitOfWorkAsync _unitofWork;

        //TODO: create IBankAccountOrchestra
        //TODO: create BankAccountOrchestra
        public ContactController(IContactOrchestra contactOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _contactOrchestra = contactOrchestra;

            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Contact> GetAllCustomerDepartmentContacts(Guid CustomerDepartmentID)
        {
            var contacts = _contactOrchestra.GetAllCustomerDepartmentContacts(CustomerDepartmentID);
            return contacts;
        }
        
    }
}