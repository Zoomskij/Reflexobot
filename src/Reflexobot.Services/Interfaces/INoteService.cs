using Reflexobot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services.Interfaces
{
    public interface INoteService
    {
        IEnumerable<Note> GetNotes();
        Task AddNoteAsync(string text, Guid studentGuid);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(Guid noteGuid);
    }
}
