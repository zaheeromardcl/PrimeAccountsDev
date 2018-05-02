using System.Collections.Generic;

namespace PrimeActs.Infrastructure.Localization
{
    public class Resource
    {
        public Resource()
        {
            CompositeKey = new Dictionary<string, string>();
        }
        public int Id { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> CompositeKey { get; set; }
        public string Value { get; set; }
    }
}