using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PrimeActs.Orchestras;
using PrimeActs.UI.Models;
using PrimeActs.Domain.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using PrimeActs.Orchestra;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.Invoice;



namespace PrimeActs.UI.Controllers.API
{
    public class InvoiceController : ApiController
    {
        private readonly IInvoiceOrchestra _invoiceOrchestra;
        private IUnitOfWorkAsync _unitofWork;
        private PrimeActsUserManager _userManager;

        public InvoiceController(IInvoiceOrchestra invoiceOrchestra, IUnitOfWorkAsync unitofWork)
        {
            _invoiceOrchestra = invoiceOrchestra;
            _unitofWork = unitofWork;
        }

        [HttpGet]
        public ResultList<InvoiceEditModel> Index([FromUri] QueryOptions queryOptions,
            [FromUri] PrimeActs.Domain.ViewModels.Invoice.SearchObject searchObject)
        {
            if (string.IsNullOrEmpty(searchObject.RecordsInDays))
                searchObject.RecordsInDays = "CURRENTMONTH";
            return _invoiceOrchestra.GetInvoices(queryOptions, searchObject);
        }
        
        [AcceptVerbs("GET", "POST")]
        [HttpGet]
        public List<SalesInvoice> GetAllCustomerDepartmentSalesInvoices(Guid CustomerDepartmentID)
        {

            var customerDepartmentSalesInvoices = _invoiceOrchestra.GetCustomerDepartmentSalesInvoices(CustomerDepartmentID);
            return customerDepartmentSalesInvoices;

        }
        
    }
}
