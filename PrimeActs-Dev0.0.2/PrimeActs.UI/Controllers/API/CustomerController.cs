using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.UI.Controllers.API
{
    public class CustomerController : ApiController
    {
        private readonly ICustomerOrchestra _customerOrchestra;
        private readonly ICustomerContactOrchestra _contactOrchestra;
        private readonly ICustomerLocationOrchestra _locationOrchestra;
        private readonly ICustomerDepartmentOrchestra _departmentOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerController(ICustomerOrchestra customerOrchestra,
            ICustomerDepartmentOrchestra departmentOrchestra,
            ICustomerLocationOrchestra locationOrchestra,
            ICustomerContactOrchestra contactOrchestra,
            IUnitOfWorkAsync unitofWork)
        {
            _customerOrchestra = customerOrchestra;
            _contactOrchestra = contactOrchestra;
            _locationOrchestra = locationOrchestra;
            _departmentOrchestra = departmentOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            var list = string.IsNullOrEmpty(search)
                ? null
                : _customerOrchestra.GetCustomerDepartmentModelsForAC(search)
                .Select(inc => new Autocomplete
                    {
                        Id = inc.CustomerDepartmentID.ToString(),
                        label = inc.CustomerDepartmentName,
                        value = inc.CustomerDepartmentID.ToString()
                    })
                .ToList();
            return list;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompleteForPC(string search)
        {
            var list = string.IsNullOrEmpty(search)
                ? null
                : _customerOrchestra.GetCustomerModelsForAC(search)
                .Select(customer => new Autocomplete
                    {
                        Id = customer.CustomerID.ToString(),
                        label = customer.CustomerCompanyName + " - " + customer.CustomerCode,
                        value = customer.CustomerCode,
                    })
                .ToList();
            return list;
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "Customer-CreateCustomer")]
        public JsonMessage CreateCustomer(CustomerEditModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.SelectMany(x => x.Value.Errors.Select(z => z.Exception));
            }
            PopulateUser();
            CustomerEditModel viewModel = _customerOrchestra.CreateCustomer(model);
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = viewModel.CustomerID.ToString(),
                Message = string.Format("Customer has been successfully created.")
            };
        }

        private void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _customerOrchestra.Initialize(applicationUser);
            _departmentOrchestra.Initialize(applicationUser);
            _locationOrchestra.Initialize(applicationUser);
            _contactOrchestra.Initialize(applicationUser);
        }

        [HttpGet]
        public CustomerEditModel GetSupplierModelBySupplierID(Guid customerID)
        {
            CustomerEditModel viewModel = _customerOrchestra.GetCustomerModelByCustomerID(customerID);
            return viewModel;
        }

        [HttpPost] // EditTab
        [PrimeActsAuthorize(OperationKey = "Customer-CustomerEdit")]
        public JsonMessage CustomerEdit(CustomerEditModel model)
        {
            PopulateUser();
            CustomerEditModel viewModel = _customerOrchestra.CreateCustomer(model);
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = viewModel.CustomerID.ToString(),
                Message = string.Format("Customer has been successfully updated.")
            };
        }
    }
}