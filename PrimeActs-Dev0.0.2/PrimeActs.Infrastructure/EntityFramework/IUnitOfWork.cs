#region

using System;
using System.Data;
using PrimeActs.Infrastructure.BaseEntities;

#endregion

namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        void Dispose(bool disposing);
        IRepository<TEntity> Repository<TEntity>() where TEntity : class, IObjectState;
        void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.Unspecified);
        bool Commit();
        void Rollback();
    }
}