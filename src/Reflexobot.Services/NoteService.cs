using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using Reflexobot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public IEnumerable<Note> GetNotes()
        {
            return _noteRepository.GetNotes();
        }
        public async Task AddNoteAsync(string text, Guid studentGuid)
        {
            var note = new Note
            {
                Text = text,
                StudentGuid = studentGuid
            };
            await _noteRepository.AddNoteAsync(note);
        }
        public async Task UpdateNoteAsync(Note note)
        {
            await _noteRepository.UpdateNoteAsync(note);
        }
        public async Task DeleteNoteAsync(Guid noteGuid)
        {
            await _noteRepository.DeleteNoteAsync(noteGuid);
        }
    }
}
