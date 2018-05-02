using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class PurchaseInvoiceFileService  : Service<PurchaseInvoiceFile>, IPurchaseInvoiceFileService
    {
        private readonly IRepositoryAsync<PurchaseInvoiceFile> _repository;

        public PurchaseInvoiceFileService(IRepositoryAsync<PurchaseInvoiceFile> repository)
            : base(repository)
        {
            _repository = repository;
        }
    }
}
