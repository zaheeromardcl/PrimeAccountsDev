#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IPortService : IService<Port>
    {
        Port PortByName(string PortName);
        Port PortById(Guid Id);
        List<Port> GetAllPorts();
        void RefreshCache();
    }
}