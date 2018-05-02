using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Company;
using PrimeActs.Domain.ViewModels.Department;
using PrimeActs.Domain.ViewModels.Division;
using PrimeActs.Infrastructure.BaseEntities;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Rules.ValidationRules;

namespace PrimeActs.Orchestras
{

    public interface ICompanyOrchestra
    {
        void Initialize(ApplicationUser principal);
        CompanyEditModel GetCompany(Guid id);
        CompanyEditModel GetCompany(string name);
        CompanyPagingModel GetCompaniessWithPaging(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Company.SearchObject searchObject);
        ResultList<CompanyEditModel> GetCompanies(QueryOptions queryOptions, PrimeActs.Domain.ViewModels.Company.SearchObject searchObject);
        List<CompanyEditModel> GetCompanies();
        List<CompanyEditModel> GetCompanysForAutoComplete(string search);
        CompanyEditModel CreateCompany(CompanyEditModel model);
        CompanyEditModel UpdateCompany(CompanyEditModel model);
        void RefreshCache();
        List<CompanyModel> GetCompanieswithDivisionsAndDepartments();
        List<CompanyModel> GetCompanieswithDivisionsAndDepartmentsWithOptionAll();
    }

    public class CompanyOrchestra : ICompanyOrchestra
    {
        private readonly ICompanyService _companyService;
        private IAddressService _addressService;
        private readonly string _serverCode;

        public CompanyOrchestra(ISetupLocalService setupLocalService, ICompanyService companyService, IAddressService addressService)
        {
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";

            _companyService = companyService;
            _addressService = addressService;
        }

        public void Initialize(ApplicationUser principal)
        {
            throw new NotImplementedException();
        }

        public CompanyEditModel GetCompany(Guid id)
        {
            return CreateFrom(_companyService.CompanyById(id));
        }

        public CompanyEditModel GetCompany(string name)
        {
            return CreateFrom(_companyService.CompanyByName(name));
        }

