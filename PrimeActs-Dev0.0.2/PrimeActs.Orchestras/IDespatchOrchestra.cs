#region

using System.Collections.Generic;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IDespatchOrchestra
    {
        //List<DespatchEditModel> GetDespatchModelsForAC();

        List<DespatchEditModel> GetDespatchModelsForAC(string search);
    }
}