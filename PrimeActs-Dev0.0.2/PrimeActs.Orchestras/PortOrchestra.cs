#region

using System.Collections.Generic;
using System.Linq;
using PrimeActs.Data.Service;
using PrimeActs.Domain;
using PrimeActs.Domain.ViewModels;

#endregion

namespace PrimeActs.Orchestras
{
    public class PortOrchestra : IPortOrchestra
    {
        private readonly IPortService _portService;

        public PortOrchestra(IPortService portService)
        {
            _portService = portService;
        }

        public List<PortEditModel> GetPortModelsForAC()
        {
            return _portService.GetAllPorts().Select(BuildPortEditModelAC).ToList();
        }

        private PortEditModel BuildPortEditModelAC(Port entity)
        {
            return new PortEditModel
            {
                PortID = entity.PortID,
                PortCode = entity.PortCode,
                PortName = entity.PortName
            };
        }
    }
}