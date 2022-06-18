using Microsoft.EntityFrameworkCore;
using Reflexobot.Data;
using Reflexobot.Entities;
using Reflexobot.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflexobot.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly ReflexobotContext _context;
        private readonly DbSet<Note> _dbSet;
        public NoteRepository(ReflexobotContext context)
        {
            _context = context;
            _dbSet = context.Set<Note>();
        }

        public IEnumerable<Note> GetNotes()
        {
            var data = _dbSet.AsNoTracking();
            return data;
        }

        public async Task AddNoteAsync(Note note)
        {
            await _dbSet.AddAsync(note);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateNoteAsync(Note note)
        {
            var currentNote = await _dbSet.FirstOrDefaultAsync(x => x.Guid == note.Guid);
            if (currentNote != null)
            {
                currentNote.Text = note.Text;
                _context.Entry(currentNote).CurrentValues.SetValues(currentNote);
            }

            await _context.SaveChangesAsync();
        }
        public async Task DeleteNoteAsync(Guid noteGuid)
        {
            var currentNote = await _dbSet.FirstOrDefaultAsync(x => x.Guid == noteGuid);
            if (currentNote != null)
            {
                 _dbSet.Remove(currentNote);
                await _context.SaveChangesAsync();
            }

        }
    }
}
