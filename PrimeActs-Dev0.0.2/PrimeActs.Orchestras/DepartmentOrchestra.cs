#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

#endregion

namespace PrimeActs.Orchestras
{

    public interface IDepartmentOrchestra
    {
        void Initialize(ApplicationUser principal);
        DepartmentEditModel GetDepartment(Guid id);
        DepartmentEditModel GetDepartment(string name);
        DepartmentPagingModel GetDeparmentsWithPaging(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Department.SearchObject searchObject);
        ResultList<DepartmentEditModel> GetDepartments(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Department.SearchObject searchObject);
        List<DepartmentEditModel> GetDeparments();
        List<DepartmentEditModel> GetDepartmentsForAutoComplete(string search,Guid DivisionID);
        DepartmentEditModel CreateDepartment(DepartmentEditModel model);
        DepartmentEditModel UpdateDepartment(DepartmentEditModel model);
        void RefreshCache();
    }

    public class DepartmentOrchestra : IDepartmentOrchestra
    {
        private readonly IDepartmentService _departmentService;
        private readonly IDivisionService _divisionService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public DepartmentOrchestra(ISetupLocalService setupLocalService, IDepartmentService departmentService, IDivisionService divisionService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _departmentService = departmentService;
            _divisionService = divisionService;
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }


        public DepartmentEditModel GetDepartment(Guid id)
        {
            return CreateFrom(_departmentService.DepartmentById(id));
        }

        public DepartmentEditModel GetDepartment(string name)
        {
            return CreateFrom(_departmentService.DepartmentByName(name));
        }

        public DepartmentPagingModel GetDeparmentsWithPaging(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Department.SearchObject searchObject)
        {
            var totalCount = 0;
            var departmentPagingModel = new DepartmentPagingModel();
            var departments = _departmentService.GetDepartments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<DepartmentEditModel>(departments.Select(CreateFrom).ToList(), queryOptions);
            departmentPagingModel.DepartmentEditModels = result;
            departmentPagingModel.SearchObject = new PrimeActs.Domain.ViewModels.Department.SearchObject
            {
                DepartmentName = searchObject.DepartmentName,
            };
            return departmentPagingModel;
        }

        public ResultList<DepartmentEditModel> GetDepartments(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Department.SearchObject searchObject)
        {
            int totalCount;
            var departments = _departmentService.GetDepartments(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<DepartmentEditModel>(departments != null ? departments.Select(CreateFrom).ToList() : null,
                    queryOptions);
        }

        public List<DepartmentEditModel> GetDeparments()
        {
            return _departmentService.GetAllDepartments().Select(BuildDepartmentEditModelAutoComplete).ToList();
        }

        public List<DepartmentEditModel> GetDepartmentsForAutoComplete(string search,Guid DivisionID)
        {
            return string.IsNullOrEmpty(search)
                ? null
                : _departmentService.GetAllDepartments()
                    .Where(x => (x.DepartmentName.StartsWith(search, StringComparison.CurrentCultureIgnoreCase) || x.DepartmentCode.StartsWith(search, StringComparison.CurrentCultureIgnoreCase)) && x.DivisionID == DivisionID && x.IsActive==true)
                    .OrderBy(x => x.DepartmentName)
                    .Select(BuildDepartmentEditModelAutoComplete)
                    .ToList();            
        }

        public DepartmentEditModel CreateDepartment(DepartmentEditModel model)
        {
            var department = ApplyChanges(model);
            department.ObjectState = ObjectState.Added;
         
            department.CreatedDate= DateTime.Now;
            department.IsActive = true;
            _departmentService.Insert(department);

            _departmentService.RefreshCache();
            model.DepartmentId = department.DepartmentID;
            return model;
        }

        public DepartmentEditModel UpdateDepartment(DepartmentEditModel model)
        {
            var department = ApplyChanges(model);
            department.ObjectState = ObjectState.Modified;
         
            department.UpdatedDate = DateTime.Now;
            _departmentService.Update(department);
            _departmentService.RefreshCache();
            return model;
        }

        public void RefreshCache()
        {
            _departmentService.RefreshCache();
        }

        private DepartmentEditModel CreateFrom(Department entity)
        {
            return new DepartmentEditModel
            {
                DepartmentId = entity.DepartmentID,
                DepartmentName = entity.DepartmentName,
                IsActive = entity.IsActive.HasValue ? entity.IsActive.Value : false,
               // UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.Value.ToString() : "",
                //CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.Value.ToString() : "",
                RelatedDivisionId = entity.Division != null ? entity.Division.DivisionID : Guid.Empty,
                RelatedDivisionName = entity.Division != null ? entity.Division.DivisionName : string.Empty,
            };
        }

        private Department ApplyChanges(DepartmentEditModel model)
        {
            return new Department
            {
                DepartmentID = Guid.Empty != model.DepartmentId ? model.DepartmentId : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                DepartmentName = model.DepartmentName,
                IsActive = model.IsActive,
            
                UpdatedDate = string.IsNullOrWhiteSpace(model.UpdatedDate)? (DateTime?) null: DateTime.Parse(model.UpdatedDate),
               
                CreatedDate = string.IsNullOrWhiteSpace(model.CreatedDate) ? (DateTime?)null : DateTime.Parse(model.CreatedDate),
                DivisionID = model.RelatedDivisionId,
            };
        }

        private DepartmentEditModel BuildDepartmentEditModelAutoComplete(Department entity)
        {
            return new DepartmentEditModel
            {
                DepartmentId = entity.DepartmentID,
                DepartmentName = entity.DepartmentName,
                DepartmentCode = entity.DepartmentCode
            };
        }


    }
}