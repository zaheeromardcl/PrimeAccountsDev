using PrimeActs.Infrastructure.Caching;

namespace PrimeActs.Infrastructure.Localization
{
    public class LocalizerFactory : ILocalizerFactory
    {
        private readonly ICache _cache;

        public LocalizerFactory(ICache cache)
        {
            _cache = cache;
        }

        public ILocalizer<Resource> CreateXmlLocalizer(string filepath)
        {
            return new XmlLocalizer(_cache, filepath);
        }
    }
}