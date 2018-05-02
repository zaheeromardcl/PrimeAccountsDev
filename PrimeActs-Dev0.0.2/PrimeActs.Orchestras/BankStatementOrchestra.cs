using System;
using System.Collections.Generic;
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

namespace PrimeActs.Orchestras
{
    public interface IBankStatementOrchestra
    {
        //bool AddBankStatementHeader(BankStatementHeader model);
        BankStatement GetBankStatementHeader(string fileName);
        BankStatement GetBankStatementHeaderByID(Guid ID);
        List<BankStatementItem> GetBankStatementItems(Guid bankStatementID);
    }
    
    public class BankStatementOrchestra : IBankStatementOrchestra
    {
        private readonly IBankStatementService _bankStatementService;
        private readonly IBankStatementItemService _bankStatementItemService;
        private ApplicationUser _principal;
        private readonly IApplicationUserOrchestra _applicationUserOrchestra;
        private readonly ISetupLocalService _setupService;
        private readonly string _serverCode;

        public BankStatementOrchestra(ISetupLocalService setupLocalService, ISetupLocalService setupService, IBankStatementService bankStatementService, IBankStatementItemService bankStatementItemService, 
            IApplicationUserOrchestra applicationUserOrchestra)
        {
            
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
            _setupService = setupService;
            _bankStatementService = bankStatementService;
            _bankStatementItemService = bankStatementItemService;
            _applicationUserOrchestra = applicationUserOrchestra;
        }
        
        //public bool AddBankStatementHeader(BankStatementHeader model)
        //{
            
        //}

        public BankStatement GetBankStatementHeader(string fileName)
        {
            var bankStatementHeader = _bankStatementService.GetBankStatementByName(fileName);
            return bankStatementHeader;
        }

        public BankStatement GetBankStatementHeaderByID(Guid ID)
        {
            var bankStatementHeader = _bankStatementService.GetBankStatementByID(ID);
            return bankStatementHeader;
        }

        public List<BankStatementItem> GetBankStatementItems(Guid bankStatementID)
        {
            var bankStatementItems = _bankStatementItemService.GetBankStatementsByID(bankStatementID);
            return bankStatementItems;
        }

        public void RemoveBankStatement(Guid bankStatementID)
        {
            var bankStatementItems = _bankStatementItemService.GetBankStatementsByID(bankStatementID);

            foreach (var item in bankStatementItems)
            {
                _bankStatementItemService.Delete(item);
            }

            var bankStatementHeader = _bankStatementService.GetBankStatementByID(bankStatementID);
            _bankStatementService.Delete(bankStatementHeader);
        }
    }
}
