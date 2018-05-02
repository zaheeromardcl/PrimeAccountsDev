using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class PurchaseInvoiceItemService : Service<PurchaseInvoiceItem>, IPurchaseInvoiceItemService
    {
        private readonly IRepositoryAsync<PurchaseInvoiceItem> _repository;

        public PurchaseInvoiceItemService(IRepositoryAsync<PurchaseInvoiceItem> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
