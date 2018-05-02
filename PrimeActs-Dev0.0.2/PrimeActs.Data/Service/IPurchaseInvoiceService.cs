using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;

namespace PrimeActs.Data.Service
{
    public interface IPurchaseInvoiceService : IService<PurchaseInvoice>
    {
        List<PurchaseInvoice> GetPurchaseInvoices(Domain.ViewModels.QueryOptions queryOptions, SearchObject searchObject,
            out int totalCount);

        PurchaseInvoice PurchaseInvoiceById(Guid Id);
        List<PurchaseInvoice> GetPurchaseInvoicesByStatus(int status);
    }
}
