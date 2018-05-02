using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;

namespace PrimeActs.UI.Controllers.API
{
    public class SupplierController : ApiController
    {
        private readonly ISupplierOrchestra _supplierOrchestra;
        private readonly ISupplierContactOrchestra _contactOrchestra;
        private readonly ISupplierLocationOrchestra _locationOrchestra;
        private readonly ISupplierDepartmentOrchestra _departmentOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public SupplierController(ISupplierOrchestra supplierOrchestra,
            ISupplierDepartmentOrchestra departmentOrchestra,
            ISupplierLocationOrchestra locationOrchestra,
            ISupplierContactOrchestra contactOrchestra,
            IUnitOfWorkAsync unitofWork)
        {
            _supplierOrchestra = supplierOrchestra;
            _contactOrchestra = contactOrchestra;
            _locationOrchestra = locationOrchestra;
            _departmentOrchestra = departmentOrchestra;
            _unitofWork = unitofWork;
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoComplete(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _supplierOrchestra.GetSupplierDeptModelsForAC(search)
                    .Select(
                        supplier =>
                            new Autocomplete
                            {
                                //Id = supplier.SupplierID.ToString(), // changed to match Consignment, needs SupplierDepartmentID for FK Constraint
                                Id = supplier.SupplierDepartmentID.ToString(),
                                //label = supplier.SupplierCompanyName, DC
                                label = supplier.SupplierDepartmentName,
                                //value = supplier.SupplierCode DC
                                value = supplier.SupplierDepartmentName
                            })
                    .ToList();
        }

        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<Autocomplete> AutoCompleteForPS(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _supplierOrchestra.GetSupplierModelsForAC(search)
                    .Select(
                        supplier =>
                            new Autocomplete
                            {
                                Id = supplier.SupplierID.ToString(),
                                value = supplier.SupplierCode,
                                label = supplier.SupplierCompanyName + " - " + supplier.SupplierCode,
                            })
                    .ToList();
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "Supplier-CreateSupplier")]
        public JsonMessage CreateSupplier(SupplierEditModel model)
        {
            PopulateUser();
            SupplierEditModel viewModel = _supplierOrchestra.CreateSupplier(model);
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = viewModel.SupplierID.ToString(),
                Message = string.Format("Supplier has been successfully created.")
            };
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _supplierOrchestra.Initialize(applicationUser);
            _departmentOrchestra.Initialize(applicationUser);
            _locationOrchestra.Initialize(applicationUser);
            _contactOrchestra.Initialize(applicationUser);
        }

        [HttpGet]
        public SupplierEditModel GetSupplierModelBySupplierID(Guid supplierID)
        {
            SupplierEditModel viewModel = _supplierOrchestra.GetSupplierModelBySupplierID(supplierID);
            return viewModel;
        }

        [HttpPost] // EditTab
        [PrimeActsAuthorize(OperationKey = "Supplier-SupplierEdit")]
        public JsonMessage SupplierEdit(SupplierEditModel model)
        {
            PopulateUser();
            SupplierEditModel viewModel = _supplierOrchestra.CreateSupplier(model);
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = viewModel.SupplierID.ToString(),
                Message = string.Format("Supplier has been successfully updated.")
            };
        }
    }
}
