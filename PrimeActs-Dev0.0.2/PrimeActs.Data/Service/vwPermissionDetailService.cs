#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;
using PrimeActs.Domain.ViewModels.Users;

#endregion

namespace PrimeActs.Data.Service
{
    public class vwPermissionDetailService : Service<vwPermissionDetail>, IvwPermissionDetailService
    {
        private readonly IRepositoryAsync<vwPermissionDetail> _repository;

        public vwPermissionDetailService(IRepositoryAsync<vwPermissionDetail> repository)
            : base(repository)
        {
            _repository = repository;
        }
        public List<vwPermissionDetail> GetPermissionDetailByUserID(Guid userID)
        {
            List<vwPermissionDetail> vwPermissionDetails = _repository.Query(t => t.UserID == userID).Select().OrderBy(t => t.PermissionDetailID).ToList();
            return vwPermissionDetails;
        }
    }
}
