
﻿using System;
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
    public class SupplierContactController : ApiController
    {
        private readonly ISupplierContactOrchestra _supplierContactOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public SupplierContactController(ISupplierContactOrchestra supplierContactOrchestra,
            IUnitOfWorkAsync unitofWork)
        {
            _supplierContactOrchestra = supplierContactOrchestra;
            _unitofWork = unitofWork;
        }
                
        [HttpGet]
        public List<SupplierContactEditModel> ListOfSupplierContacts(List<Guid> supplierContactIds)
        {
            var list = _supplierContactOrchestra
                .GetSupplierContactModels(supplierContactIds);
            return list;
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "SupplierContact-CreateContacts")]
        public JsonMessage CreateContacts(SupplierContactEditModelList model)
        {
            PopulateUser();
            foreach (var supplierContactEditModel in model.SupplierContacts)
            {
                _supplierContactOrchestra
                    .CreateSupplierContact(supplierContactEditModel, null, null); // locaGuidList && depaGuidList
            }
            _unitofWork.SaveChanges();
            return new JsonMessage()
            {
                StatusId = 1,
                Data = new JavaScriptSerializer().Serialize(model),
                Message = string.Format("Contacts have been successfully created/updated.")
            };
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _supplierContactOrchestra.Initialize(applicationUser);
        }
    }
}
