using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeActs.Domain.ViewModels.Journal
{
    public class JournaViewlModel
    {
        public DateTime EntryDate { get; set; }
        public int AccountingYear { get; set; }
        public List<PurchaseLedgerEntry> PurchaseLedgerEntries { get; set; }
        public decimal PurchaseTotal { get; set; }
        public List<SalesLedgerEntry> SalesLedgerEntries { get; set; }
        public decimal SaleTotal { get; set; }
        public List<NominalLedgerEntry> NominalLedgerEntries { get; set; }
        public decimal NominalTotal { get; set; }

     
        

    }
}
