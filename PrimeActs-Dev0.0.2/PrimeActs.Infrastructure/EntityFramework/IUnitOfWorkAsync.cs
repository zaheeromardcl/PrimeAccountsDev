#region

using System.Threading;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

#endregion

namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IUnitOfWorkAsync : IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        IRepositoryAsync<TEntity> RepositoryAsync<TEntity>() where TEntity : class, IObjectState;
    }
}