namespace PrimeActs.Infrastructure.Localization
{
    public interface ILocalizerFactory
    {
        ILocalizer<Resource> CreateXmlLocalizer(string filepath);
    }
}