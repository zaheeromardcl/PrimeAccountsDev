using System;
using System.Collections.Generic;
using System.Xml;
using PrimeActs.Infrastructure.Caching;

namespace PrimeActs.Infrastructure.Localization
{
    public class XmlLocalizer : ILocalizer<Resource>
    {
        private readonly ICache _cache;
        private string _filepath;
        private string _cacheKey;
        private Dictionary<string, Resource> _localizationDict;

        private const string ResourceNode = "resource";
        private const string IdNode = "Id";
        private const string KeyNode = "Key";
        private const string ValueNode = "Value";

        public XmlLocalizer(ICache cache, string filepath)
        {
            if (cache == null)
            {
                throw new ArgumentNullException("cache");
            }

            _cache = cache;

            Initialize(filepath);
        }

        private void Initialize(string filepath)
        {
            _cacheKey = String.Format("XmlLocalizer_{0}", filepath);

            _filepath = filepath;

            if (_localizationDict == null)
            {
                _localizationDict = _cache.Get(_cacheKey) as Dictionary<string, Resource>;
            }

            if (_localizationDict == null)
            {
                ReadToCache();
            }
        }

        private void ReadToCache()
        {
            _localizationDict = new Dictionary<string, Resource>();
            using (var reader = XmlReader.Create(_filepath))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.Name == ResourceNode && reader.IsStartElement())
                    {
                        using (var innerReader = reader.ReadSubtree())
                        {
                            reader.MoveToContent();
                            var resource = new Resource();

                            while (innerReader.Read())
                            {
                                if (reader.Name != ResourceNode && reader.IsStartElement() && !String.IsNullOrWhiteSpace(reader.Name))
                                {
                                    switch (reader.Name)
                                    {
                                        case IdNode:
                                            resource.Id = Convert.ToInt32(reader.ReadString());
                                            break;
                                        case KeyNode:
                                            resource.Key = reader.ReadString();
                                            break;
                                        case ValueNode:
                                            resource.Value = reader.ReadString();
                                            break;
                                        default:
                                            resource.CompositeKey.Add(reader.Name, reader.ReadInnerXml());
                                            break;
                                    }
                                }
                            }
                            if (!string.IsNullOrWhiteSpace(resource.Key) && !_localizationDict.ContainsKey(resource.Key))
                            {
                                AddToDictionary(resource.Key, resource);
                            }
                        }
                    }
                }
            }

            SetCache();
        }

        public Resource GetValue(string key)
        {
            Resource output;
            _localizationDict.TryGetValue(key, out output);
            return output;
        }

        private void AddToDictionary(string key, Resource value)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key should not be empty");
            }
            _localizationDict.Add(key, value);
        }

        private void SetCache()
        {
            _cache.Set(_cacheKey, _localizationDict);
        }

        public void AddValue(string key, Resource value)
        {
            AddToDictionary(key, value);
            SetCache();
        }
    }
}