#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

#endregion

namespace PrimeActs.Orchestras
{

    public interface IDivisionOrchestra
    {
        void Initialize(ApplicationUser principal);
        DivisionEditModel GetDivision(Guid id);
        DivisionEditModel GetDivision(string name);
        DivisionPagingModel GetDivisionsWithPaging(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Division.SearchObject searchObject);
        ResultList<DivisionEditModel> GetDivisions(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Division.SearchObject searchObject);
        List<DivisionEditModel> GetDivisions();
        List<DivisionEditModel> GetDivisionsForAutoComplete(string search);
        DivisionEditModel CreateDivision(DivisionEditModel model);
        DivisionEditModel UpdateDivision(DivisionEditModel model);
        void RefreshCache();
    }

    public class DivisionOrchestra : IDivisionOrchestra
    {
        private readonly IDivisionService _divisionService;
        private readonly string _serverCode;

        public DivisionOrchestra(ISetupLocalService setupLocalService, IDivisionService divisionService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _divisionService = divisionService;
        }

        public void Initialize(ApplicationUser principal)
        {
            throw new NotImplementedException();
        }

        public DivisionEditModel GetDivision(Guid id)
        {
            return CreateFrom(_divisionService.DivisionById(id));
        }

        public DivisionEditModel GetDivision(string name)
        {
            return CreateFrom(_divisionService.DivisionByName(name));
        }

        public DivisionPagingModel GetDivisionsWithPaging(QueryOptions queryOptions, Domain.ViewModels.Division.SearchObject searchObject)
        {
            var totalCount = 0;
            var divisionPagingModel = new DivisionPagingModel();
            var divisions = _divisionService.GetDivisions(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<DivisionEditModel>(divisions.Select(CreateFrom).ToList(), queryOptions);
            divisionPagingModel.DivisionEditModels = result;
            divisionPagingModel.SearchObject = new PrimeActs.Domain.ViewModels.Division.SearchObject
            {
                DivisionName = searchObject.DivisionName,
            };
            return divisionPagingModel;
        }

        public ResultList<DivisionEditModel> GetDivisions(QueryOptions queryOptions, Domain.ViewModels.Division.SearchObject searchObject)
        {
            int totalCount;
            var divisions = _divisionService.GetDivisions(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<DivisionEditModel>(divisions != null ? divisions.Select(CreateFrom).ToList() : null,
                    queryOptions);
        }

        public List<DivisionEditModel> GetDivisions()
        {
            return _divisionService.GetAllDivisions().Select(BuildDepartmentEditModelAutoComplete).ToList();
        }

        public List<DivisionEditModel> GetDivisionsForAutoComplete(string search)
        {
            return string.IsNullOrEmpty(search)
                 ? null
                 : _divisionService.GetAllDivisions()
                     .Where(x => x.DivisionName.StartsWith(search))
                     .OrderBy(x => x.DivisionName)
                     .Select(BuildDepartmentEditModelAutoComplete)
                     .ToList();
        }

        public DivisionEditModel CreateDivision(DivisionEditModel model)
        {
            var division = ApplyChanges(model);
            division.ObjectState = ObjectState.Added;
            //division.CreatedBy = 
            division.CreatedDate = DateTime.Now;
            division.IsActive = true;
            _divisionService.Insert(division);

            _divisionService.RefreshCache();
            model.DivisionId = division.DivisionID;
            return model;
        }

        public DivisionEditModel UpdateDivision(DivisionEditModel model)
        {
            var division = ApplyChanges(model);
            division.ObjectState = ObjectState.Modified;
           
            division.UpdatedDate = DateTime.Now;
            _divisionService.Update(division);
            _divisionService.RefreshCache();
            return model;
        }

        public void RefreshCache()
        {
            _divisionService.RefreshCache();
        }

        private string RandomString()
        {
            return DateTime.Today.DayOfWeek.ToString().Substring(0, 2).ToUpper() + new Random().Next(100000, 999999);
        }

        private Division ApplyChanges(DivisionEditModel model)
        {
            return new Division
            {
                DivisionID = Guid.Empty != model.DivisionId ? model.DivisionId : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                DivisionName = model.DivisionName,
                IsActive = model.IsActive,
              
                UpdatedDate = string.IsNullOrWhiteSpace(model.UpdatedDate) ? (DateTime?)null : DateTime.Parse(model.UpdatedDate),
                //CreatedBy = model.CreatedBy,
                CreatedDate = string.IsNullOrWhiteSpace(model.CreatedDate) ? (DateTime?)null : DateTime.Parse(model.CreatedDate),
                CompanyID =  model.RelatedCompanyId,
            };
        }

        private DivisionEditModel CreateFrom(Division entity)
        {
            return new DivisionEditModel
            {
                DivisionId = entity.DivisionID,
                DivisionName = entity.DivisionName,
                IsActive = entity.IsActive ?? false,
                RelatedCompanyId = entity.Company != null ? entity.Company.CompanyID : Guid.Empty,
                RelatedCompanyName = entity.Company != null ? entity.Company.CompanyName : string.Empty,
               // UpdatedBy = entity.UpdatedBy,
                UpdatedDate = entity.UpdatedDate.HasValue ? entity.UpdatedDate.ToString() : string.Empty,
               // CreatedBy = entity.CreatedBy,
                CreatedDate = entity.CreatedDate.HasValue ? entity.CreatedDate.ToString() : string.Empty
            };
        }
        private DivisionEditModel BuildDepartmentEditModelAutoComplete(Division entity)
        {
            return new DivisionEditModel
            {
                DivisionId = entity.DivisionID,
                DivisionName = entity.DivisionName
            };
        }

        private string ConvertIntToStr(int input)
        {
            lock (this)
            {
                var output = "";
                while (input > 0)
                {
                    var current = input % 10;
                    input /= 10;

                    if (current == 0)
                        current = 10;

                    output = (char)('A' + (current - 1)) + output;
                }
                return output;
            }
        }
    }
}