#region

using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Domain.ViewModels;
using System.Linq;
using System.Data;
using System.Data.Entity;

#endregion

namespace PrimeActs.Data.Service
{
    public interface ISalesInvoiceItemService : IService<SalesInvoiceItem>
    {
        List<SalesInvoiceItem> SalesInvoiceItemsById(Guid id);
        SalesInvoiceItem SalesInvoiceItemById(Guid Id);
    }

    public class SalesInvoiceItemService : Service<SalesInvoiceItem>, ISalesInvoiceItemService
    {

        private readonly IRepositoryAsync<SalesInvoiceItem> _repository;
        private readonly string _type;

        public SalesInvoiceItemService(IRepositoryAsync<SalesInvoiceItem> repository)
            : base(repository)
        {
            _repository = repository;
            _type = typeof (SalesInvoiceItem).FullName;
        }
        public SalesInvoiceItem SalesInvoiceItemById(Guid Id)
        {
            return
                _repository.Query(fil => fil.SalesInvoiceItemID == Id)
                    .Include(inc => inc.SalesInvoice)
                    .Select().SingleOrDefault();
        }
        public List<SalesInvoiceItem> SalesInvoiceItemsById(Guid Id)
        {
            return
                _repository.Query(fil => fil.SalesInvoiceID == Id)
                    .Include(inc => inc.SalesInvoice)
                    .Select()
                    .ToList();
        }
        
    }
}