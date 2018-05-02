using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

namespace PrimeActs.UI.Controllers.API
{
    public class PurchaseInvoiceController : ApiController
    {
        private readonly IPurchaseInvoiceOrchestra _purchaseInvoiceOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public PurchaseInvoiceController(IPurchaseInvoiceOrchestra purchaseInvoiceOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _purchaseInvoiceOrchestra = purchaseInvoiceOrchestra;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public ResultList<PurchaseInvoiceModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject searchObject)
        {
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "LASTMONTH";
            var purchaseInvoices = _purchaseInvoiceOrchestra.GetPurchaseInvoices(queryOptions, searchObject);
            return purchaseInvoices;
        }

        [HttpPost]
        [PrimeActsAuthorize(OperationKey = "PurchaseInvoice-CreatePurchaseInvoice")]
        public PurchaseInvoiceModel CreatePurchaseInvoice(PurchaseInvoiceModel model)
        {
            PopulateUser();
            if (model.PurchaseInvoiceID == null || model.PurchaseInvoiceID == Guid.Empty)
            {
                _purchaseInvoiceOrchestra.CreatePurchaseInvoice(model);
            }
            else
            {
                _purchaseInvoiceOrchestra.UpdatePurchaseInvoice(model);
            }
            
            _unitofWork.SaveChanges();
            return _purchaseInvoiceOrchestra.GetPurchaseInvoiceEditModel(model.PurchaseInvoiceID);
        }

        [HttpPost]
        public PurchaseInvoiceItemModel SavePurchaseInvoiceItem(PurchaseInvoiceItemModel model)
        {
            PopulateUser();
            if (model.PurchaseInvoiceItemID == Guid.Empty)
            {
                _purchaseInvoiceOrchestra.CreatePurchaseInvoiceItem(model);
            }
            else
            {
                _purchaseInvoiceOrchestra.UpdatePurchaseInvoiceItem(model);
            }
            
            _unitofWork.SaveChanges();
            return model;
        }
        
        [HttpPost]
        public PurchaseInvoiceItemModel PurchaseInvoiceItemForReview(PurchaseInvoiceItemModel model)
        {
            PopulateUser();

            _purchaseInvoiceOrchestra.PurchaseInvoiceItemForReview(model);
            
            _unitofWork.SaveChanges();
            return model;
        }

        [HttpPost]
        public HttpResponseMessage RemovePurchaseInvoiceItem(Guid? id)
        {
            if (id != null && id != Guid.Empty)
            {
                _purchaseInvoiceOrchestra.RemovePurchaseItem(id.Value);
                _unitofWork.SaveChanges();
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public void PopulateUser()
        {
            var applicationUser = User.Identity.GetApplicationUser();
            _purchaseInvoiceOrchestra.Initialize(applicationUser);
        }
    }
}