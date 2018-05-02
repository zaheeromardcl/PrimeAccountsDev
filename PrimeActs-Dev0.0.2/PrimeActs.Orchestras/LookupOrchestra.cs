#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;
using SearchObject = PrimeActs.Domain.ViewModels.Consignment.SearchObject;
using System.Diagnostics;
using System.Configuration;
using System.Globalization;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public class LookupOrchestra : ILookupOrchestra
    {
        private ApplicationUser _principal;
        private readonly ICountryService _countryService;
        // No service why not ?? private readonly ICreditRatingService _creditRatingService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerTypeService _customerTypeService;
        private readonly IDespatchService _despatchService;
        private readonly IPackWtUnitService _packWtUnitService;
        private readonly IPortService _portService;
        private readonly IPorterageService _porterageService;
        private readonly IPurchaseChargeTypeService _purchaseChargeTypeService;
        private readonly IPurchaseTypeService _purchaseTypeService;
        private readonly ISetupGlobalService _setupGlobalService;
        private readonly ISetupLocalService _setupLocalService;
        // No service why not ?? private readonly IStockLocation _stockLocation;
        private readonly ITransactionTaxLocationService _transactionTaxLocationService;
        // No service why not ?? private readonly IWarehouseLocation _warehouseLocation;

        //Permissions table specific to this user and company/dept/div lists
        private readonly IvwPermissionDetailService _permissionDetailService;
        private readonly ICompanyService _companyService;
        private readonly IDivisionService _divisionService;
        private readonly IDepartmentService _departmentService;

        public LookupOrchestra(ICountryService countryService, ICurrencyService currencyService, ICustomerTypeService customerTypeService, IDespatchService despatchService,
            IPorterageService porterageService, IPortService portService, IPackWtUnitService packWtUnitService, IPurchaseChargeTypeService purchaseChargeTypeService,
            IPurchaseTypeService purchaseTypeService, ITransactionTaxLocationService transactionTaxLocationService, IvwPermissionDetailService permissionDetailService,
            ICompanyService companyService, IDivisionService divisionService, IDepartmentService departmentService, ISetupGlobalService setupGlobalService, ISetupLocalService setupLocalService)
        {
            _countryService = countryService;
            _currencyService = currencyService;
            _customerTypeService = customerTypeService;
            _despatchService = despatchService;
            _packWtUnitService = packWtUnitService;
            _portService = portService;
            _porterageService = porterageService;
            _purchaseChargeTypeService = purchaseChargeTypeService;
            _purchaseTypeService = purchaseTypeService;
            _setupGlobalService = setupGlobalService;
            _setupLocalService = setupLocalService;
            _transactionTaxLocationService = transactionTaxLocationService;
            _permissionDetailService = permissionDetailService;
            _companyService = companyService;
            _divisionService = divisionService;
            _departmentService = departmentService;
        }
        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }
        public LookupLists GetLookupLists()
        {
            var defaultCompanyID = _principal.CompanyId.ToString();
            var defaultDivisionID = _principal.DivisionId.ToString();
            var defaultDepartmentID = _principal.DepartmentId.ToString();
            var countries = _countryService.GetAllCountries();
            var currencies = _currencyService.GetAllCurrencys();
            var customerTypes = _customerTypeService.ListOfCustomerTypes();
            var despatchLocations = _despatchService.GetAllDespatches();
            var packWtUnits = _packWtUnitService.GetAllPackWtUnits();
            var ports = _portService.GetAllPorts();
            var porterages = _porterageService.GetAllPorterages();
            var purchaseChargeTypes = _purchaseChargeTypeService.GetAllPurchaseChargeTypes();
            var purchaseTypes = _purchaseTypeService.GetAllPurchaseTypes();
            var transactionTaxLocations = _transactionTaxLocationService.GetAllTransactionLocations();
            var permissionDetails = _permissionDetailService.GetPermissionDetailByUserID(_principal.Id);
            var companies = _companyService.GetAllCompanies().Select(
                    thisCompany => new CompanyDropDown
                    {
                        CompanyID = thisCompany.CompanyID.ToString(),
                        CompanyName = thisCompany.CompanyName,
                        IsActive = thisCompany.IsActive ?? true
                    }
                ).ToList();
            var divisions = _divisionService.GetAllDivisions().Select(
                    thisDivision => new DivisionDropDown
                    {
                        DivisionID = thisDivision.DivisionID.ToString(),
                        CompanyID = thisDivision.CompanyID.ToString(),
                        DivisionName = thisDivision.DivisionName,
                        IsActive = thisDivision.IsActive ?? true,
                        AutogenerateConsignnmentReference = _setupGlobalService.GetSetupBooleanByNameAndDivisionID("AutogenerateConsignmentNumber", thisDivision.DivisionID) ?? true
                    }
                ).ToList();
            var departments = _departmentService.GetAllDepartments().Select(
                    thisDepartment => new DepartmentDropDown
                    {
                        DepartmentID = thisDepartment.DepartmentID.ToString(),
                        DivisionID = thisDepartment.DivisionID.ToString(),
                        DepartmentName = thisDepartment.DepartmentName,
                        DepartmentCode = thisDepartment.DepartmentCode,
                        IsActive = thisDepartment.IsActive ?? true
                    }
                ).ToList();
            return new LookupLists() { 
                DefaultCompanyID = defaultCompanyID,
                DefaultDivisionID = defaultDivisionID,
                DefaultDepartmentID = defaultDepartmentID,
                Country = countries,
                Currency = currencies,
                CustomerType = customerTypes,
                DespatchLocation = despatchLocations,
                PackWtUnit = packWtUnits, 
                Port = ports,
                Porterage = porterages,
                PurchaseChargeType = purchaseChargeTypes,
                PurchaseType = purchaseTypes,
                TransactionTaxLocation = transactionTaxLocations,
                PermissionDetail = permissionDetails,
                Company = companies,
                Division = divisions,
                Department = departments
            };
        }

    }
}