        public CompanyPagingModel GetCompaniessWithPaging(QueryOptions queryOptions, Domain.ViewModels.Company.SearchObject searchObject)
        {
            var totalCount = 0;
            var companyPagingModel = new CompanyPagingModel();
            var companys = _companyService.GetCompanies(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            var result = new ResultList<CompanyEditModel>(companys.Select(CreateFrom).ToList(), queryOptions);
            companyPagingModel.CompanyEditModels = result;
            companyPagingModel.SearchObject = new PrimeActs.Domain.ViewModels.Company.SearchObject
            {
                CompanyName = searchObject.CompanyName,
            };
            return companyPagingModel;
        }

        public ResultList<CompanyEditModel> GetCompanies(QueryOptions queryOptions, Domain.ViewModels.Company.SearchObject searchObject)
        {
            int totalCount;
            var companys = _companyService.GetCompanies(queryOptions, searchObject, out totalCount);
            queryOptions.TotalPages = (int)Math.Ceiling((double)totalCount / queryOptions.PageSize);
            return
                new ResultList<CompanyEditModel>(companys != null ? companys.Select(CreateFrom).ToList() : null,
                    queryOptions);
        }

        public List<CompanyEditModel> GetCompanies()
        {
            return _companyService.GetAllCompanies().Select(BuildDepartmentEditModelAutoComplete).ToList();
        }

        public List<CompanyModel> GetCompanieswithDivisionsAndDepartments()
        {
            var companieswithDivisionsAndDepartments = _companyService.GetCompanieswithDivisionsAndDepartments()
                .OrderBy(x => x.CompanyName)
                .Select(BuildCompanyFull)
                .ToList();
            
            return companieswithDivisionsAndDepartments;
        }

        public List<CompanyModel> GetCompanieswithDivisionsAndDepartmentsWithOptionAll()
        {
            var companieswithDivisionsAndDepartments = _companyService.GetCompanieswithDivisionsAndDepartments()
                .OrderBy(x => x.CompanyName)
                .Select(BuildCompanyFullWithOptionAll)
                .ToList();

            companieswithDivisionsAndDepartments.Add(new CompanyModel()
            {
                CompanyName = "All",
                CompanyId = Guid.Empty,
                Divisions = new List<DivisionModel>() { new DivisionModel() { DivisionId = Guid.Empty, DivisionName = "All", Departments = new HashSet<DepartmentEditModel>() { new DepartmentEditModel() { DepartmentId = Guid.Empty, DepartmentName = "All" } } } }
            });

            return companieswithDivisionsAndDepartments;
        }

        public List<CompanyEditModel> GetCompanysForAutoComplete(string search)
        {
            return string.IsNullOrEmpty(search)
                 ? null
                 : _companyService.GetAllCompanies()
                     .Where(x => x.CompanyName.StartsWith(search))
                     .OrderBy(x => x.CompanyName)
                     .Select(BuildDepartmentEditModelAutoComplete)
                     .ToList();
        }

        public CompanyEditModel CreateCompany(CompanyEditModel model)
        {
            var company = ApplyChanges(model);
            company.ObjectState = ObjectState.Added;
         
            company.CreatedDate = DateTime.Now;
            company.IsActive = true;
            _companyService.Insert(company);

            _companyService.RefreshCache();
            model.CompanyId = company.CompanyID;
            return model;
        }

        public CompanyEditModel UpdateCompany(CompanyEditModel model)
        {
            var company = ApplyChanges(model);
            company.ObjectState = ObjectState.Modified;
           
            company.UpdatedDate = DateTime.Now;
            _companyService.Update(company);
            _companyService.RefreshCache();
            return model;
        }

        public void RefreshCache()
        {
            _companyService.RefreshCache();
        }

        private string RandomString()
        {
            return DateTime.Today.DayOfWeek.ToString().Substring(0, 2).ToUpper() + new Random().Next(100000, 999999);
        }

        private Company ApplyChanges(CompanyEditModel model)
        {
            return new Company
            {
                CompanyID = Guid.Empty != model.CompanyId ? model.CompanyId : PrimeActs.Service.IDGenerator.NewGuid(_serverCode.ToCharArray()[0]),
                CompanyName = model.CompanyName,
               
                EmailAddress = model.EmailAddress,
                Telephone = model.Telephone,
                FaxNo = model.FaxNo,
                Website = model.Website,
                InvoiceInfo = model.InvoiceInfo,
                IsActive = model.IsActive,
                
                UpdatedDate = model.UpdatedDate,//string.IsNullOrWhiteSpace(model.UpdatedDate) ? (DateTime?)null : DateTime.Parse(model.UpdatedDate),
                
                CreatedDate = model.CreatedDate,//string.IsNullOrWhiteSpace(model.CreatedDate) ? (DateTime?)null : DateTime.Parse(model.CreatedDate),
                RegisteredAddressID = model.RelatedRegisteredAddressId == Guid.Empty ? (Guid?)null : model.RelatedRegisteredAddressId,
                AddressID = model.RelatedAddressId == Guid.Empty ? (Guid?)null : model.RelatedAddressId,
                ParentCompanyID = model.RelatedCompanyId==Guid.Empty?(Guid?)null:model.RelatedCompanyId,
            };
        }

        private CompanyEditModel CreateFrom(Company entity)
        {
            var companyeditModel = new CompanyEditModel();
            //{
            companyeditModel.CompanyId = entity.CompanyID;
            companyeditModel.CompanyName = entity.CompanyName;
            companyeditModel.CompanyNo = string.IsNullOrEmpty(entity.CompanyNo) ? "" : entity.CompanyNo;
            companyeditModel.EmailAddress = string.IsNullOrEmpty(entity.EmailAddress) ? "emailEmpty" : entity.EmailAddress;
            
            companyeditModel.Logo = entity.Logo ==null ? "" : entity.Logo.ToString();
            companyeditModel.Telephone = string.IsNullOrEmpty(entity.Telephone) ? "" : entity.Telephone;
            companyeditModel.FaxNo = string.IsNullOrEmpty(entity.FaxNo) ? "" : entity.FaxNo;
            companyeditModel.Website = string.IsNullOrEmpty(entity.Website) ? "" : entity.Website;
            companyeditModel.InvoiceInfo = string.IsNullOrEmpty(entity.InvoiceInfo) ? "" : entity.InvoiceInfo;
            companyeditModel.IsActive = entity.IsActive ?? false;
            companyeditModel.RelatedCompanyId = entity.ParentCompany != null ? entity.ParentCompany.CompanyID : Guid.Empty;
            companyeditModel.RelatedCompanyName = entity.ParentCompany != null ? entity.ParentCompany.CompanyName : string.Empty;
            companyeditModel.RelatedAddressId = entity.Address != null ? entity.Address.AddressID : Guid.Empty;
            companyeditModel.RelatedRegisteredAddressId = entity.RegisteredAddress != null ? entity.RegisteredAddress.AddressID : Guid.Empty;
            //companyeditModel.UpdatedBy = string.IsNullOrEmpty(entity.UpdatedBy) ? "" : entity.UpdatedBy;
            companyeditModel.UpdatedDate = DateTime.Now;
            //companyeditModel.CreatedBy = string.IsNullOrEmpty(entity.CreatedBy) ? "" : entity.CreatedBy;
            companyeditModel.CreatedDate = DateTime.Now;
            //
            return companyeditModel;
        }

        private CompanyEditModel BuildDepartmentEditModelAutoComplete(Company entity)
        {
            return new CompanyEditModel
            {
                CompanyId = entity.CompanyID,
                CompanyName = entity.CompanyName
            };
        }

        private CompanyModel BuildCompanyFull(Company entity)
        {
            return new CompanyModel
            {
                CompanyId = entity.CompanyID,
                CompanyName = entity.CompanyName,
                Divisions = entity.Divisions.Select(d => new DivisionModel(){DivisionId = d.DivisionID, DivisionName = d.DivisionName, Departments = new HashSet<DepartmentEditModel>(d.Departments.Select(de => new DepartmentEditModel{DepartmentId = de.DepartmentID, DepartmentName = de.DepartmentName}).ToList().Distinct())}).ToList()
            };
        }

        private CompanyModel BuildCompanyFullWithOptionAll(Company entity)
        {
            var buildCompanyFullWithOptionAll = new CompanyModel
            {
                CompanyId = entity.CompanyID,
                CompanyName = entity.CompanyName,
                Divisions = entity.Divisions.Select
                (d =>
                {
                    var departmentEditModels = d.Departments.Select
                        (de => 
                            new DepartmentEditModel {   DepartmentId = de.DepartmentID, 
                                DepartmentName = de.DepartmentName 
                            }
                        ).ToList();
                    departmentEditModels.Add(new DepartmentEditModel()
                    {
                        DepartmentId = Guid.Empty,
                        DepartmentName = "All"
                    });
                    var distinctList = departmentEditModels.Distinct();
                    return new DivisionModel()
                    {
                        DivisionId = d.DivisionID,
                        DivisionName = d.DivisionName,
                        Departments = new HashSet<DepartmentEditModel>(
                            distinctList
                            )
                    };
                }).ToList()
            };

            buildCompanyFullWithOptionAll.Divisions.Add(new DivisionModel(){DivisionId = Guid.Empty, DivisionName = "All", Departments = new HashSet<DepartmentEditModel>{new DepartmentEditModel(){DepartmentId = Guid.Empty, DepartmentName = "All"}}});

            return buildCompanyFullWithOptionAll;
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