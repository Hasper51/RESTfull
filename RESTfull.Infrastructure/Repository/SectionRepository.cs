using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Infrastructure
{
    public class SectionRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public SectionRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<List<Section>> GetAllAsync()
        {
            return await _context.Sections.OrderBy(s => s.Title).ToListAsync();
        }

        public async Task<Section?> GetByIdAsync(Guid Id)
        {
            return await _context.Sections
                .Where(s => s.Id == Id)
                .Include(s => s.Discipline)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Section>> GetSectionsByDisciplineIdAsync(Guid disciplineId)
        {
            return await _context.Sections
                .Where(s => s.DisciplineId == disciplineId)
                .OrderBy(s => s.Title)
                .ToListAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            Section? section = await _context.Sections.FindAsync(Id);
            if (section != null)
            {
                _context.Remove(section);
                await _context.SaveChangesAsync();
            }
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }

        public async Task UpdateAsync(Section section)
        {
            var existSection = GetByIdAsync(section.Id).Result;
            if (existSection != null)
            {
                _context.Entry(existSection).CurrentValues.SetValues(section);
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Section section)
        {
            _context.Add(section);
            await _context.SaveChangesAsync();
        }
    }
}
