using System;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using SearchObject = PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject;

namespace PrimeActs.Orchestras
{
    public interface IPurchaseInvoiceOrchestra
    {
        ResultList<PurchaseInvoiceModel> GetPurchaseInvoices(QueryOptions queryOptions, SearchObject searchObject);
        PurchaseInvoicePagingModel GetPurchaseInvoicePagingModel(QueryOptions queryOptions, SearchObject searchObject);
        PurchaseInvoiceDetailsViewModel GetPurchaseInvoiceDetailsViewModel(Guid id);
        PurchaseInvoiceModel CreatePurchaseInvoice(PurchaseInvoiceModel model);
        PurchaseInvoiceModel UpdatePurchaseInvoice(PurchaseInvoiceModel model);
        PurchaseInvoiceModel GetPurchaseInvoiceEditModel(Guid purchaseInvoiceId);
        void Initialize(ApplicationUser applicationUser);
        PurchaseInvoiceItemModel CreatePurchaseInvoiceItem(PurchaseInvoiceItemModel model);
        PurchaseInvoiceItemModel UpdatePurchaseInvoiceItem(PurchaseInvoiceItemModel model);
        void RemovePurchaseItem(Guid id);
        void UpdatePurchaseInvoiceStatus(Guid purchaseInvoiceID, PurchaseInvoiceStatus status);
        void PurchaseInvoiceItemForReview(PurchaseInvoiceItemModel model);
    }
}
