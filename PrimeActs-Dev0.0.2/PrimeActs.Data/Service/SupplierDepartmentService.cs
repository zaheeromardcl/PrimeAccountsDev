using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Cache;

namespace PrimeActs.Data.Service
{
    public class SupplierDepartmentService: Service<SupplierDepartment>, PrimeActs.Data.Service.ISupplierDepartmentService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<SupplierDepartment> _repository;
        private readonly object lockboject = new object();

        public SupplierDepartmentService(IRepositoryAsync<SupplierDepartment> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public SupplierDepartment SupplierDepartmentById(Guid Id)
        {
            var sdItem =
                _repository.Query(x => x.SupplierDepartmentID == Id)
                .Include(inc => inc.Supplier)
                .Include(inc => inc.Supplier.SupplierLocations)
                .Include(inc => inc.Supplier.SupplierLocations.Select(sl => sl.Address))
                .Include(inc => inc.Supplier.SupplierContacts)
                .Include(inc => inc.Supplier.SupplierContacts.Select(sc => sc.Contact))
                .Include(inc => inc.Consignments)
                .Include(inc => inc.SupplierBankAccounts)
                .Include(inc => inc.SupplierBankAccounts.Select(sba => sba.BankAccount))
                .Select().FirstOrDefault();
            return sdItem;
        }

        public SupplierDepartment SupplierDepartmentBasicById(Guid Id)
        {
            return _repository.Query(x => x.SupplierDepartmentID == Id)
                .Select()
                .FirstOrDefault();
        }

        // light query for reporting
        public SupplierDepartment SupplierDepartmentDetailsById(Guid Id)
        {
            return _repository.Query(x => x.SupplierDepartmentID == Id)
                .Include(inc => inc.Supplier)
                .Select()
                .FirstOrDefault();
        }

        private void CheckCache()
        {
            var type = typeof(SupplierDepartment).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var produce = new List<SupplierDepartment>();
                    foreach (
                        var entityType in
                            _repository.Query()                                
                                .Include(inc => inc.SupplierLocations)
                                .Select())
                    {
                        produce.Add(new SupplierDepartment
                        {
                            SupplierDepartmentID = entityType.SupplierDepartmentID,
                            SupplierDepartmentName = entityType.SupplierDepartmentName,
                            Commission = entityType.Commission,
                            Handling = entityType.Handling,
                            SupplierLocations = entityType.SupplierLocations
                        });
                    }
                    _cache.Set(type, produce);
                }
            }
        }
    }
}
