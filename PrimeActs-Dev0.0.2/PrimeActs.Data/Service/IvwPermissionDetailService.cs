#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface IvwPermissionDetailService : IService<vwPermissionDetail>
    {
        List<vwPermissionDetail> GetPermissionDetailByUserID(Guid UserID);
    }
}
