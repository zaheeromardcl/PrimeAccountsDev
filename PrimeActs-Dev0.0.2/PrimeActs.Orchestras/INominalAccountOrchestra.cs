#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface INominalAccountOrchestra
    {
        void Initialize(ApplicationUser principal);
        List<NominalAccountModel> GetNominalAccountModelsForAC(string search, Guid companyID);
    }
}
