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
using PrimeActs.Domain.ViewModels;

namespace PrimeActs.UI.Controllers.API
{
    public class SupplierDepartmentController : ApiController
    {
        private readonly ISupplierDepartmentOrchestra _supplierDepartmentOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public SupplierDepartmentController(ISupplierDepartmentOrchestra supplierDepartmentOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _supplierDepartmentOrchestra = supplierDepartmentOrchestra;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public SupplierDepartmentViewModel GetSupplierDepartmentVm(Guid id)
        {
            return _supplierDepartmentOrchestra.GetSupplierDepartmentModel(id);
        }
        
        [HttpGet]
        public SupplierDepartmentWithConsigmentViewModel SupplierDepartmentWithConsignments(Guid id)
        {
            var searchObj = new SupplierDepartmentSearch()
            {
                From = DateTime.Today.AddMonths(-1),
                To = DateTime.Now
            };
            return _supplierDepartmentOrchestra.GetSupplierDepartmentWithConsignmentsModel(id, searchObj);
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "SupplierDepartment-CreateDepartments")]
        public JsonMessage CreateDepartments(SupplierDepartmentEditModelModels model)
        {
            PopulateUser();
            foreach (var supplierDepartmentEditModel in model.SupplierDepartments)
            {
                _supplierDepartmentOrchestra.CreateSupplierDepartment(supplierDepartmentEditModel, null); // - last param must be !null
            }
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = new JavaScriptSerializer().Serialize(model),
                Message = string.Format("Departments have been successfully created/updated.")
            };
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _supplierDepartmentOrchestra.Initialize(applicationUser);
        }
    }
}
