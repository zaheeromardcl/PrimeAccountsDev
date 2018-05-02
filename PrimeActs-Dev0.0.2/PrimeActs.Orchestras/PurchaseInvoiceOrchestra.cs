using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.PurchaseInvoice;
using PrimeActs.Infrastructure.BaseEntities;
using SearchObject = PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject;

namespace PrimeActs.Orchestras
{
    public class PurchaseInvoiceOrchestra : IPurchaseInvoiceOrchestra
    {
        private readonly IPurchaseInvoiceService _purchaseInvoiceService;
        private readonly IPurchaseInvoiceItemService _purchaseInvoiceItemService;
        private readonly ISupplierDepartmentService _supplierDepartmentService;
        private readonly INoteService _noteService;
        private readonly string _serverCode;
        private ApplicationUser _principal;

        public PurchaseInvoiceOrchestra(IPurchaseInvoiceService purchaseInvoiceService, ISetupLocalService setupLocalService, ISupplierDepartmentService supplierDepartmentService, IPurchaseInvoiceItemService purchaseInvoiceItemService, INoteService noteService)
        {
            _purchaseInvoiceService = purchaseInvoiceService;
            _purchaseInvoiceItemService = purchaseInvoiceItemService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _supplierDepartmentService = supplierDepartmentService;
            _noteService = noteService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public PurchaseInvoiceItemModel CreatePurchaseInvoiceItem(PurchaseInvoiceItemModel model)
        {
            try
            {
                var item = ApplyChanges(model);

                item.ObjectState = ObjectState.Added;
                item.CreatedBy = _principal.Id;
                item.CreatedDate = DateTime.Now;

                _purchaseInvoiceItemService.Insert(item);

                model.PurchaseInvoiceItemID = item.PurchaseInvoiceItemID;
                model.CreatedBy = item.CreatedBy.Value;
                model.CreatedDate = item.CreatedDate.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Purchase Invoice Item failed", ex);
            }

            return model;
        }

        public PurchaseInvoiceItemModel UpdatePurchaseInvoiceItem(PurchaseInvoiceItemModel model)
        {
            try
            {
                var item = ApplyChanges(model);

                item.ObjectState = ObjectState.Modified;
                item.UpdatedBy = _principal.Id;
                item.UpdatedDate = DateTime.Today;

                _purchaseInvoiceItemService.Update(item);
                model.UpdatedBy = item.UpdatedBy.Value;
                model.UpdatedDate = item.UpdatedDate.ToString();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Update of Purchase Invoice Item failed", ex);
            }

            return model;
        }

        public void RemovePurchaseItem(Guid id)
        {
            try
            {
                _purchaseInvoiceItemService.Delete(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Delete of Purchase Invoice Item failed", ex);
            }
        }

        // only for admin
        public void UpdatePurchaseInvoiceStatus(Guid purchaseInvoiceID, PurchaseInvoiceStatus status)
        {
            var purchaseInvoice = _purchaseInvoiceService.PurchaseInvoiceById(purchaseInvoiceID);

            purchaseInvoice.Status = (int)(status);
            purchaseInvoice.UpdatedBy = _principal.Id;
            purchaseInvoice.UpdatedDate = DateTime.Now;

            _purchaseInvoiceService.Update(purchaseInvoice);
        }

        public void PurchaseInvoiceItemForReview(PurchaseInvoiceItemModel model)
        {
            var purchaseInvoice = _purchaseInvoiceService.PurchaseInvoiceById(model.PurchaseInvoiceID);

            int status;

            if (purchaseInvoice.Status == (int)PurchaseInvoiceStatus.Total || purchaseInvoice.Status == (int)PurchaseInvoiceStatus.TotalAndPendingQuery)
            {
                status = (int)PurchaseInvoiceStatus.TotalAndPendingQuery;
            }
            else
            {
                status = (int)PurchaseInvoiceStatus.PendingQuery;
            }
            purchaseInvoice.Status = status;
            purchaseInvoice.UpdatedBy = _principal.Id;
            purchaseInvoice.UpdatedDate = DateTime.Now;

            var newText = "ItemID:" + model.PurchaseInvoiceItemID + " Quantity: " + model.Quantity + " Estimated Purchase Cost: " + model.EstimatedPurchaseCost + " Total: " + model.TotalPrice + " ";
            // insert/update note
            if (purchaseInvoice.NoteID != null)
            {
                //var note = _noteService.NoteById(purchaseInvoice.NoteID.Value);

                purchaseInvoice.Note.NoteText = purchaseInvoice.Note.NoteText + newText;
                purchaseInvoice.Note.ObjectState = ObjectState.Modified;

                //                _noteService.Update(note);
            }
            else
            {
                var note = new Note();
                note.NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
                note.NoteText = newText;
                note.NoteDescription = "Pending Query";
                note.ObjectState = ObjectState.Added;
                _noteService.Insert(note);
                purchaseInvoice.NoteID = note.NoteID;
            }

            _purchaseInvoiceService.Update(purchaseInvoice);
        }

        public ResultList<PurchaseInvoiceModel> GetPurchaseInvoices(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var list = _purchaseInvoiceService.GetPurchaseInvoices(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<PurchaseInvoiceModel>(
                    list != null ? list.Select(BuildPurchaseInvoiceModel).ToList() : null, queryOptions);
        }

        public PurchaseInvoicePagingModel GetPurchaseInvoicePagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var purchaseInvoicePagingModel = new PurchaseInvoicePagingModel();

            List<PurchaseInvoice> purchaseInvoices;

            try
            {
                purchaseInvoices = _purchaseInvoiceService.GetPurchaseInvoices(queryOptions, searchObject, out totalCount);
            }
            catch (Exception)
            {
                purchaseInvoices = new List<PurchaseInvoice>();
            }

            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            //This line gets rid of items!! Fix the error
            var result = new ResultList<PurchaseInvoiceModel>(purchaseInvoices.Select(BuildPurchaseInvoiceModel).ToList(),
                queryOptions);
            purchaseInvoicePagingModel.PurchaseInvoiceEditModels = result;
            purchaseInvoicePagingModel.SearchObject = new PrimeActs.Domain.ViewModels.PurchaseInvoice.SearchObject
            {
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null
            };
            return purchaseInvoicePagingModel;
        }

        private PurchaseInvoiceModel BuildPurchaseInvoiceModel(PurchaseInvoice entity)
        {
            var status = entity.Status == null ? "null" : ((PurchaseInvoiceStatus)entity.Status).ToString();
            var subTotal = entity.PurchaseInvoiceItems.Sum(purchaseInvoiceItem => purchaseInvoiceItem.TotalPrice * (purchaseInvoiceItem.PurchaseInvoiceItemQuantity ?? 0));
            return new PurchaseInvoiceModel()
            {
                PurchaseInvoiceID = entity.PurchaseInvoiceID,
                PurchaseInvoiceReference = entity.PurchaseInvoiceReference.ToString(),
                CreatedDateText = entity.CreatedDate.ToString(),
                SubTotal = subTotal,
                Status = status,
                PreviousStatus = status,
                SupplierInvoiceAmount = entity.Total,
                SupplierCode = entity.SupplierDepartment.Supplier.SupplierCode
            };
        }

        public PurchaseInvoiceDetailsViewModel GetPurchaseInvoiceDetailsViewModel(Guid id)
        {
            var entity = _purchaseInvoiceService.PurchaseInvoiceById(id);
            var status = entity.Status == null ? "null" : ((PurchaseInvoiceStatus)entity.Status).ToString();

            var items = entity.PurchaseInvoiceItems.Select(
                    pii => new PurchaseInvoiceItemModel()
                    {
                        PurchaseInvoiceItemID = pii.PurchaseInvoiceItemID,
                        Quantity = (pii.PurchaseInvoiceItemQuantity ?? 0),

                        Description = pii.ConsignmentItem.Brand,
                        CurrencyAmount = pii.CurrencyAmount,

                        TotalPrice = pii.TotalPrice
                    }).ToList();

            string noteText = "";

            if (entity.Note != null)
            {
                noteText = entity.Note.NoteText;

                var separator = new string[] { "ItemID:" };
                var arr = noteText.Split(separator, StringSplitOptions.None);

                //var counter = 1;
                //foreach (var s in arr)
                //{
                //    if (s.Length > 30)
                //    {
                //        var itemID = s.Substring(0, 36);
                //        var item = items.FirstOrDefault(i => i.PurchaseInvoiceItemID == Guid.Parse(itemID));
                //        if (item != null)
                //        {
                //            item.Alias = "#" + counter + " ";
                //            noteText = noteText.Replace("ItemID:" + itemID, System.Environment.NewLine + item.Alias);
                //            counter++;
                //        }
                //    }
                //}

                foreach (var s in arr)
                {
                    if (s.Length > 30)
                    {
                        var itemID = s.Substring(0, 36);
                        var item = items.FirstOrDefault(i => i.PurchaseInvoiceItemID == Guid.Parse(itemID));
                        if (item != null)
                        {
                            var itemNote = s.Substring(36);
                            if (item.Notes == null)
                                item.Notes = new List<string>();
                            item.Notes.Add(itemNote);
                        }
                    }
                }
            }

            var viewModel = new PurchaseInvoiceDetailsViewModel
            {
                PurchaseInvoiceID = entity.PurchaseInvoiceID,
                PurchaseInvoiceReference = entity.PurchaseInvoiceReference,
                PurchaseInvoiceDate = entity.PurchaseInvoiceDate.ToShortDateString(),
                SupplierDepartmentName = entity.SupplierDepartment.SupplierDepartmentName,
                Address = entity.Address == null ? new AddressModels() : new AddressModels()
                {
                    AddressLine1 = entity.Address.AddressLine1 ?? "",
                    AddressLine2 = entity.Address.AddressLine2 ?? "",
                    AddressLine3 = entity.Address.AddressLine3 ?? "",
                    CountyCity = entity.Address.CountyCity ?? "",
                    PostalTown = entity.Address.PostalTown ?? "",
                    Postcode = entity.Address.Postcode ?? "",
                },
                CreatedDate = entity.CreatedDate.Value,
                CreatedBy = entity.CreatedBy.Value,
                Total = entity.PurchaseInvoiceItems.Sum(purchaseInvoiceItem => purchaseInvoiceItem.TotalPrice * (purchaseInvoiceItem.PurchaseInvoiceItemQuantity ?? 0)),
                SubTotal = entity.PurchaseInvoiceItems.Sum(purchaseInvoiceItem => purchaseInvoiceItem.TotalPrice * (purchaseInvoiceItem.PurchaseInvoiceItemQuantity ?? 0)),
                Status = status,
                SupplierInvoiceAmount = entity.Total,
                PurchaseInvoiceItems = items,
                NoteText = noteText
            };

            return viewModel;
        }

        public PurchaseInvoiceModel CreatePurchaseInvoice(PurchaseInvoiceModel model)
        {
            var purchaseInvoice = ApplyChanges(model, true);

            model.ServerCode = _serverCode;
            purchaseInvoice.ObjectState = ObjectState.Added;

            purchaseInvoice.CreatedBy = _principal.Id;
            purchaseInvoice.CreatedDate = DateTime.Now;
            model.PurchaseInvoiceID = purchaseInvoice.PurchaseInvoiceID;
            model.UpdatedBy = _principal.Id;
            model.UpdatedDate = DateTime.Now;

            purchaseInvoice.CreatedDate = DateTime.Now;

            _purchaseInvoiceService.Insert(purchaseInvoice);

            return model;
        }

        public PurchaseInvoiceModel UpdatePurchaseInvoice(PurchaseInvoiceModel model)
        {
            var purchaseInvoice = ApplyChanges(model, false);

            purchaseInvoice.ObjectState = ObjectState.Modified;
            purchaseInvoice.UpdatedBy = _principal.Id;
            purchaseInvoice.UpdatedDate = DateTime.Now;
            model.UpdatedBy = purchaseInvoice.UpdatedBy.Value;
            model.UpdatedDate = DateTime.Now;

            _purchaseInvoiceService.Update(purchaseInvoice);

            return model;
        }

        public PurchaseInvoiceModel GetPurchaseInvoiceEditModel(Guid purchaseInvoiceId)
        {
            return GetPurchaseInvoiceDetailsViewModel(purchaseInvoiceId);

        }

        private PurchaseInvoice ApplyChanges(PurchaseInvoiceModel model, bool isItANewPruchaseInvoice)
        {
            var date = DateTime.Parse(model.PurchaseInvoiceDate);
            var addressID = _supplierDepartmentService.SupplierDepartmentById(model.SupplierDepartmentId).SupplierLocations.FirstOrDefault().AddressID;
            //    var purchaseLedgerEntryID = System.Guid.Parse("00760000-0002-0000-0006-827344180700");
            var purchaseInvoiceID = Guid.Empty != model.PurchaseInvoiceID
                ? model.PurchaseInvoiceID
                : new Guid(model.UploadFolder);
            var divisionId = _principal.DivisionId.Value;
            var supplierDepartmentId = model.SupplierDepartmentId;
            var purchaseInvoiceReference = model.PurchaseInvoiceReference;
            var createdDate = DateTime.Now;
            var updatedDate = DateTime.Now;
            var updatedBy = _principal.Id;
            var createdBy = _principal.Id;
            var isSaved = model.IsSaved;

            // calculate the total of purchase invoice items and compare it with the Total of the purchase invoice
            PurchaseInvoice purchaseInvoice;
            if (!isItANewPruchaseInvoice)
            {
                var status = (int)PurchaseInvoiceStatus.OK;
                //this is not a new purchase invoice
                purchaseInvoice = _purchaseInvoiceService.PurchaseInvoiceById(purchaseInvoiceID);
                var totalOfItems = purchaseInvoice.PurchaseInvoiceItems.Sum(
                    purchaseInvoiceItem => purchaseInvoiceItem.TotalPrice * (purchaseInvoiceItem.PurchaseInvoiceItemQuantity ?? 0));

                if (model.SupplierInvoiceAmount != totalOfItems)
                {
                    if (purchaseInvoice.Status == (int)PurchaseInvoiceStatus.PendingQuery || purchaseInvoice.Status == (int)PurchaseInvoiceStatus.TotalAndPendingQuery)
                    {
                        status = (int)PurchaseInvoiceStatus.TotalAndPendingQuery;
                    }
                    else
                    {
                        status = (int)PurchaseInvoiceStatus.Total;
                    }
                }

                purchaseInvoice.PurchaseInvoiceReference = purchaseInvoiceReference;
                purchaseInvoice.PurchaseInvoiceDate = date;
                purchaseInvoice.Total = model.SupplierInvoiceAmount.Value;
                purchaseInvoice.Status = status;
                purchaseInvoice.UpdatedDate = updatedDate;
                purchaseInvoice.UpdatedBy = _principal.Id;

                purchaseInvoice.SupplierDepartmentID = supplierDepartmentId;
            }
            else
            {
                purchaseInvoice = new PurchaseInvoice
                {
                    PurchaseInvoiceID = purchaseInvoiceID,
                    PurchaseInvoiceReference = purchaseInvoiceReference,
                    PurchaseInvoiceDate = date,
                    ServerCode = _serverCode,
                    // CurrencyRate = decimal.Parse(model.CurrencyRate),
                    NoteID = null,
                    SupplierDepartmentID = supplierDepartmentId,
                    AddressID = addressID,
                    // PurchaseLedgerEntryID = purchaseLedgerEntryID,
                    DivisionID = divisionId,

                    CreatedDate = createdDate,
                    CreatedBy = _principal.Id,
                    UpdatedDate = updatedDate,
                    UpdatedBy = _principal.Id,

                    Total = model.SupplierInvoiceAmount.Value,
                    Status = 1
                };
            }

            return purchaseInvoice;
        }

        private PurchaseInvoiceItem ApplyChanges(PurchaseInvoiceItemModel model)
        {
            var purchaseInvoiceItemId = Guid.Empty != model.PurchaseInvoiceItemID ? model.PurchaseInvoiceItemID : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]);
            var purchaseInvoiceId = model.PurchaseInvoiceID;
            var totalPrice = model.TotalPrice ?? 0;
            var quantity = model.Quantity;
            var consignmentItemId = model.ConsignmentItemID;

            var createdDate = !string.IsNullOrEmpty(model.CreatedDate) ? DateTime.Parse(model.CreatedDate) : (DateTime?)null;

            return new PurchaseInvoiceItem
            {
                PurchaseInvoiceItemID = purchaseInvoiceItemId,
                PurchaseInvoiceID = purchaseInvoiceId,
                TotalPrice = totalPrice,
                PurchaseInvoiceItemQuantity = quantity,
                ConsignmentItemID = consignmentItemId,
                TransactionTaxRateID = Guid.Parse("76000100-0000-0070-9204-000068336078"),
                PurchaseInvoiceItemDescription = model.Description,
                CreatedDate = createdDate,
                CreatedBy = model.CreatedBy,
                PurchaseInvoiceItemChargeTypeID = model.PurchaseInvoiceItemChargeTypeID
            };
        }
    }
}
