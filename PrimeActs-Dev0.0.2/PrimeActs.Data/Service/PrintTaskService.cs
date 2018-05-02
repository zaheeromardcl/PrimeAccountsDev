using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

namespace PrimeActs.Data.Service
{
    public interface IPrintTaskService : IService<PrintTask>
    {
        List<PrintTask> PrintTaskByName(string PrintTaskName);
        PrintTask PrintTaskByID(Guid ID);
        void RefreshCache();
        
    }

    public class PrintTaskService : Service<PrintTask>, IPrintTaskService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<PrintTask> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;
        
        public PrintTaskService(IRepositoryAsync<PrintTask> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (PrintTask).FullName;
        }

        public PrintTask PrintTaskByID(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<PrintTask>).Where(t => t.PrintTaskID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<PrintTask> PrintTaskByName(string printTaskName)
        {
           // CheckCache();
           // var data = (_cache.Get(type) as IEnumerable<PrintTask>).Where(t => t.PrintTaskName == printTaskName);
            var printTaskList =
                _repository.Query()
                    .Select()
                    .Where(t => t.PrintTaskName == printTaskName)
                    .ToList();
            return printTaskList == null ? null: printTaskList;
            //  return data == null ? null : data.ToList();
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
                    var ports = new List<PrintTask>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        ports.Add(new PrintTask
                        {
                            PrintTaskID = entityType.PrintTaskID,
                            PrintTaskName = entityType.PrintTaskName,
                            HasColour = entityType.HasColour,
                            RequireColour = entityType.RequireColour,
                            HasRaw = entityType.HasRaw,
                            RequireRaw = entityType.RequireRaw,
                            HasTractor = entityType.HasTractor,
                            RequireTractor = entityType.RequireTractor
                        });
                    }
                    _cache.Set(type, ports);
                }
            }
            return type;
        }
    }
}
