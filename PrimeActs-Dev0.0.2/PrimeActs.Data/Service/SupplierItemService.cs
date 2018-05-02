using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface ISupplierItemService : IService<SupplierItem>
    {
        List<SupplierItem> SupplierItemsBySupplierID(Guid id);
        List<SupplierItem> GetSupplierItemsOnly(Guid id);
    }

    public class SupplierItemService : Service<SupplierItem>, ISupplierItemService
    {

        private readonly IRepositoryAsync<SupplierItem> _repository;
        //private readonly IRepositoryAsync<SalesInvoiceItem> _repositorySalesInvoiceItem;

        public SupplierItemService(IRepositoryAsync<SupplierItem> repository) //, IRepositoryAsync<SalesInvoiceItem> repositorySalesInvoiceItem
            : base(repository)
        {
            _repository = repository;
            //_repositorySalesInvoiceItem = repositorySalesInvoiceItem;
        }

        public List<SupplierItem> SupplierItemsBySupplierID(Guid id)
        {
            var items = _repository.Query(fil => fil.SupplierID == id)
                .Include(inc => inc.Company)
                .Include(inc => inc.SupplierLocations)
                .Include(inc => inc.SupplierDepartments)
                .Include(inc => inc.SupplierContacts)
                .Select().ToList();
            return items;
        }

        public List<SupplierItem> GetSupplierItemsOnly(Guid id)
        {
            var items = _repository.Query(fil => fil.SupplierID == id)
                .Include(inc => inc.Note)
                .Include(inc => inc.Company)
                .Include(inc => inc.ParentSupplier)
                .Select().OrderBy(inc => inc.CreatedDate)
                .ToList();
            return items;
        }
    }
}
