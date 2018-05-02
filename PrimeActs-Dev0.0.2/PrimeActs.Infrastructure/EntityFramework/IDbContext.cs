#region

using System;
using PrimeActs.Infrastructure.BaseEntities;

#endregion

namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IDbContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}