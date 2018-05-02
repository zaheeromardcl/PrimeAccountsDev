
namespace PrimeActs.Infrastructure.Caching
{
    public enum CacheType
    {
        Null = 0,
        
        Memory,

        AppFabric,

        AzureTableStorage,

        Disk
    }
}
