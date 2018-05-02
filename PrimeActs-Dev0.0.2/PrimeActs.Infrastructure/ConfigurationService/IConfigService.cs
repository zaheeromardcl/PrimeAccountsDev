namespace PrimeActs.Infrastructure.ConfigurationService
{
    public interface IConfigurationService
    {
        string PrimeActsConnection { get; }
        string URL { get; }
    }
}