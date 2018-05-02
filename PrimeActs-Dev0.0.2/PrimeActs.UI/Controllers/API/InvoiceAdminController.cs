using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Orchestras;

namespace PrimeActs.UI.Controllers.API
{
    public class InvoiceAdminController : ApiController
    {
        private readonly IPurchaseInvoiceOrchestra _purchaseInvoiceOrchestra;
        private IUnitOfWorkAsync _unitofWork;

        public InvoiceAdminController(IPurchaseInvoiceOrchestra purchaseInvoiceOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _purchaseInvoiceOrchestra = purchaseInvoiceOrchestra;
            _unitofWork = unitofWork;
        }

        [PrimeActsAuthorizeAttributeAPI(OperationKey = "InvoiceAdmin-UpdatePurchaseInvoice")]
        [HttpPost]
        public void UpdatePurchaseInvoice(Guid id, string status)
        {
            var signedInUSer = User.Identity.GetApplicationUser();
            if (User.Identity.IsAuthenticated && signedInUSer.IsInvoiceAdminAuthenticated)
            {
                _purchaseInvoiceOrchestra.Initialize(signedInUSer);

                PurchaseInvoiceStatus status2;
                if (Enum.TryParse<PurchaseInvoiceStatus>(status, out status2))
                {
                    if (id != Guid.Empty && status2 != PurchaseInvoiceStatus.OK)
                    {
                        _purchaseInvoiceOrchestra.UpdatePurchaseInvoiceStatus(id, status2);

                        //update the status of this purchase Invoice
                        _unitofWork.SaveChanges();
                    }
                }
            }
        }
    }
}
