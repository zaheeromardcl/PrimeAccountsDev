#region

using System.Threading;
using System.Threading.Tasks;
using PrimeActs.Infrastructure.BaseEntities;

#endregion

namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IRepositoryAsync<TEntity> : IRepository<TEntity> where TEntity : class, IObjectState
    {
        IDataContextAsync Context { get; }
        Task<TEntity> FindAsync(params object[] keyValues);
        Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);
        Task<bool> DeleteAsync(params object[] keyValues);
        Task<bool> DeleteAsync(CancellationToken cancellationToken, params object[] keyValues);

        
    }
}