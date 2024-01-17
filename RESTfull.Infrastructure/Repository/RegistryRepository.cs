using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace RESTfull.Infrastructure
{
    public class RegistryRepository
    {
        private readonly Context _context;

        public Context UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        public RegistryRepository(Context context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<List<Registry>> GetAllAsync()
        {
            return await _context.Registries.OrderBy(r => r.Name).ToListAsync();
        }

        public async Task<Registry?> GetByIdAsync(Guid Id)
        {
            return await _context.Registries
                .Where(r => r.Id == Id)
                .Include(r => r.Disciplines)
                .FirstOrDefaultAsync();
        }

        public async Task<Registry?> GetByNameAsync(string Name)
        {
            return await _context.Registries
                .Where(r => r.Name == Name)
                .Include(r => r.Disciplines)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteAsync(Guid Id)
        {
            Registry? registry = await _context.Registries.FindAsync(Id);
            if (registry != null)
            {
                _context.Remove(registry);
                await _context.SaveChangesAsync();
            }
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }

        public async Task UpdateAsync(Registry registry)
        {
            var existRegistry = GetByIdAsync(registry.Id).Result;
            if (existRegistry != null)
            {
                _context.Entry(existRegistry).CurrentValues.SetValues(registry);
                foreach (var registry1 in registry.Disciplines)
                {
                    var existDiscipline =
                        existRegistry.Disciplines.FirstOrDefault(d => d.Id == registry1.Id);
                    if (existDiscipline == null)
                    {
                        existRegistry.Disciplines.Add(registry1);
                    }
                    else
                    {
                        _context.Entry(existDiscipline).CurrentValues.SetValues(registry1);
                    }
                }
                foreach (var existDiscipline in existRegistry.Disciplines)
                {
                    if (!registry.Disciplines.Any(d => d.Id == existDiscipline.Id))
                    {
                        _context.Remove(existDiscipline);
                    }
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddAsync(Registry registry)
        {
            _context.Add(registry);
            await _context.SaveChangesAsync();

        }
    }
}
