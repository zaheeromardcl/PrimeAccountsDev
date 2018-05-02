#region

using System.Threading;
using System.Threading.Tasks;

#endregion

namespace PrimeActs.Infrastructure.EntityFramework
{
    public interface IDataContextAsync : IDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync();
    }
}