using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace PrimeActs.Domain.ViewModels.Nominal
{
    public class ReconciliationTestModel
    {
        public ReconciliationTestModel()
        {
            ReconciledStatements = new List<Guid>();
        }
        // add a hidden id?guid
        public Guid RecId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Test { get; set; }
        public bool IsSelected { get; set; }
        public bool IsReconciled { get; set; }
        public List<Guid> ReconciledStatements { get; set; }
    }

    public class StatementImportTestModel
    {
        public Guid StatementId { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool IsSelected { get; set; }
        public bool IsReconciled { get; set; }
        public bool HasPossibleMatchingNominal { get; set; }
        // add a matching id on reconciliation list
    }

    public class StatementImportTestModel2
    {
        public Guid? StatementId { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
        public string Date { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool? IsSelected { get; set; }
        public bool? IsReconciled { get; set; }
        // add a matching id on reconciliation list
    }

    public class StatementImportNatWestModel
    {
        //public Guid StatementId { get; set; }
        public string SortCode { get; set; }
        public string AccountNumber { get; set; }
        public string AccountAlias { get; set; }
        public string AccountShortName { get; set; }
        public string Currency { get; set; }
        public string AccountType { get; set; }
        public string Bic { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
        public string Narrative3 { get; set; }
        public string Narrative4 { get; set; }
        public string Type { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        
        // add a matching id on reconciliation list
    }

    public class StatementPostModel
    {
        List<StatementImportTestModel> statementImportTestModel;
        List<ReconciliationTestModel> reconciliationTestModel;
    }

    public sealed class NatWestClassMap : CsvHelper.Configuration.CsvClassMap<StatementImportNatWestModel>
    {
        public NatWestClassMap()
        {
            Map(m => m.SortCode).Name("Sort Code");
            Map(m => m.AccountNumber).Name("Account Number");
            Map(m => m.AccountAlias).Name("Account Alias");
            Map(m => m.AccountShortName).Name("Account Short Name");
            Map(m => m.Currency).Name("Currency");
            Map(m => m.AccountType).Name("Account Type");
            Map(m => m.Bic).Name("BIC");
            Map(m => m.BankName).Name("Bank Name");
            Map(m => m.BranchName).Name("Branch Name");
            Map(m => m.Description).Name("Narrative #1");
            Map(m => m.Narrative1).Name("Narrative #2");
            Map(m => m.Narrative2).Name("Narrative #3");
            Map(m => m.Narrative3).Name("Narrative #4");
            Map(m => m.Type).Name("Type");
            Map(m => m.Debit).Name("Debit");
            Map(m => m.Credit).Name("Credit");
            Map(m => m.Date).Name("Date");
        }
    }

    public class StatementImportCSVModel
    {
        //public Guid StatementId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Narrative1 { get; set; }
        public string Narrative2 { get; set; }
    }

    public class ReconciliationViewModel
    {
        public ReconciliationViewModel()
        {
            ReconciliationItems = new List<ReconciliationTestModel>();
            StatementImportItems = new List<StatementImportTestModel>();
        }
        public List<ReconciliationTestModel> ReconciliationItems { get; set; }
        public List<StatementImportTestModel> StatementImportItems { get; set; }
        public Guid BankStatementID { get; set; }
    }

    public class BankReconciliationHeaderItem
    {
        public Guid BankStatementID { get; set; }
        public string BankStatementImportDate { get; set; }
        public string BankStatementFileName { get; set; }
        public bool BankStatementReconciled { get; set; }
        public Guid BankAccountID { get; set; }
        public string BankStatementStartDate { get; set; }
        public string BankStatementEndDate { get; set; }
        public bool IsActiveReconciliation { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal OpeningBalance { get; set; }
        public bool CanBeReconciled { get; set; }
    }

    public class BankReconciliationHeaderViewModel
    {
        public BankReconciliationHeaderViewModel()
        {
            BankReconciliationHeaderItems = new List<BankReconciliationHeaderItem>();
        }

        public List<BankReconciliationHeaderItem> BankReconciliationHeaderItems { get; set; }
        // Last Edited Header
        public Guid BankStatementID { get; set; }
        public string BankStatementImportDate { get; set; }
        public string BankStatementFileName { get; set; }
        public bool BankStatementReconciled { get; set; }
        public Guid BankAccountID { get; set; }
        public string BankStatementStartDate { get; set; }
        public string BankStatementEndDate { get; set; }
        public int MaxFileSize { get; set; }
        public int MaxNrOfFiles { get; set; }
        public string MainFolder { get; set; }
        public string UploadFolder { get; set; }
        public string AcceptedFileTypes { get; set; }
        public bool CanCallDetailsPage { get; set; }
        public decimal CurrentBalance { get; set; }
        public decimal OpeningBalance { get; set; }
        public bool CanBeReconciled { get; set; }
    }
}
