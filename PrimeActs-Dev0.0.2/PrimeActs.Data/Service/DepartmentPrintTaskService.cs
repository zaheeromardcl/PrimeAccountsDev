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
    public interface IDepartmentPrintTaskService : IService<DepartmentPrintTask>
    {
       // List<DepartmentPrintTask> DepartmentPrintTaskByName(string PrintTaskName);
        DepartmentPrintTask DepartmentPrintTaskByID(Guid ID);
        List<DepartmentPrintTask> DepartmentPrintTaskByTaskID(Guid ID);
        void RefreshCache();
    }

    public class DepartmentPrintTaskService : Service<DepartmentPrintTask>, IDepartmentPrintTaskService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<DepartmentPrintTask> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;

        public DepartmentPrintTaskService(IRepositoryAsync<DepartmentPrintTask> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof(DepartmentPrintTask).FullName;
        }

        public DepartmentPrintTask DepartmentPrintTaskByID(Guid Id)
        {
           CheckCache();
            var data = (_cache.Get(type) as IEnumerable<DepartmentPrintTask>).Where(t => t.DepartmentPrintTaskID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<DepartmentPrintTask> DepartmentPrintTaskByTaskID(Guid Id)
        {
            var listPrintTasks = _repository.Query().Include(t => t.DepartmentPrinter)
                .Select()
                .Where(x => x.PrintTaskID == Id)
                .ToList();
            return listPrintTasks;
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
                    var ports = new List<DepartmentPrintTask>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        ports.Add(new DepartmentPrintTask
                        {
                            DepartmentPrintTaskID = entityType.DepartmentPrintTaskID,
                            DepartmentPrinterID = entityType.DepartmentPrinterID,
                            PrintTaskID = entityType.PrintTaskID
                        });
                    }
                    _cache.Set(type, ports);
                }
            }
            return type;
        }
    }
}

