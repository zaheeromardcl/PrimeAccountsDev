using System;
using System.Collections.Generic;
using PrimeActs.Orchestras;
using System.Linq;
using System.Security.Principal;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Domain.ViewModels.Consignment;
using PrimeActs.Infrastructure.BaseEntities;
//using SearchObject = PrimeActs.Domain.ViewModels..SearchObject;
using System.Diagnostics;
using System.Configuration;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Orchestras
{
    public interface ITempBankNominalOrchestra
    {
        
        //TempBankNominalLedgerEntry GetTempBankNominalLedgerEntry(string fileName);
        List<TempBankNominalLedgerEntry> GetTempBankNominalLedgerEntries(Guid bankStatementID);
        void SaveTempBankNominalLedgerEntry(TempBankNominalLedgerEntry tempBankNominalLedgerEntry);
        void UpdateTempBankNominalLedgerEntry(TempBankNominalLedgerEntry tempBankNominalLedgerEntry);
        void SaveTempBankNominalLedgerEntries(List<TempBankNominalLedgerEntry> tempBankNominalLedgerEntries);
        //void SaveBankNominalLedgerEntries(List<BankStatementItemNominalLedgerEntry> bankStatementItemNominalLedgerEntries);
        void UpdateIsReconciled(Guid id,bool isReconciled);
        void SaveTempStatementMatch(Guid nominalId, Guid statementId);
        void DeleteTempStatementMatch(Guid nominalId);
        bool StatementIsMatched(Guid statementItemId);
        List<Guid> GetMatchingStatements(Guid nominalId);
        Guid? GetCurrentStatementInReconciliation();
        decimal GetTotalIsReconciled(Guid bankStatementID);
        bool AllStatementItemsReconciled(Guid bankStatementID);

        void SaveBankStatementItemNominalLedgerEntries(List<BankStatementItemNominalLedgerEntry> bankStatementItemNominalLedgerEntries);
        void DeleteTempBankStatementItemNominalLedgerRange(ref List<TempBankStatementItemNominalLedgerEntry> tempBankNominalLedgerEntries);
        void DeleteTempBankNominalLedgerRange(ref List<TempBankNominalLedgerEntry> tempBankNominalLedgerEntries);
        List<TempBankStatementItemNominalLedgerEntry> GetTempBankStatementItemNominalLedgerEntries(Guid bankStatementID);
        List<TempBankStatementItemNominalLedgerEntry> GetAllTempBankStatementItemNominalLedgerEntries();
    }
    
    public class TempBankNominalOrchestra : ITempBankNominalOrchestra
    {
        private readonly ITempBankNominalLedgerEntryService _tempBankNominalLedgerEntryService;
        private readonly ITempBankStatementItemNominalLedgerEntryService _tempBankStatementItemNominalLedgerEntryService;
        private readonly IBankStatementItemNominalLedgerEntryService _bankStatementItemNominalLedgerEntryService;
        private ApplicationUser _principal;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly IBankStatementItemService _bankStatementItemService;
        private readonly ISetupLocalService _setupService;
        private readonly string _serverCode;
        private readonly IUnitOfWorkAsync _unitofWork;

        public TempBankNominalOrchestra(ISetupLocalService setupLocalService, ISetupLocalService setupService, ITempBankNominalLedgerEntryService tempBankNominalLedgerEntryService, ITempBankStatementItemNominalLedgerEntryService tempBankStatementItemNominalLedgerEntryService,
            IApplicationUserOrchestra applicationUserOrchestra, IBankStatementItemService bankStatementItemService, IBankStatementItemNominalLedgerEntryService bankStatementItemNominalLedgerEntryService, IUnitOfWorkAsync unitofWork)
        {
            
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _setupService = setupService;
            _tempBankNominalLedgerEntryService = tempBankNominalLedgerEntryService;
            _tempBankStatementItemNominalLedgerEntryService = tempBankStatementItemNominalLedgerEntryService;
            _bankStatementItemNominalLedgerEntryService = bankStatementItemNominalLedgerEntryService;
            //_bankStatementItemNominalLedgerEntryService = bankStatementItemNominalLedgerEntryService;
            _applicationUserOrchestra = applicationUserOrchestra;
            _bankStatementItemService = bankStatementItemService;
            _unitofWork = unitofWork;
        }

        public List<TempBankNominalLedgerEntry> GetTempBankNominalLedgerEntries(Guid bankStatementID)
        {
            var tempNominalLedgerItems = _tempBankNominalLedgerEntryService.GetTempBankNominalLenderEntriesByStatementID(bankStatementID);
            return tempNominalLedgerItems;
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetAllTempBankStatementItemNominalLedgerEntries()
        {
            // var tempBankStatementItemNominalLedgerItems = _tempBankStatementItemNominalLedgerEntryService.GetByStatementID(bankStatementID);
            var tempBankStatementItemNominalLedgerItems = _tempBankStatementItemNominalLedgerEntryService.GetAll();
            return tempBankStatementItemNominalLedgerItems;
        }

        public List<TempBankStatementItemNominalLedgerEntry> GetTempBankStatementItemNominalLedgerEntries(Guid bankStatementID)
        {
           // var tempBankStatementItemNominalLedgerItems = _tempBankStatementItemNominalLedgerEntryService.GetByStatementID(bankStatementID);
            var tempBankStatementItemNominalLedgerItems = _tempBankStatementItemNominalLedgerEntryService.GetByStatementID(bankStatementID);
            return tempBankStatementItemNominalLedgerItems;
        }

        public void SaveTempBankNominalLedgerEntry(TempBankNominalLedgerEntry tempBankNominalLedgerEntry)
        {
            _tempBankNominalLedgerEntryService.Insert(tempBankNominalLedgerEntry);
            _unitofWork.SaveChanges();
        }

        public void UpdateTempBankNominalLedgerEntry(TempBankNominalLedgerEntry tempBankNominalLedgerEntry)
        {
            _tempBankNominalLedgerEntryService.Update(tempBankNominalLedgerEntry);
            _unitofWork.SaveChanges();
        }

        public void UpdateIsReconciled(Guid id, bool isReconciled)
        {
            var existingEntry = _tempBankNominalLedgerEntryService.GetTempBankNominalLenderEntriesByID(id);
            existingEntry.IsReconciled = isReconciled;
            _tempBankNominalLedgerEntryService.Update(existingEntry);
            _unitofWork.SaveChanges();
        }

        public void SaveTempBankNominalLedgerEntries(List<TempBankNominalLedgerEntry> tempBankNominalLedgerEntries)
        {
            _tempBankNominalLedgerEntryService.InsertRange(tempBankNominalLedgerEntries);
            _unitofWork.SaveChanges();
        }

        public void SaveBankStatementItemNominalLedgerEntries(List<BankStatementItemNominalLedgerEntry> bankStatementItemNominalLedgerEntries)
        {
            _bankStatementItemNominalLedgerEntryService.InsertRange(bankStatementItemNominalLedgerEntries);
            _unitofWork.SaveChanges();
        }

        public void SaveTempStatementMatch(Guid nominalId, Guid statementId)
        {
            TempBankStatementItemNominalLedgerEntry match = new TempBankStatementItemNominalLedgerEntry
            {
                BankStatementItemNominalLedgerEntryID = Guid.NewGuid(),
                BankStatementItemID = statementId,
                NominalLedgerEntryID = nominalId
            };
           
            _tempBankStatementItemNominalLedgerEntryService.Insert(match);
            _unitofWork.SaveChanges();
        }

        public void DeleteTempStatementMatch(Guid nominalId)
        {
            TempBankStatementItemNominalLedgerEntry match = new TempBankStatementItemNominalLedgerEntry
            {
                BankStatementItemNominalLedgerEntryID = Guid.NewGuid(),
                NominalLedgerEntryID = nominalId
            };
            var matches = _tempBankStatementItemNominalLedgerEntryService.GetByNominalID(nominalId);
            foreach (var m in matches)
            {
                _tempBankStatementItemNominalLedgerEntryService.Delete(m);
            }
            
            _unitofWork.SaveChanges();
        }

        public bool StatementIsMatched(Guid statementItemId)
        {
          var match =  _tempBankStatementItemNominalLedgerEntryService.GetByStatementID(statementItemId);

            var isReconciled = match.Count > 0 ? true : false;

            return isReconciled;
        }

        public List<Guid> GetMatchingStatements(Guid nominalId)
        {
            var m = from x in _tempBankStatementItemNominalLedgerEntryService.GetByNominalID(nominalId)
                select x.BankStatementItemID;

            var matches = m.ToList();
            
            //var matches2 = _tempBankStatementItemNominalLedgerEntryService.GetByNominalID(nominalId).Select(c => new Guid { c.BankStatementItemID });
            //List<Guid> matches3 = matches2.ToList<Guid>();
            //var matches = matches2.Cast<Guid>().ToList();
            return matches;
        }

        public Guid? GetCurrentStatementInReconciliation()
        {
            var currentReconciliation = _tempBankNominalLedgerEntryService.GetCurrentStatementInReconciliation();
            return currentReconciliation;
        }

        public decimal GetStatementTotalToReconcile(Guid bankStatementID)
        {
            var totalStatementTotalToReconcile =
                _bankStatementItemService.GetStatementTotalToReconcile(bankStatementID);
            return totalStatementTotalToReconcile;
        }

        public bool AllStatementItemsReconciled(Guid bankStatementID)
        {
            var toReconcile = GetStatementTotalToReconcile(bankStatementID);
            var hasReconciled = GetTotalIsReconciled(bankStatementID);
            bool isReconciled = toReconcile == hasReconciled;
            return isReconciled;
        }

        public void DeleteTempBankStatementItemNominalLedgerRange(ref List<TempBankStatementItemNominalLedgerEntry> tempBankNominalLedgerEntries)
        {
            _tempBankStatementItemNominalLedgerEntryService.DeleteAll();
            //foreach (var rec in tempBankNominalLedgerEntries)
            //{
            //    _tempBankStatementItemNominalLedgerEntryService.Delete(rec);
            //}
           
           // _unitofWork.SaveChanges();
        }

        public void DeleteTempBankNominalLedgerRange(ref List<TempBankNominalLedgerEntry> tempBankNominalLedgerEntries)
        {
            _tempBankNominalLedgerEntryService.DeleteAll();
            //foreach (var rec in tempBankNominalLedgerEntries)
            //{
            //    _tempBankNominalLedgerEntryService.Delete(rec);
            //}
            //_unitofWork.SaveChanges();
        }

        public decimal GetTotalIsReconciled(Guid bankStatementID)
        {
            // strange bug getting IsReconcilled will do manualy until resolved
            var nominalEntries = _tempBankNominalLedgerEntryService.GetTempBankNominalLenderEntriesByStatementID(bankStatementID);
            decimal totalMatched = 0;
            
            foreach (var n in nominalEntries)
            {
                var matchedrec = _tempBankStatementItemNominalLedgerEntryService.GetByNominalID(n.NominalLedgerEntryID);
                if (matchedrec.Count > 0)
                {
                    var relatedEntries =
                        _tempBankNominalLedgerEntryService.GetTempBankNominalLenderEntriesByID(n.NominalLedgerEntryID);

                    var sum = relatedEntries.TransactionAmount;
                    totalMatched = totalMatched + sum;
                }
            }
           
            //return _tempBankNominalLedgerEntryService.GetTotalIsReconciled(bankStatementID);
            return totalMatched;
        }

        //public TempBankNominalLedgerEntry GetTempBankNominalLedgerEntry(string fileName)
        //{

        //}
    }
}
