#region

using System.Data.Entity;
using PrimeActs.Data.Mapping;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Contexts
{
    public class PAndIContext : DataContextBase
    {
        static PAndIContext()
        {
            Database.SetInitializer<PAndIContext>(null);
        }

        public PAndIContext()
            : base("Name=PAndIContext")
        {
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserClaim> ApplicationUserClaims { get; set; }
        public DbSet<ApplicationUserLogin> ApplicationUserLogins { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Audit> Audits { get; set; }
        
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankStatement> BankStatement { get; set; }
        public DbSet<BankStatementItem> BankStatementItem { get; set; }
        public DbSet<BankStatementItemNominalLedgerEntry> BankStatementItemNominalLedgerEntry { get; set; }
        public DbSet<BankReconciliation> BankReconciliations { get; set; }
        public DbSet<BatchNumberLog> BatchNumberLogs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyNominalAccount> CompanyNominalAccounts { get; set; }
        public DbSet<Consignment> Consignments { get; set; }
        public DbSet<ConsignmentFile> ConsignmentFiles { get; set; }
        public DbSet<ConsignmentItem> ConsignmentItems { get; set; }
        public DbSet<ConsignmentItemPriceReturn> ConsignmentItemPriceReturns { get; set; }
        public DbSet<ConsignmentItemArrival> ConsignmentItemArrivals { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CreditRating> CreditRatings { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerBankAccount> CustomerBankAccounts { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }
        public DbSet<CustomerContactDepartment> CustomerContactDepartments { get; set; }
        public DbSet<CustomerContactLocation> CustomerContactLocations { get; set; }
        public DbSet<CustomerCurrency> CustomerCurrencies { get; set; }
        public DbSet<CustomerDepartment> CustomerDepartments { get; set; }
        public DbSet<CustomerDepartmentLocation> CustomerDepartmentLocations { get; set; }
        public DbSet<CustomerLocation> CustomerLocations { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DepartmentPrinter> DepartmentPrinters { get; set; }
        public DbSet<DepartmentPrintTask> DepartmentPrintTask { get; set; }
        public DbSet<DepartmentStockLocation> DepartmentStockLocations { get; set; }
        public DbSet<DespatchLocation> DespatchLocations { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Intrastat> Intrastats { get; set; }
        public DbSet<IntrastatItem> IntrastatItems { get; set; }
        public DbSet<LedgerEntryType> LedgerEntryTypes { get; set; }
        public DbSet<MasterGroup> MasterGroups { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<NominalAccount> NominalAccounts { get; set; }
        public DbSet<NominalLedgerEntry> NominalLedgerEntries { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PackWtUnit> PackWtUnits { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<Porterage> Porterages { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Printer> Printers { get; set; }
        public DbSet<PrintTask> PrintTasks { get; set; }
        public DbSet<Produce> Produces { get; set; }
        public DbSet<ProduceGroup> ProduceGroups { get; set; }
        public DbSet<ProduceGroupDepartment> ProduceGroupDepartments { get; set; }
        public DbSet<ProduceIntrastat> ProduceIntrastats { get; set; }
        public DbSet<ProduceTransactionTaxCode> ProduceTransactionTaxCodes { get; set; }
        public DbSet<PurchaseLedgerInvoiceAllocation> PurchaseLedgerInvoiceAllocations { get; set; }
        public DbSet<PurchaseChargeType> PurchaseChargeTypes { get; set; }
        public DbSet<PurchaseInvoice> PurchaseInvoices { get; set; }
        public DbSet<PurchaseInvoiceItem> PurchaseInvoiceItems { get; set; }
        public DbSet<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
        public DbSet<PurchaseType> PurchaseTypes { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<RoleContext> RoleContexts { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<SalesLedgerInvoiceAllocation> SalesLedgerInvoiceAllocations { get; set; }
        public DbSet<SalesInvoice> SalesInvoices { get; set; }
        public DbSet<SalesInvoiceItem> SalesInvoiceItems { get; set; }
        public DbSet<SalesLedgerEntry> SalesLedgerEntries { get; set; }
        public DbSet<SetupGlobal> SetupGlobals { get; set; }
        public DbSet<SetupLocal> SetupLocals { get; set; }
        public DbSet<StockBoard> StockBoards { get; set; }
        public DbSet<StockBoardProduceGroup> StockBoardProduceGroups { get; set; }
        public DbSet<StockLocation> StockLocations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierBankAccount> SupplierBankAccounts { get; set; }
        public DbSet<SupplierContact> SupplierContacts { get; set; }
        public DbSet<SupplierContactDepartment> SupplierContactDepartments { get; set; }
        public DbSet<SupplierContactLocation> SupplierContactLocations { get; set; }
        public DbSet<SupplierDepartment> SupplierDepartments { get; set; }
        public DbSet<SupplierDepartmentLocation> SupplierDepartmentLocations { get; set; }
        public DbSet<SupplierLocation> SupplierLocations { get; set; }

        public DbSet<TempBankNominalLedgerEntry> TempBankNominalLedgerEntries { get; set; }
        public DbSet<TempBankStatementItemNominalLedgerEntry> TempBankStatementItemNominalLedgerEntries { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<TicketRange> TicketRanges { get; set; }
        public DbSet<TransferType> TransferTypes { get; set; }
        public DbSet<TransactionTaxCode> TransactionTaxCodes { get; set; }
        public DbSet<TransactionTaxRate> TransactionTaxRates { get; set; }
        public DbSet<TransactionTaxLocation> TransactionTaxLocation { get; set; }
        public DbSet<Version> Versions { get; set; }
        public DbSet<WarehouseLocation> WarehouseLocations { get; set; }
        public DbSet<UserTabPanel> TabPanels { get; set; }
        public DbSet<vwStockBoard> vwStockBoards { get; set; }
        public DbSet<vwCashTicket> vwCashTickets { get; set; }
        public DbSet<vwPermissionDetail> vwPermissionDetails { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new ApplicationPermissionMap());
            modelBuilder.Configurations.Add(new ApplicationRoleMap());
            modelBuilder.Configurations.Add(new ApplicationUserClaimMap());
            modelBuilder.Configurations.Add(new ApplicationUserLoginMap());
            modelBuilder.Configurations.Add(new ApplicationUserMap());
            modelBuilder.Configurations.Add(new ApplicationUserRoleMap());
            modelBuilder.Configurations.Add(new AuditMap());
            modelBuilder.Configurations.Add(new BankAccountMap());
            modelBuilder.Configurations.Add(new BankStatementMap());
            modelBuilder.Configurations.Add(new BankStatementItemMap());
            modelBuilder.Configurations.Add(new BankStatementItemNominalLedgerEntryMap());
            modelBuilder.Configurations.Add(new BankReconciliationMap());
            modelBuilder.Configurations.Add(new BatchNumberLogMap());
            modelBuilder.Configurations.Add(new CompanyMap());
            modelBuilder.Configurations.Add(new CompanyNominalAccountMap());
            modelBuilder.Configurations.Add(new ConsignmentMap());
            modelBuilder.Configurations.Add(new ConsignmentFileMap());
            modelBuilder.Configurations.Add(new ConsignmentItemMap());
            modelBuilder.Configurations.Add(new ConsignmentItemPriceReturnMap());
            modelBuilder.Configurations.Add(new ConsignmentItemArrivalMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CreditRatingMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerBankAccountMap());
            modelBuilder.Configurations.Add(new CustomerContactMap());
            modelBuilder.Configurations.Add(new CustomerContactDepartmentMap());
            modelBuilder.Configurations.Add(new CustomerContactLocationMap());
            modelBuilder.Configurations.Add(new CustomerCurrencyMap());
            modelBuilder.Configurations.Add(new CustomerDepartmentMap());
            modelBuilder.Configurations.Add(new CustomerDepartmentLocationMap());
            modelBuilder.Configurations.Add(new CustomerLocationMap());
            modelBuilder.Configurations.Add(new CustomerTypeMap());
            modelBuilder.Configurations.Add(new DepartmentMap());
            modelBuilder.Configurations.Add(new DepartmentPrinterMap());
            modelBuilder.Configurations.Add(new DepartmentPrintTaskMap());
            modelBuilder.Configurations.Add(new DepartmentStockLocationMap());
            modelBuilder.Configurations.Add(new DespatchLocationMap());
            modelBuilder.Configurations.Add(new DivisionMap());
            modelBuilder.Configurations.Add(new ErrorLogMap());
            modelBuilder.Configurations.Add(new FileMap());
            modelBuilder.Configurations.Add(new GroupMap());
            modelBuilder.Configurations.Add(new IntrastatMap());
            modelBuilder.Configurations.Add(new IntrastatItemMap());
            modelBuilder.Configurations.Add(new LedgerEntryTypeMap());
            modelBuilder.Configurations.Add(new MasterGroupMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new NominalAccountMap());
            modelBuilder.Configurations.Add(new NominalLedgerEntryMap());
            modelBuilder.Configurations.Add(new NoteMap());
            modelBuilder.Configurations.Add(new PackWtUnitMap());

            modelBuilder.Configurations.Add(new PaymentTypeMap());
            modelBuilder.Configurations.Add(new PortMap());
            modelBuilder.Configurations.Add(new PorterageMap());
            modelBuilder.Configurations.Add(new PriceMap());
            modelBuilder.Configurations.Add(new PrinterMap());
            modelBuilder.Configurations.Add(new PrintTaskMap());
            modelBuilder.Configurations.Add(new ProduceMap());
            modelBuilder.Configurations.Add(new ProduceGroupMap());
            modelBuilder.Configurations.Add(new ProduceGroupDepartmentMap());
            
            modelBuilder.Configurations.Add(new ProduceIntrastatMap());
            modelBuilder.Configurations.Add(new ProduceTransactionTaxCodeMap());
            modelBuilder.Configurations.Add(new PurchaseLedgerInvoiceAllocationMap());
            modelBuilder.Configurations.Add(new PurchaseChargeTypeMap());
            modelBuilder.Configurations.Add(new PurchaseInvoiceMap());
            modelBuilder.Configurations.Add(new PurchaseInvoiceFileMap());
            modelBuilder.Configurations.Add(new PurchaseInvoiceItemMap());
            modelBuilder.Configurations.Add(new PurchaseLedgerEntryMap());
            modelBuilder.Configurations.Add(new PurchaseTypeMap());
            modelBuilder.Configurations.Add(new RepositoryMap());
            modelBuilder.Configurations.Add(new RoleContextMap());
            modelBuilder.Configurations.Add(new RolePermissionMap());
            modelBuilder.Configurations.Add(new SalesLedgerInvoiceAllocationMap());
            modelBuilder.Configurations.Add(new SalesInvoiceMap());
            modelBuilder.Configurations.Add(new SalesInvoiceItemMap());
            modelBuilder.Configurations.Add(new SalesLedgerEntryMap());
            modelBuilder.Configurations.Add(new SetupGlobalMap());
            modelBuilder.Configurations.Add(new SetupLocalMap());
            modelBuilder.Configurations.Add(new StockBoardMap());
            modelBuilder.Configurations.Add(new StockBoardProduceGroupMap());
            modelBuilder.Configurations.Add(new StockLocationMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new SupplierBankAccountMap());
            modelBuilder.Configurations.Add(new SupplierContactMap());
            modelBuilder.Configurations.Add(new SupplierContactLocationMap());
            modelBuilder.Configurations.Add(new SupplierContactDepartmentMap());
            modelBuilder.Configurations.Add(new SupplierDepartmentLocationMap());
            modelBuilder.Configurations.Add(new SupplierDepartmentMap());
            modelBuilder.Configurations.Add(new SupplierLocationMap());
            modelBuilder.Configurations.Add(new TabPanelMap());
            modelBuilder.Configurations.Add(new TempBankNominalLedgerEntryMap());
            modelBuilder.Configurations.Add(new TempBankStatementItemNominalLedgerEntryMap());
            modelBuilder.Configurations.Add(new TicketMap());
            modelBuilder.Configurations.Add(new TicketItemMap());
            modelBuilder.Configurations.Add(new TicketRangeMap());
            modelBuilder.Configurations.Add(new TransferTypeMap());
            modelBuilder.Configurations.Add(new TransactionTaxCodeMap());
            modelBuilder.Configurations.Add(new TransactionTaxRateMap());
            modelBuilder.Configurations.Add(new TransactionTaxLocationMap());

            modelBuilder.Configurations.Add(new VersionMap());
            modelBuilder.Configurations.Add(new vwStockBoardMap());
            modelBuilder.Configurations.Add(new vwCashTicketMap());
            modelBuilder.Configurations.Add(new vwPermissionDetailMap());
            
            modelBuilder.Configurations.Add(new WarehouseLocationMap());
        }
    }
}