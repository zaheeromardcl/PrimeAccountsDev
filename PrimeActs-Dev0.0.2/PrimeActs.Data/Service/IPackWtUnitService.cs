#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IPackWtUnitService : IService<PackWtUnit>
    {
        PackWtUnit PackWtUnitByWtUnit(string wtUnit);
        PackWtUnit PackWtUnitById(Guid Id);
        List<PackWtUnit> GetAllPackWtUnits();
        void RefreshCache();
    }
}