using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public class PermissionService : Service<Permission>, IPermissionService
    {
        private readonly IRepositoryAsync<Permission> _repository;

        public PermissionService(IRepositoryAsync<Permission> repository) : base(repository)
        {
            _repository = repository;
        }

        public Permission FindById(Guid id, bool withRoles)
        {
            var query = _repository.Query(x => x.PermissionID == id);
            if (withRoles)
            {
                throw new NotImplementedException();
                //query = query.Include(x => x.ApplicationRoles);
            }
            
            var permission = query.Select().FirstOrDefault();
            
            return permission;
        }
    }
}
