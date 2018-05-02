#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using PrimeActs.Domain;
using PrimeActs.Infrastructure.Cache;
using PrimeActs.Infrastructure.EntityFramework;

#endregion

namespace PrimeActs.Data.Service
{
    public class ProduceGroupService : Service<ProduceGroup>, IProduceGroupService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<ProduceGroup> _repository;
        private readonly object lockboject = new object();

        public ProduceGroupService(IRepositoryAsync<ProduceGroup> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
        }

        public ProduceGroup ProduceGroupByName(string produceGroupName)
        {
            var type = typeof (ProduceGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var produceGroups = new List<ProduceGroup>();
                    foreach (var entityType in _repository.Query().Select().ToList())
                    {
                        produceGroups.Add(new ProduceGroup
                        {
                            ProduceGroupID = entityType.ProduceGroupID,
                            ProduceGroupName = entityType.ProduceGroupName
                        });
                    }
                    _cache.Set(type, produceGroups);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<ProduceGroup>).Where(t => t.ProduceGroupName == produceGroupName);
            return data == null ? null : data.FirstOrDefault();
        }

        public ProduceGroup ProduceGroupIncludeProduceByName(string produceGroupName, Guid optDepartmentID = default(Guid))
        {
            var produceGroups = new List<ProduceGroup>();
            //var produceGroupsold = _repository.Query().Include(m => m.Produces).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p=> p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var test = produceGroups.Select(a => a.ProduceGroupDepartments.Where( b => b.DepartmentID == Guid.Parse("76000400-0000-0070-9204-000068336078"))).ToList();
            if (optDepartmentID == default(Guid))
            {
                produceGroups =
                    _repository.Query()
                        .Include(m => m.Produces)
                        .Select()
                        .Where(f => f.ProduceGroupName == produceGroupName)
                        .ToList();
            }
            else
            {
                var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p => p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName == produceGroupName);
                produceGroups =
                    produceGroups1.Where(
                        a =>
                            a.ProduceGroupDepartments.Any(
                                b => b.DepartmentID == optDepartmentID)).ToList();
            }
            return produceGroups == null ? null : produceGroups.FirstOrDefault();
        }

        public ProduceGroup ProduceGroupIncludeProduceByID(Guid produceGroupID, Guid optDepartmentID = default(Guid))
        {
            var produceGroups = new List<ProduceGroup>();
            if (optDepartmentID == default(Guid))
            {
                produceGroups =
                    _repository.Query()
                        .Include(m => m.Produces)
                        .Select()
                        .Where(f => f.ProduceGroupID == produceGroupID)
                        .ToList();
            }
            else
            {
                var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p => p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupID == produceGroupID);
                produceGroups =
                    produceGroups1.Where(
                        a =>
                            a.ProduceGroupDepartments.Any(
                                b => b.DepartmentID == optDepartmentID)).ToList();
            }
            return produceGroups == null ? null : produceGroups.FirstOrDefault();
        }

        public List<ProduceGroup> ProduceGroupIncludeProduceByNameRange(string produceGroupStartName, string produceGroupEndName, Guid optDepartmentID = default(Guid))
        {
            var produceGroups = new List<ProduceGroup>();
            //var produceGroupsold = _repository.Query().Include(m => m.Produces).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p=> p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var test = produceGroups.Select(a => a.ProduceGroupDepartments.Where( b => b.DepartmentID == Guid.Parse("76000400-0000-0070-9204-000068336078"))).ToList();
            if (optDepartmentID == default(Guid))
            {
                produceGroups =
                    _repository.Query()
                        .Include(m => m.Produces)
                        .Select()
                        .Where(f => f.ProduceGroupName.CompareTo(produceGroupStartName) >= 0 && f.ProduceGroupName.CompareTo(produceGroupEndName) <= 0) 
                        .ToList();
            }
            else
            {
                var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p => p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName.CompareTo(produceGroupStartName) >= 0 && f.ProduceGroupName.CompareTo(produceGroupEndName) <= 0);
                produceGroups =
                    produceGroups1.Where(
                        a =>
                            a.ProduceGroupDepartments.Any(
                                b => b.DepartmentID == optDepartmentID)).ToList();
            }
            //return produceGroups == null ? null : produceGroups.FirstOrDefault();
            return produceGroups.OrderBy(a=> a.ProduceGroupName).ToList();
        }

        public List<ProduceGroup> ProduceGroupIncludeProduceByDepartment(Guid optDepartmentID = default(Guid))
        {
            var produceGroups = new List<ProduceGroup>();
            //var produceGroupsold = _repository.Query().Include(m => m.Produces).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p=> p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var test = produceGroups.Select(a => a.ProduceGroupDepartments.Where( b => b.DepartmentID == Guid.Parse("76000400-0000-0070-9204-000068336078"))).ToList();
            if (optDepartmentID == default(Guid))
            {
                produceGroups =
                    _repository.Query()
                        .Include(m => m.Produces)
                        .Select()
                        .ToList();
            }
            else
            {
                var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p => p.ProduceGroupDepartments).Select();
                produceGroups =
                    produceGroups1.Where(
                        a =>
                            a.ProduceGroupDepartments.Any(
                                b => b.DepartmentID == optDepartmentID)).ToList();
            }
            //return produceGroups == null ? null : produceGroups.FirstOrDefault();
            return produceGroups.OrderBy(a => a.ProduceGroupName).ToList();
        }

        public ProduceGroup ProduceGroupIncludeProduce(Guid optDepartmentID = default(Guid))
        {
            var produceGroups = new List<ProduceGroup>();
            //var produceGroupsold = _repository.Query().Include(m => m.Produces).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p=> p.ProduceGroupDepartments).Select().Where(f => f.ProduceGroupName == produceGroupName).ToList();
            //var test = produceGroups.Select(a => a.ProduceGroupDepartments.Where( b => b.DepartmentID == Guid.Parse("76000400-0000-0070-9204-000068336078"))).ToList();
            if (optDepartmentID == default(Guid))
            {
                produceGroups =
                    _repository.Query()
                        .Include(m => m.Produces)
                        .Select()
                        .ToList();
            }
            else
            {
                var produceGroups1 = _repository.Query().Include(m => m.Produces).Include(p => p.ProduceGroupDepartments).Select();
                produceGroups =
                    produceGroups1.Where(
                        a =>
                            a.ProduceGroupDepartments.Any(
                                b => b.DepartmentID == optDepartmentID)).ToList();
            }
            return produceGroups == null ? null : produceGroups.FirstOrDefault();
        }

        public ProduceGroup ProduceGroupById(Guid Id)
        {
            var type = typeof (ProduceGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    var companies = new List<ProduceGroup>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        companies.Add(new ProduceGroup
                        {
                            ProduceGroupID = entityType.ProduceGroupID,
                            ProduceGroupName = entityType.ProduceGroupName
                        });
                    }
                    _cache.Set(type, companies);
                }
            }
            var data = (_cache.Get(type) as IEnumerable<ProduceGroup>).Where(t => t.ProduceGroupID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public List<ProduceGroup> GetAllProduceGroups()
        {
            var type = typeof (ProduceGroup).FullName;
            if (!_cache.Exists(type))
            {
                lock (lockboject)
                {
                    _cache.Set(type, _repository.Query().Select().ToList());
                }
            }
            return (_cache.Get(type) as List<ProduceGroup>);
            ;
        }

        public void RefreshCache()
        {
            _cache.Remove(typeof (ProduceGroup).FullName);
        }
    }
}