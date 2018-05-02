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
    public class SupplierLocationController : ApiController
    {
        private readonly ISupplierLocationOrchestra _supplierLocationOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public SupplierLocationController(ISupplierLocationOrchestra supplierLocationOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _supplierLocationOrchestra = supplierLocationOrchestra;
            _unitofWork = unitofWork;
        }
                
        [HttpGet]
        public List<SupplierLocationModel> ListOfSupplierLocations(List<Guid> supplierLocationIds)
        {
            var list = _supplierLocationOrchestra
                .GetSupplierLocationModels(supplierLocationIds);
            return list;
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "SupplierLocation-CreateLocations")]
        public JsonMessage CreateLocations(SupplierLocationEditModelList model)
        {
            PopulateUser();
            foreach (var supplierLocationEditModel in model.SupplierLocations)
            {
                _supplierLocationOrchestra
                    .CreateSupplierLocation(supplierLocationEditModel);
            }
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = new JavaScriptSerializer().Serialize(model),
                Message = string.Format("Locations have been successfully created/updated.")
            };
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _supplierLocationOrchestra.Initialize(applicationUser);
        }
    }
}
