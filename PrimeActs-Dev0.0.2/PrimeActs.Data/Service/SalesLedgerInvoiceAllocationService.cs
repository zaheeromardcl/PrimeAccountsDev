using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class SalesLedgerInvoiceAllocationService : Service<SalesLedgerInvoiceAllocation>, ISalesLedgerInvoiceAllocationService
    {
        private readonly IRepositoryAsync<SalesLedgerInvoiceAllocation> _repository;
        private readonly string _type;

        public SalesLedgerInvoiceAllocationService(IRepositoryAsync<SalesLedgerInvoiceAllocation> repository)
            : base(repository)
        {
            _repository = repository;
            _type = typeof(SalesLedgerInvoiceAllocation).FullName;
        }
    }
}
