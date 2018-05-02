#region

using System.Collections.Generic;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public interface IPortOrchestra
    {
        List<PortEditModel> GetPortModelsForAC();
    }
}