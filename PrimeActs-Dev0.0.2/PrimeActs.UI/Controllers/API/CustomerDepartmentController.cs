using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.UI.Controllers.API
{
    public class CustomerDepartmentController : ApiController
    {
        private readonly ICustomerDepartmentOrchestra _customerDepartmentOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public CustomerDepartmentController(ICustomerDepartmentOrchestra customerDepartmentOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _customerDepartmentOrchestra = customerDepartmentOrchestra;
            _unitofWork = unitofWork;
        }
             
        [HttpGet]
        public List<CustomerDepartment> GetCustomerDepartments(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                return _customerDepartmentOrchestra.GetCustomerDepartments((Guid) id);
            }
            return null;
        }

        [HttpGet]
        public CustomerDepartmentEditModel GetCustomerDepartmentEditModel(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                return _customerDepartmentOrchestra.GetCustomerDepartmentEditModel((Guid)id);
            }
            return null;
        }

        [HttpGet]
        public CustomerDepartmentViewModel GetCustomerDepartment(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                return _customerDepartmentOrchestra.GetCustomerDepartment((Guid)id);
            }
            return null;
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "CustomerDepartment-CreateDepartments")]
        public JsonMessage CreateDepartments(CustomerDepartmentEditModelModels model)
        {
            PopulateUser();
            foreach (var customerDepartmentEditModel in model.CustomerDepartments)
            {
                _customerDepartmentOrchestra
                    .CreateCustomerDepartment(customerDepartmentEditModel, new List<Guid>()); ////////////////
            }
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = new JavaScriptSerializer().Serialize(model),
                Message = string.Format("Departments have been successfully created/updated.")
            };
        }

        [HttpGet]
        public List<ItemViewModel> DDLRebateCustomerDepartment()
        {
            List<CustomerDepartmentViewModel> cdvmList = _customerDepartmentOrchestra
                .GetAllCustomerDepartments();
            var ivmList = new List<ItemViewModel>();
            foreach (var model in cdvmList)
            {
                var cdemList = model.CustomerDepartmentEditModels;
                var items = cdemList.Select(x => new ItemViewModel
                    {
                        Id = x.CustomerDepartmentID.ToString(),
                        label = x.RebateCustomerCompany_DepartmentName,
                        value = x.CustomerDepartmentID.ToString()
                    });
                    ivmList.AddRange(items);
            }
            return ivmList;
        }

        [HttpPost]
        public CustomerDepartmentViewModel UpdateRebate(CustomerDepartmentViewModel model)
        {
            PopulateUser();
            
            if (model != null)
            {
                _customerDepartmentOrchestra.UpdateRebate(model);
                _unitofWork.SaveChanges();
            }
            return model;
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _customerDepartmentOrchestra.Initialize(applicationUser);
        }
    }
}