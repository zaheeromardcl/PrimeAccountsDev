#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IDespatchService : IService<DespatchLocation>
    {
        DespatchLocation DespatchByName(string PortName);
        DespatchLocation DespatchById(Guid Id);
        List<DespatchLocation> GetAllDespatches();
        void RefreshCache();
    }
}