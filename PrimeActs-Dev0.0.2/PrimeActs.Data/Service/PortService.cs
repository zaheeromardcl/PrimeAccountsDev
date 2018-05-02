#region

using System;
using System.Collections.Generic;
using System.Linq;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class PortService : Service<Port>, IPortService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Port> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public PortService(IRepositoryAsync<Port> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Port).FullName;
        }

        public Port PortByName(string portName)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Port>).Where(t => t.PortName == portName);
            return data == null ? null : data.FirstOrDefault();
        }

        public Port PortById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Port>).Where(t => t.PortID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(Port port)
        {
            _repository.Insert(port);
            RefreshCache();
        }

        public List<Port> GetAllPorts()
        {
            CheckCache();
            return (_cache.Get(type) as List<Port>);
        }


        public void RefreshCache()
        {
            _cache.Remove(type);
        }

        private string CheckCache()
        {
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var ports = new List<Port>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        ports.Add(new Port
                        {
                            PortID = entityType.PortID,
                            PortName = entityType.PortName,
                            PortCode = entityType.PortCode,
                            CompanyID = entityType.CompanyID
                        });
                    }
                    _cache.Set(type, ports);
                }
            }
            return type;
        }
    }
}