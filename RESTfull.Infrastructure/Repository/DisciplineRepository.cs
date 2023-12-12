using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTfull.Infrastructure
{
    public class DisciplineRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public DisciplineRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<List<Discipline>> GetAllAsync()
        {
            return await _context.Disciplines.OrderBy(p => p.Title).ToListAsync();
        }

        public async Task<Discipline?> GetByIdAsync(Guid Id)
        {
            return await _context.Disciplines
                .Where(p => p.Id == Id)
                .Include(p => p.Sections)
                .FirstOrDefaultAsync();
        }

        public async Task<Discipline?> GetByTitleAsync(string Title)
        {
            return await _context.Disciplines
                .Where(p => p.Title == Title)
                .Include(p => p.Sections)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            Discipline? discipline = await _context.Disciplines.FindAsync(Id);
            if (discipline != null)
            {
                _context.Remove(discipline);
                await _context.SaveChangesAsync();
            }
        }
        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
        public async Task UpdateAsync(Discipline discipline)
        {
            var existDiscipline = GetByIdAsync(discipline.Id).Result;
            if (existDiscipline != null)
            {
                _context.Entry(existDiscipline).CurrentValues.SetValues(discipline);
                foreach (var Title in discipline.Sections)
                {
                    var existSectionTitle =
                        existDiscipline.Sections.FirstOrDefault(pn => pn.Id == Title.Id);
                    if (existSectionTitle == null)
                    {
                        existDiscipline.Sections.Add(Title);
                    }
                    else
                    {
                        _context.Entry(existSectionTitle).CurrentValues.SetValues(Title);
                    }
                }
                foreach (var existTitle in existDiscipline.Sections)
                {
                    if (!discipline.Sections.Any(pn => pn.Id == existTitle.Id))
                    {
                        _context.Remove(existTitle);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Discipline discipline)
        {
            _context.Add(discipline);
            await _context.SaveChangesAsync();
        }
    }
}
