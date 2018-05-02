using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;

namespace PrimeActs.Orchestras
{
    public class SupplierOrchestra : ISupplierOrchestra
    {
        private readonly ISupplierService _supplierService;
        private readonly ISupplierItemService _supplierItemService;
        private readonly INoteService _noteService;
        private readonly ISupplierContactOrchestra _contactOrchestra;
        private readonly ISupplierLocationOrchestra _locationOrchestra;
        private readonly ISupplierDepartmentOrchestra _departmentOrchestra;
        private ApplicationUser _principal;
        private readonly string _serverCode;
        private IApplicationUserService _appUserService;

        public SupplierOrchestra(ISupplierService supplierService, ISupplierItemService supplierItemService,
            ISupplierLocationOrchestra locationOrchestra, ISupplierDepartmentOrchestra departmentOrchestra, ISupplierContactOrchestra contactOrchestra,
            INoteService noteService, IApplicationUserService appUserService,
            ISetupLocalService setupLocalService)
        {
            _supplierService = supplierService;
            _supplierItemService = supplierItemService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _departmentOrchestra = departmentOrchestra;
            _locationOrchestra = locationOrchestra;
            _contactOrchestra = contactOrchestra;
            _noteService = noteService;
            _appUserService = appUserService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public SupplierEditModel GetSupplierModelBySupplierID(Guid supplierID)
        {
            Supplier entity = _supplierService.GetSupplierByIdFromRepo(supplierID);
            SupplierEditModel supplierModel = entity != null
                ? BuildSupplierEditModel(entity)
                : null;
            return supplierModel;
        }

        public List<SupplierEditModel> GetSupplierModelsForAC(string search)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _supplierService.GetAllSuppliers()
                    .Where(x =>
                           x.SupplierCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) |
                           x.SupplierCompanyName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase))
                    .Select(BuildSupplierEditModelAC)
                    .ToList();
        }


        public List<SupplierDepartmentEditModel> GetSupplierDeptModelsForAC(string search)
        {
            List<SupplierDepartmentEditModel> supplierDepartmentEditModels = new List<SupplierDepartmentEditModel>();

            var suppliers = _supplierService.Query(x => x.SupplierCode.StartsWith(search) |
                          x.SupplierCompanyName.StartsWith(search))
                    .Include(inc => inc.SupplierDepartments)
                    .Include(inc => inc.SupplierDepartments.Select(sd => sd.Country))
                    .Select().ToList();

            foreach (var item in suppliers)
            {
                foreach (var itemSD in item.SupplierDepartments)
                {
                    supplierDepartmentEditModels.Add(BuildSupplierDeptEditModelAC(itemSD));
                }
            }
            return supplierDepartmentEditModels;
        }

        public SupplierEditModel CreateSupplier(SupplierEditModel model)
        {
            try
            {
                var item = ApplyChanges(model);
                item.NoteID = SaveNote(model);
                if (model.SupplierID == Guid.Empty)
                {
                    item.ObjectState = ObjectState.Added;
                    model.SupplierID = item.SupplierID;
                    _supplierService.Insert(item);
                }
                else
                {
                    item.ObjectState = ObjectState.Modified;
                    _supplierService.Update(item);
                }
                SaveSupplierDataIn3lists(model);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Creating Supplier failed", ex);
            }
            return model;
        }

        private void SaveSupplierDataIn3lists(SupplierEditModel model)
        {
            Dictionary<string, Guid> locaDic = new Dictionary<string, Guid>();
            Dictionary<string, Guid> depaDic = new Dictionary<string, Guid>();
            foreach (var item in model.SupplierLocations)
            {
                var result = _locationOrchestra.CreateSupplierLocation(item);
                //if (IsIdGuid(item.SupplierLocationID.ToString())) ///////////////////////////////
                if (!item.ItemAdding && !item.ItemDeleting)
                    item.x_SupplierLocationID = item.SupplierLocationID.ToString();
                locaDic[item.x_SupplierLocationID] = result.SupplierLocationID;
            }
            foreach (var item in model.SupplierDepartments)
            {
                var locaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedLocationIds)
                    locaGuidList.Add(locaDic[xId]);
                var result = _departmentOrchestra.CreateSupplierDepartment(item, locaGuidList);
                //if (IsIdGuid(item.SupplierDepartmentID.ToString())) ////////////////////////////////
                if (!item.ItemAdding && !item.ItemDeleting)
                    item.x_SupplierDepartmentID = item.SupplierDepartmentID.ToString();
                depaDic[item.x_SupplierDepartmentID] = result.SupplierDepartmentID;
            }
            foreach (var item in model.SupplierContacts)
            {
                var locaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedLocationIds)
                    locaGuidList.Add(locaDic[xId]);
                var depaGuidList = new List<Guid>();
                foreach (var xId in item.SelectedDepartmentIds)
                    depaGuidList.Add(depaDic[xId]);
                _contactOrchestra.CreateSupplierContact(item, locaGuidList, depaGuidList);
            }
        }

        private bool IsIdGuid(string expression)
        {
            if (expression != null)
            {
                Regex guidRegEx = new Regex(@"^(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})$");
                return guidRegEx.IsMatch(expression);
            }
            return false;
        }

        private Supplier ApplyChanges(SupplierEditModel model)
        {
            var item = new Supplier();
            item.SupplierID = model.SupplierID == Guid.Empty
                ? PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]) : model.SupplierID;
            SupplierItemsOf3lists(model, item.SupplierID);
            item.SupplierCompanyName = model.SupplierCompanyName;
            item.SupplierCode = model.SupplierCode;
            item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                ? DateTime.Parse(model.CreatedDate) : DateTime.Now;
            item.CreatedBy = _principal.Id;
            item.UpdatedDate = DateTime.Now;
            item.UpdatedBy = _principal.Id;
            item.IsActive = true;
            item.IsFactor = model.IsFactor;
            item.IsHaulier = model.IsHaulier;
            item.ParentSupplierID = model.ParentSupplierID;
            item.CompanyID = model.CompanyID;
            return item;
        }

        private void SupplierItemsOf3lists(SupplierEditModel model, Guid id)
        {
            foreach (var item in model.SupplierContacts)
            {
                item.SupplierID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedByUserID = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedByUserID = _principal.Id.ToString();
                item.IsActive = true;
            }
            foreach (var item in model.SupplierDepartments)
            {
                item.SupplierID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedBy = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedBy = _principal.Id.ToString();
                item.IsActive = true;
            }
            foreach (var item in model.SupplierLocations)
            {
                item.SupplierID = id;
                item.CreatedDate = !string.IsNullOrEmpty(model.CreatedDate)
                    ? DateTime.Parse(model.CreatedDate).ToString() : DateTime.Now.ToString();
                item.CreatedBy = _principal.Id.ToString();
                item.UpdatedDate = DateTime.Now.ToString();
                item.UpdatedBy = _principal.Id.ToString();
                item.IsActive = true;
            }
        }

        private Guid? SaveNote(SupplierEditModel model)
        {
            if (string.IsNullOrEmpty(model.NoteDescription))
                model.NoteDescription = "?!";  // <--- Paul Edwards
            if (!string.IsNullOrEmpty(model.Notes))
            {
                if (model.NoteID == null || model.NoteID == Guid.Empty)
                    model.NoteID = CreateNote(model, _principal);
                else model.NoteID = UpdateNote(model, _principal);
            }
            return model.NoteID;
        }

        private Guid CreateNote(SupplierEditModel model, ApplicationUser author)
        {
            var note = new Note
            {
                NoteID = PrimeActs.Service.IDGenerator.NewGuid(_serverCode[0]),
                NoteText = model.Notes,
                NoteDescription = model.NoteDescription,
                // IsActive = true,
                ObjectState = ObjectState.Added
            };
            _noteService.Insert(note);
            return note.NoteID;
        }

        private Guid UpdateNote(SupplierEditModel model, ApplicationUser author)
        {
            var note = _noteService.Find(model.NoteID.Value);
            note.NoteText = model.Notes;
            note.NoteDescription = model.NoteDescription;
            note.ObjectState = ObjectState.Modified;
            _noteService.Update(note);
            return note.NoteID;
        }

        private SupplierEditModel BuildSupplierEditModelAC(Supplier entity)
        {
            return new SupplierEditModel
            {
                SupplierID = entity.SupplierID,
                SupplierCode = entity.SupplierCode,
                SupplierCompanyName = entity.SupplierCompanyName
            };
        }

        private SupplierDepartmentEditModel BuildSupplierDeptEditModelAC(SupplierDepartment sdentity)
        {
            SupplierDepartmentEditModel strSupplierDeptList = new SupplierDepartmentEditModel();
            strSupplierDeptList.SupplierDepartmentName = sdentity.Supplier.SupplierCode + " - " + sdentity.Supplier.SupplierCompanyName + " - ";
            strSupplierDeptList.SupplierDepartmentName = strSupplierDeptList.SupplierDepartmentName + sdentity.SupplierDepartmentName + " - " + sdentity.Commission + " - " + sdentity.Handling;
            strSupplierDeptList.SupplierDepartmentID = sdentity.SupplierDepartmentID;
            strSupplierDeptList.CountryID = sdentity.CountryID ?? Guid.Parse("00760000-0000-0001-0006-827344180700");
            strSupplierDeptList.CountryName = sdentity.Country == null ? "United Kingdom" : sdentity.Country.CountryName;
            return strSupplierDeptList;
        }

        public SupplierPagingModel GetSupplierPagingModel(QueryOptions queryOptions, SearchObject searchObject)
        {
            var totalCount = 0;
            var supplierPagingModel = new SupplierPagingModel();
            var items = _supplierService.GetSuppliers(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<SupplierEditModel>(items.Select(BuildSupplierEditModel).ToList(), queryOptions);
            supplierPagingModel.SupplierEditModels = result;
            supplierPagingModel.SearchObject = new SearchObject
            {
                SupplierCode = searchObject.SupplierCode,
                SupplierCompanyName = searchObject.SupplierCompanyName,
                FromDate = searchObject.FromDate.HasValue ? searchObject.FromDate.Value : (DateTime?)null,
                ToDate = searchObject.ToDate.HasValue ? searchObject.ToDate.Value : (DateTime?)null,
                RecordsInDays = searchObject.RecordsInDays
            };
            return supplierPagingModel;
        }

        public SupplierItemPagingModel GetSupplierItemPagingModel(Guid id, QueryOptions queryOptions)
        {
            var totalCount = 0;
            var supplierItemPagingModel = new SupplierItemPagingModel();
            var items = _supplierItemService.SupplierItemsBySupplierID(id);
            queryOptions.TotalPages = totalCount;
            var result = new ResultList<SupplierItemEditModel>(items.Select(BuildSupplierItemEditModel).ToList(),
                queryOptions);
            supplierItemPagingModel.SupplierItemEditModels = result;
            return supplierItemPagingModel;
        }

        private SupplierEditModel BuildSupplierEditModel(Supplier entity)
        {
            var supplierEditModel = new SupplierEditModel();
            supplierEditModel.SupplierID = entity.SupplierID;
            supplierEditModel.SupplierCode = entity.SupplierCode;
            supplierEditModel.SupplierCompanyName = entity.SupplierCompanyName;
            supplierEditModel.ParentSupplierID = entity.ParentSupplierID;
            supplierEditModel.ParentSupplierName = entity.ParentSupplier.SupplierCompanyName;
            supplierEditModel.IsHaulier = entity.IsHaulier;
            supplierEditModel.IsFactor = entity.IsFactor;
            supplierEditModel.CompanyID = entity.CompanyID;
            supplierEditModel.CompanyName = entity.Company.CompanyName;
            if (entity.NoteID.HasValue)
            {
                supplierEditModel.NoteID = entity.NoteID;
                entity.Note = _noteService.NoteById(entity.NoteID.Value);
                supplierEditModel.Notes = entity.Note.NoteText;
            }
            supplierEditModel.IsActive = entity.IsActive ?? true;
            supplierEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty;
            supplierEditModel.CreatedBy = entity.CreatedBy.HasValue ? entity.CreatedBy.Value.ToString() : string.Empty;
            supplierEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty;
            supplierEditModel.UpdatedBy = entity.UpdatedBy.HasValue ? entity.UpdatedBy.Value.ToString() : string.Empty;
            //foreach (var supplierItem in entity.SupplierItems)
            //    supplierEditModel.SupplierItems.Add(BuildSupplierItemEditModel(supplierItem)); //////////////////////////
            supplierEditModel.SupplierLocations = _locationOrchestra.BuildSupplierLocationModels(entity.SupplierLocations);
            supplierEditModel.SupplierDepartments = _departmentOrchestra.BuildSupplierDepartmentModels(entity.SupplierDepartments);
            supplierEditModel.SupplierContacts = _contactOrchestra.BuildSupplierContactModels(entity.SupplierContacts);
            return supplierEditModel;
        }

        private SupplierItemEditModel BuildSupplierItemEditModel(SupplierItem entity)
        {
            var supplierItemEditModel = new SupplierItemEditModel();

            supplierItemEditModel.SupplierID = entity.SupplierID;
            supplierItemEditModel.SupplierCode = entity.SupplierCode;
            supplierItemEditModel.CreatedBy = entity.CreatedBy.HasValue ? entity.CreatedBy.Value : Guid.Empty;
            supplierItemEditModel.CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.Value : DateTime.UtcNow;
            supplierItemEditModel.UpdatedBy = entity.UpdatedBy.HasValue ? entity.UpdatedBy.Value : Guid.Empty;
            supplierItemEditModel.UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.Value : DateTime.UtcNow;
            return supplierItemEditModel;
        }

        public SupplierEditModel GetSupplierOnly(Guid id)
        {
            var supplier = _supplierService.GetSupplierOnly(id);
            ApplicationUser appUserName = _appUserService.UserById(Guid.Parse(supplier.CreatedBy.ToString()));
            Note noteObj = _noteService.NoteById(Guid.Parse(supplier.NoteID.ToString()));
            var editModel = BuildSupplierEditModel(supplier);
            editModel.FullName = appUserName.Firstname + " " + appUserName.Lastname;
            editModel.Notes = noteObj.NoteText;
            return editModel;

        }

        public List<SupplierItemEditModel> GetSupplierItemsOnly(Guid id)
        {
            var supplierItems = _supplierItemService.GetSupplierItemsOnly(id);
            return supplierItems.Select(BuildSupplierItemEditModel).ToList();

        }
    }
}

//private List<SupplierDepartmentEditModel> BuildSupplierDeptModelAC(Supplier entity)
//{
//        SupplierID = entity.SupplierID,
//        SupplierCode = entity.SupplierCode,
//        SupplierCompanyName = entity.SupplierCompanyName,
//        SupplierDepartmentName =
//            entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                .Select(x => x.SupplierDepartmentName)
//                .ToList(),
//        Commision =
//            entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                .Select(x => x.Commission)
//                .FirstOrDefault() > 0
//                ? entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                    .Select(x => x.Commission)
//                    .FirstOrDefault()
//                    .ToString()
//                : " 0.00",
//        Handling =
//            entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                .Select(x => x.Handling)
//                .FirstOrDefault() > 0
//                ? entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                    .Select(x => x.Handling)
//                    .FirstOrDefault()
//                    .ToString()
//                : " 0.00",
//        SupplierDeptID =
//            entity.SupplierDepartments.Where(x => x.SupplierID == entity.SupplierID)
//                .Select(x => x.SupplierDepartmentID)
//                .FirstOrDefault()
//}
