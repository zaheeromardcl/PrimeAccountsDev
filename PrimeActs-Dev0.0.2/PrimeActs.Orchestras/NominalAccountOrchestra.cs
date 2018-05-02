#region
using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;
using PrimeActs.Infrastructure.BaseEntities;
#endregion

namespace PrimeActs.Orchestras
{
    public class NominalAccountOrchestra : INominalAccountOrchestra
    {
        private readonly INominalAccountService _nominalAccountService;
        private readonly INoteService _noteService;
        private ApplicationUser _principal;
        private readonly string _serverCode;

        public NominalAccountOrchestra(INominalAccountService nominalAccountService,
            ISetupLocalService setupLocalService)
        {
            _nominalAccountService = nominalAccountService;
            var setting = setupLocalService.Find("ServerCode");
            _serverCode = setting != null ? setting.SetupValueNvarchar : "L";
        }

        public void Initialize(ApplicationUser principal)
        {
            _principal = principal;
        }

        public List<NominalAccountModel> GetNominalAccountModelsForAC(string search, Guid companyID)
        {
            List<NominalAccountModel> nominalAccountModels = new List<NominalAccountModel>();

            var accounts = _nominalAccountService.Query(x => x.CompanyID == companyID &&
                (x.NominalCode.StartsWith(search) || x.NominalAccountName.StartsWith(search))
                ).Select().ToList();

            foreach (var item in accounts)
            {
                nominalAccountModels.Add(BuildNominalAccountModelAC(item));
            }
            return nominalAccountModels;  
        }

        private NominalAccountModel BuildNominalAccountModelAC(NominalAccount sdentity)
        {
            NominalAccountModel strNominalAccountList = new NominalAccountModel();
            strNominalAccountList.NominalAccountName = sdentity.NominalAccountName;
            strNominalAccountList.NominalAccountID = sdentity.NominalAccountID;
            strNominalAccountList.NominalCode = sdentity.NominalCode;
            
            return strNominalAccountList;
        }

    }
}
