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
    public class NoteService : Service<Note>, INoteService
    {
        private readonly ICache _cache;
        private readonly IRepositoryAsync<Note> _repository;
        private readonly object lockboject = new object();
        private readonly string type = string.Empty;


        public NoteService(IRepositoryAsync<Note> repository, ICache cache)
            : base(repository)
        {
            _repository = repository;
            _cache = cache;
            type = typeof (Note).FullName;
        }

        public Note NoteByName(string author)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Note>);
            return data == null ? null : data.FirstOrDefault();
        }

        public Note NoteById(Guid Id)
        {
            CheckCache();
            var data = (_cache.Get(type) as IEnumerable<Note>).Where(t => t.NoteID == Id);
            return data == null ? null : data.FirstOrDefault();
        }

        public override void Insert(Note port)
        {
            _repository.Insert(port);
            RefreshCache();
        }

        public List<Note> GetAllNotes()
        {
            CheckCache();
            return (_cache.Get(type) as List<Note>);
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
                    var ports = new List<Note>();
                    foreach (var entityType in _repository.Query().Select())
                    {
                        ports.Add(new Note
                        {
                            NoteID = entityType.NoteID,
                            NoteText = entityType.NoteText,
                            NoteDescription = entityType.NoteDescription
                        });
                    }
                    _cache.Set(type, ports);
                }
            }
            return type;
        }
    }
}