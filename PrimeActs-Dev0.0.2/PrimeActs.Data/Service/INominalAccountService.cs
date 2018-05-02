#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface INominalAccountService : IService<NominalAccount>
    {
        NominalAccount NominalAccountByName(string code);
        NominalAccount NominalAccountById(Guid id);
        List<NominalAccount> GetAllNominalAccounts();
        void RefreshCache();
    }
}
