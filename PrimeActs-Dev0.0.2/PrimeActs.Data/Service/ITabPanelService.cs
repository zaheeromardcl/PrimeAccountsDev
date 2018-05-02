#region
using System;
using System.Collections.Generic;
using PrimeActs.Domain;
#endregion

namespace PrimeActs.Data.Service
{
   
    public interface ITabPanelService : IService<UserTabPanel>
    {
        UserTabPanel TabPanelById(Guid Id, string Name);
        List<UserTabPanel> GetAllTabPanels(Guid UserId);
        //void DeleteTabPanel(Guid UserId, string Name);


    }
}
