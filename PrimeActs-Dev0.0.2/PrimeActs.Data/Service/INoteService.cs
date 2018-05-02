#region

using System;
using System.Collections.Generic;
using PrimeActs.Domain;

#endregion

namespace PrimeActs.Data.Service
{
    public interface INoteService : IService<Note>
    {
        Note NoteByName(string author);
        Note NoteById(Guid Id);
        List<Note> GetAllNotes();
        void RefreshCache();
    }
}