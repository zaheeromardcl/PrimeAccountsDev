#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IMasterGroupService : IService<MasterGroup>
    {
        MasterGroup MasterGroupByName(string MasterGroupName);
        MasterGroup MasterGroupById(Guid Id);
        List<MasterGroup> GetAllMasterGroups();
        void RefreshCache();
    }
}