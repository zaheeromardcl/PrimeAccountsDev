#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IPorterageService : IService<Porterage>
    {
        Porterage PorterageByCode(string portCode);
        Porterage PorterageById(Guid Id);
        List<Porterage> GetAllPorterages();
        void RefreshCache();
    }
}