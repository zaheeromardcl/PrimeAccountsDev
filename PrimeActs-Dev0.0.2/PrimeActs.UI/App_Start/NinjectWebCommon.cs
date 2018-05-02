#region

using System;
using System.Web;
using System.Web.Http;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using PrimeActs.Data.Contexts;
using PrimeActs.Data.Contexts.NonEntityDataAccess;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Infrastructure.Validation;
using PrimeActs.Orchestra;
using PrimeActs.Orchestras;
using PrimeActs.UI.App_Start;
using WebActivatorEx;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.Logging;
using Microsoft.AspNet.SignalR;

#endregion

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: ApplicationShutdownMethod(typeof(NinjectWebCommon), "Stop")]

namespace PrimeActs.UI.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        ///     Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        ///     Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        ///     Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        ///     Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //Entity Framework
            kernel.Bind<IDataContextAsync>().To<PAndIContext>().InRequestScope();
            kernel.Bind<IUnitOfWorkAsync>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<INonEntityDataService>().To<NonEntityPAndIContext>().InRequestScope();

            //Cache
            kernel.Bind<ICache>().To<MemoryCache>();

            //Validation
            kernel.Bind<IValidationDictionary>().To<ModelStateValidation>();

            // Logging
            kernel.Bind<ILogger>().To<Log4NetLogger>();

            //ICustomerService 
            //ISalesPersonService 
            //ITaxService 
            //IAddressService

            //SignalR Hubs 
            //var resolver = new NinjectSignalRDependencyResolver(kernel);
            //kernel.Bind(typeof(IHubContext<dynamic>)).ToMethod(context => resolver.Resolve<ILiveStock>().GetHubContext<LiveStockboardHub>().Clients).WhenInjectedInto<ILiveStock>();
            //var config = new HubConfiguration();
            //config.Resolver = resolver;
            //PrimeActs.UI.Startup.ConfigureSignalR(app, config);

            //Repository
            kernel.Bind<IRepositoryAsync<ApplicationRole>>().To<Repository<ApplicationRole>>();
            kernel.Bind<IRepositoryAsync<ApplicationUser>>().To<Repository<ApplicationUser>>();
            kernel.Bind<IRepositoryAsync<ApplicationUserRole>>().To<Repository<ApplicationUserRole>>();
            kernel.Bind<IRepositoryAsync<RolePermission>>().To<Repository<RolePermission>>();
            kernel.Bind<IRepositoryAsync<Audit>>().To<Repository<Audit>>();
            kernel.Bind<IRepositoryAsync<SetupGlobal>>().To<Repository<SetupGlobal>>();
            kernel.Bind<IRepositoryAsync<SetupLocal>>().To<Repository<SetupLocal>>();
            kernel.Bind<IRepositoryAsync<Address>>().To<Repository<Address>>();
            kernel.Bind<IRepositoryAsync<Division>>().To<Repository<Division>>();
            kernel.Bind<IRepositoryAsync<Company>>().To<Repository<Company>>();
            kernel.Bind<IRepositoryAsync<Produce>>().To<Repository<Produce>>();
            kernel.Bind<IRepositoryAsync<Department>>().To<Repository<Department>>();
            kernel.Bind<IRepositoryAsync<DepartmentPrinter>>().To<Repository<DepartmentPrinter>>();
            kernel.Bind<IRepositoryAsync<DepartmentPrintTask>>().To<Repository<DepartmentPrintTask>>();
            kernel.Bind<IRepositoryAsync<MasterGroup>>().To<Repository<MasterGroup>>();
            kernel.Bind<IRepositoryAsync<ProduceGroup>>().To<Repository<ProduceGroup>>();
            kernel.Bind<IRepositoryAsync<Consignment>>().To<Repository<Consignment>>();
            kernel.Bind<IRepositoryAsync<ConsignmentItem>>().To<Repository<ConsignmentItem>>();
            kernel.Bind<IRepositoryAsync<ConsignmentItemPriceReturn>>().To<Repository<ConsignmentItemPriceReturn>>();
            kernel.Bind<IRepositoryAsync<ConsignmentItemArrival>>().To<Repository<ConsignmentItemArrival>>();
            kernel.Bind<IRepositoryAsync<Ticket>>().To<Repository<Ticket>>();
            kernel.Bind<IRepositoryAsync<TicketItem>>().To<Repository<TicketItem>>();
            kernel.Bind<IRepositoryAsync<Port>>().To<Repository<Port>>();
            kernel.Bind<IRepositoryAsync<PurchaseType>>().To<Repository<PurchaseType>>();
            kernel.Bind<IRepositoryAsync<PurchaseChargeType>>().To<Repository<PurchaseChargeType>>();
            kernel.Bind<IRepositoryAsync<PurchaseInvoice>>().To<Repository<PurchaseInvoice>>();
            kernel.Bind<IRepositoryAsync<PurchaseInvoiceFile>>().To<Repository<PurchaseInvoiceFile>>();
            kernel.Bind<IRepositoryAsync<PurchaseInvoiceItem>>().To<Repository<PurchaseInvoiceItem>>();
            kernel.Bind<IRepositoryAsync<Porterage>>().To<Repository<Porterage>>();
            kernel.Bind<IRepositoryAsync<Country>>().To<Repository<Country>>();
            kernel.Bind<IRepositoryAsync<PackWtUnit>>().To<Repository<PackWtUnit>>();
            kernel.Bind<IRepositoryAsync<DespatchLocation>>().To<Repository<DespatchLocation>>();
            kernel.Bind<IRepositoryAsync<TransactionTaxCode>>().To<Repository<TransactionTaxCode>>();
            kernel.Bind<IRepositoryAsync<TransactionTaxLocation>>().To<Repository<TransactionTaxLocation>>();
            kernel.Bind<IRepositoryAsync<TransactionTaxRate>>().To<Repository<TransactionTaxRate>>();
            kernel.Bind<IRepositoryAsync<File>>().To<Repository<File>>();
            kernel.Bind<IRepositoryAsync<ConsignmentFile>>().To<Repository<ConsignmentFile>>();
            kernel.Bind<IRepositoryAsync<Currency>>().To<Repository<Currency>>();
            kernel.Bind<IRepositoryAsync<CustomerCurrency>>().To<Repository<CustomerCurrency>>();
            kernel.Bind<IRepositoryAsync<Note>>().To<Repository<Note>>();
            kernel.Bind<IRepositoryAsync<LedgerEntryType>>().To<Repository<LedgerEntryType>>();
            kernel.Bind<IRepositoryAsync<SalesLedgerEntry>>().To<Repository<SalesLedgerEntry>>();
            kernel.Bind<IRepositoryAsync<SalesLedgerInvoiceAllocation>>().To<Repository<SalesLedgerInvoiceAllocation>>();
            kernel.Bind<IRepositoryAsync<SalesInvoiceItem>>().To<Repository<SalesInvoiceItem>>();
            kernel.Bind<IRepositoryAsync<SalesInvoice>>().To<Repository<SalesInvoice>>();
            kernel.Bind<IRepositoryAsync<StockBoard>>().To<Repository<StockBoard>>();
            kernel.Bind<IRepositoryAsync<BatchNumberLog>>().To<Repository<BatchNumberLog>>();
            kernel.Bind<IRepositoryAsync<NominalAccount>>().To<Repository<NominalAccount>>();
            kernel.Bind<IRepositoryAsync<NominalLedgerEntry>>().To<Repository<NominalLedgerEntry>>();
            kernel.Bind<IRepositoryAsync<TransferType>>().To<Repository<TransferType>>();
            kernel.Bind<IRepositoryAsync<Printer>>().To<Repository<Printer>>();
            kernel.Bind<IRepositoryAsync<PrintTask>>().To<Repository<PrintTask>>();
            kernel.Bind<IRepositoryAsync<PaymentType>>().To<Repository<PaymentType>>();
            kernel.Bind<IRepositoryAsync<UserTabPanel>>().To<Repository<UserTabPanel>>();
            kernel.Bind<IRepositoryAsync<BankAccount>>().To<Repository<BankAccount>>();
            kernel.Bind<IRepositoryAsync<CustomerBankAccount>>().To<Repository<CustomerBankAccount>>();
            kernel.Bind<IRepositoryAsync<BankStatement>>().To<Repository<BankStatement>>();
            kernel.Bind<IRepositoryAsync<BankStatementItem>>().To<Repository<BankStatementItem>>();
            kernel.Bind<IRepositoryAsync<TempBankNominalLedgerEntry>>().To<Repository<TempBankNominalLedgerEntry>>();
            kernel.Bind<IRepositoryAsync<TempBankStatementItemNominalLedgerEntry>>().To<Repository<TempBankStatementItemNominalLedgerEntry>>();
            kernel.Bind<IRepositoryAsync<BankStatementItemNominalLedgerEntry>>().To<Repository<BankStatementItemNominalLedgerEntry>>();
            kernel.Bind<IRepositoryAsync<Contact>>().To<Repository<Contact>>();
            kernel.Bind<IRepositoryAsync<vwStockBoard>>().To<Repository<vwStockBoard>>();
            kernel.Bind<IRepositoryAsync<vwCashTicket>>().To<Repository<vwCashTicket>>();
            kernel.Bind<IRepositoryAsync<vwPermissionDetail>>().To<Repository<vwPermissionDetail>>();

            kernel.Bind<IRepositoryAsync<Customer>>().To<Repository<Customer>>();
            kernel.Bind<IRepositoryAsync<CustomerDepartment>>().To<Repository<CustomerDepartment>>();
            kernel.Bind<IRepositoryAsync<CustomerLocation>>().To<Repository<CustomerLocation>>(); ///
            kernel.Bind<IRepositoryAsync<CustomerContact>>().To<Repository<CustomerContact>>(); /////
            kernel.Bind<IRepositoryAsync<CustomerContactLocation>>().To<Repository<CustomerContactLocation>>(); ///////
            kernel.Bind<IRepositoryAsync<CustomerContactDepartment>>().To<Repository<CustomerContactDepartment>>(); ////
            kernel.Bind<IRepositoryAsync<CustomerDepartmentLocation>>().To<Repository<CustomerDepartmentLocation>>(); //
            kernel.Bind<IRepositoryAsync<CustomerType>>().To<Repository<CustomerType>>(); ///////////
            //kernel.Bind<IRepositoryAsync<CustomerItem>>().To<Repository<CustomerItem>>(); //////////////////////////////
            kernel.Bind<IRepositoryAsync<Supplier>>().To<Repository<Supplier>>();
            kernel.Bind<IRepositoryAsync<SupplierDepartment>>().To<Repository<SupplierDepartment>>();
            kernel.Bind<IRepositoryAsync<SupplierLocation>>().To<Repository<SupplierLocation>>(); ///
            kernel.Bind<IRepositoryAsync<SupplierContact>>().To<Repository<SupplierContact>>(); /////
            kernel.Bind<IRepositoryAsync<SupplierContactLocation>>().To<Repository<SupplierContactLocation>>(); ///////
            kernel.Bind<IRepositoryAsync<SupplierContactDepartment>>().To<Repository<SupplierContactDepartment>>(); ////
            kernel.Bind<IRepositoryAsync<SupplierDepartmentLocation>>().To<Repository<SupplierDepartmentLocation>>(); //
            kernel.Bind<IRepositoryAsync<SupplierItem>>().To<Repository<SupplierItem>>(); //////////////////////////////

            //Orchestra
            //kernel.Bind<ISetupLocal>().To<SetupLocal>();
            kernel.Bind<IApplicationRoleOrchestra>().To<ApplicationRoleOrchestra>();
            kernel.Bind<IApplicationUserOrchestra>().To<ApplicationUserOrchestra>();
            kernel.Bind<IAuditOrchestra>().To<AuditOrchestra>();
            kernel.Bind<IDivisionOrchestra>().To<DivisionOrchestra>();
            kernel.Bind<ICompanyOrchestra>().To<CompanyOrchestra>();
            kernel.Bind<ITicketOrchestra>().To<TicketOrchestra>();
            kernel.Bind<IProduceOrchestra>().To<ProduceOrchestra>();
            kernel.Bind<IProduceGroupOrchestra>().To<ProduceGroupOrchestra>();
            kernel.Bind<IPurchaseInvoiceOrchestra>().To<PurchaseInvoiceOrchestra>();
            kernel.Bind<IDepartmentOrchestra>().To<DepartmentOrchestra>();
            kernel.Bind<IFileOrchestra>().To<FileOrchestra>();
            kernel.Bind<IConsignmentOrchestra>()
                .To<ConsignmentOrchestra>()
                .WithConstructorArgument("UserName",
                    HttpContext.Current != null ? HttpContext.Current.User.Identity.Name : string.Empty);
            kernel.Bind<IPurchaseTypeOrchestra>().To<PurchaseTypeOrchestra>();
            kernel.Bind<IPurchaseChargeTypeOrchestra>().To<PurchaseChargeTypeOrchestra>();
            kernel.Bind<IPortOrchestra>().To<PortOrchestra>();
            kernel.Bind<IDespatchOrchestra>().To<DespatchOrchestra>();
            kernel.Bind<ICustomerCurrencyOrchestra>().To<CustomerCurrencyOrchestra>();
            kernel.Bind<ICurrencyOrchestra>().To<CurrencyOrchestra>();
            kernel.Bind<IInvoiceOrchestra>().To<InvoiceOrchestra>();
            kernel.Bind<IEventOrchestra>().ToConstant(new EventOrchestra());
            kernel.Bind<ICountryOrchestra>().To<CountryOrchestra>();
            kernel.Bind<IStockBoardOrchestra>().To<StockBoardOrchestra>();
            kernel.Bind<IPrintOrchestra>().To<PrintOrchestra>();
            kernel.Bind<IPrintTaskOrchestra>().To<PrintTaskOrchestra>();
            kernel.Bind<IPrinterOrchestra>().To<PrinterOrchestra>(); // DC 2107
            kernel.Bind<IDepartmentPrintTaskOrchestra>().To<DepartmentPrintTaskOrchestra>(); // DC 2107
            kernel.Bind<ITabPanelOrchestra>().To<TabPanelOrchestra>();
            kernel.Bind<IBankAccountOrchestra>().To<BankAccountOrchestra>();
            kernel.Bind<IContactOrchestra>().To<ContactOrchestra>();
            kernel.Bind<INominalOrchestra>().To<NominalOrchestra>();
            kernel.Bind<IBankStatementOrchestra>().To<BankStatementOrchestra>();
            kernel.Bind<ITempBankNominalOrchestra>().To<TempBankNominalOrchestra>();
            kernel.Bind<INominalAccountOrchestra>().To<NominalAccountOrchestra>();

            kernel.Bind<ICustomerOrchestra>().To<CustomerOrchestra>();
            kernel.Bind<ICustomerDepartmentOrchestra>().To<CustomerDepartmentOrchestra>();
            kernel.Bind<ICustomerLocationOrchestra>().To<CustomerLocationOrchestra>(); ///
            kernel.Bind<ICustomerContactOrchestra>().To<CustomerContactOrchestra>(); /////
            kernel.Bind<ICustomerTypeOrchestra>().To<CustomerTypeOrchestra>(); ///////////
            kernel.Bind<ILookupOrchestra>().To<LookupOrchestra>();
            kernel.Bind<ISupplierOrchestra>().To<SupplierOrchestra>();
            kernel.Bind<ISupplierDepartmentOrchestra>().To<SupplierDepartmentOrchestra>();
            kernel.Bind<ISupplierLocationOrchestra>().To<SupplierLocationOrchestra>(); ///
            kernel.Bind<ISupplierContactOrchestra>().To<SupplierContactOrchestra>(); /////

            //Data Service
            kernel.Bind<IRolePermissionService>().To<RolePermissionService>();
            kernel.Bind<ISetupGlobalService>().To<SetupGlobalService>();
            kernel.Bind<ISetupLocalService>().To<SetupLocalService>();
            kernel.Bind<IAddressService>().To<AddressService>();
            kernel.Bind<IAuditService>().To<AuditService>();
            kernel.Bind<IDivisionService>().To<DivisionService>();
            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IDepartmentPrinterService>().To<DepartmentPrinterService>();
            kernel.Bind<ITicketService>().To<TicketService>();
            kernel.Bind<ICashTicketService>().To<CashTicketService>();
            kernel.Bind<ITicketItemService>().To<TicketItemService>();
            kernel.Bind<IProduceService>().To<ProduceService>();
            kernel.Bind<IPurchaseInvoiceService>().To<PurchaseInvoiceService>();
            kernel.Bind<IPurchaseInvoiceFileService>().To<PurchaseInvoiceFileService>();
            kernel.Bind<IPurchaseInvoiceItemService>().To<PurchaseInvoiceItemService>();
            kernel.Bind<IDepartmentService>().To<DepartmentService>();
            kernel.Bind<IDepartmentPrintTaskService>().To<DepartmentPrintTaskService>();
            kernel.Bind<IPrintTaskService>().To<PrintTaskService>();
            kernel.Bind<IMasterGroupService>().To<MasterGroupService>();
            kernel.Bind<IProduceGroupService>().To<ProduceGroupService>();
            kernel.Bind<IPortService>().To<PortService>();
            kernel.Bind<IPurchaseTypeService>().To<PurchaseTypeService>();
            kernel.Bind<IPurchaseChargeTypeService>().To<PurchaseChargeTypeService>();
            kernel.Bind<IConsignmentService>().To<ConsignmentService>();
            kernel.Bind<ICountryService>().To<CountryService>();
            kernel.Bind<IPorterageService>().To<PorterageService>();
            kernel.Bind<IConsignmentItemService>().To<ConsignmentItemService>();
            kernel.Bind<IConsignmentItemPriceReturnService>().To<ConsignmentItemPriceReturnService>();
            kernel.Bind<IConsignmentItemArrivalService>().To<ConsignmentItemArrivalService>();
            kernel.Bind<IPackWtUnitService>().To<PackWtUnitService>();
            kernel.Bind<IDespatchService>().To<DespatchService>();
            kernel.Bind<ITransactionTaxCodeService>().To<TransactionTaxCodeService>();
            kernel.Bind<ITransactionTaxLocationService>().To<TransactionTaxLocationService>();
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IConsignmentFileService>().To<ConsignmentFileService>();
            kernel.Bind<ICurrencyService>().To<CurrencyService>();
            kernel.Bind<ICustomerCurrencyService>().To<CustomerCurrencyService>();
            kernel.Bind<INoteService>().To<NoteService>();
            kernel.Bind<ILedgerEntryTypeService>().To<LedgerEntryTypeService>();
            kernel.Bind<ISalesLedgerEntryService>().To<SalesLedgerEntryService>();
            kernel.Bind<ISalesLedgerInvoiceAllocationService>().To<SalesLedgerInvoiceAllocationService>();
            kernel.Bind<ISalesInvoiceItemService>().To<SalesInvoiceItemService>();
            kernel.Bind<ISalesInvoiceService>().To<SalesInvoiceService>();
            kernel.Bind<IStockBoardService>().To<StockBoardService>();
            kernel.Bind<IBatchNumberLogService>().To<BatchNumberLogService>();
            kernel.Bind<INominalAccountService>().To<NominalAccountService>();
            kernel.Bind<INominalLedgerEntryService>().To<NominalLedgerEntryService>();
            kernel.Bind<IApplicationRoleService>().To<ApplicationRoleService>();
            kernel.Bind<IApplicationUserService>().To<ApplicationUserService>();
            kernel.Bind<IApplicationUserRoleService>().To<ApplicationUserRoleService>();
            kernel.Bind<ITransferTypeService>().To<TransferTypeService>();
            kernel.Bind<IProduceForTicketService>().To<ProduceForTicketService>();
            kernel.Bind<IPrintService>().To<PrintService>();
            kernel.Bind<IPrinterService>().To<PrinterService>();
            kernel.Bind<IPaymentTypeService>().To<PaymentTypeService>();
            kernel.Bind<ITabPanelService>().To<TabPanelService>();
            kernel.Bind<IBankAccountService>().To<BankAccountService>();
            kernel.Bind<ICustomerBankAccountService>().To<CustomerBankAccountService>();
            kernel.Bind<IContactService>().To<ContactService>();
            kernel.Bind<ITransactionTaxRateService>().To<TransactionTaxRateService>();
            kernel.Bind<ILiveStock>().To<LiveStock>();
            kernel.Bind<IBankStatementService>().To<BankStatementService>();
            kernel.Bind<IBankStatementItemService>().To<BankStatementItemService>();
            kernel.Bind<IBankStatementItemNominalLedgerEntryService>().To<BankStatementItemNominalLedgerEntryService>();
            kernel.Bind<ITempBankNominalLedgerEntryService>().To<TempBankNominalLedgerEntryService>();
            kernel.Bind<ITempBankStatementItemNominalLedgerEntryService>().To<TempBankStatementItemNominalLedgerEntryService>();
            kernel.Bind<IvwStockBoardService>().To<vwStockBoardService>();
            kernel.Bind<IvwPermissionDetailService>().To<vwPermissionDetailService>();

            kernel.Bind<ICustomerService>().To<CustomerService>();
            kernel.Bind<ICustomerDepartmentService>().To<CustomerDepartmentService>();
            kernel.Bind<ICustomerLocationService>().To<CustomerLocationService>(); /////
            kernel.Bind<ICustomerContactService>().To<CustomerContactService>(); ///////
            kernel.Bind<ICustomerContactLocationService>().To<CustomerContactLocationService>(); ////////
            kernel.Bind<ICustomerContactDepartmentService>().To<CustomerContactDepartmentService>(); ////
            kernel.Bind<ICustomerDepartmentLocationService>().To<CustomerDepartmentLocationService>(); //
            //kernel.Bind<ICustomerItemService>().To<CustomerItemService>(); //////////////////////////////
            kernel.Bind<ICustomerTypeService>().To<CustomerTypeService>(); /////////////
            kernel.Bind<ISupplierService>().To<SupplierService>();
            kernel.Bind<ISupplierDepartmentService>().To<SupplierDepartmentService>();
            kernel.Bind<ISupplierLocationService>().To<SupplierLocationService>(); /////
            kernel.Bind<ISupplierContactService>().To<SupplierContactService>(); ///////
            kernel.Bind<ISupplierContactLocationService>().To<SupplierContactLocationService>(); ////////
            kernel.Bind<ISupplierContactDepartmentService>().To<SupplierContactDepartmentService>(); ////
            kernel.Bind<ISupplierDepartmentLocationService>().To<SupplierDepartmentLocationService>(); //
            kernel.Bind<ISupplierItemService>().To<SupplierItemService>(); //////////////////////////////
        }
    }
}