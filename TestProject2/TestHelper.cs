using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RESTfull.Infrastructure;
using RESTfull.Domain;
namespace TestProject2
{
    public class TestHelper
    {
        private readonly Context _context;

        public TestHelper()
        {
            var contextOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Test")
                .Options;
            _context = new Context(contextOptions);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var discipline1 = new Discipline
            {
                Title = "Math",
                Attestation = "Exan",
                Hours = 100
            };
            discipline1.AddSection(new Section { Title = "Equations", Content = "x=2y" });
            discipline1.AddSection(new Section { Title = "Graphs", Content = "hyperbola" });
            _context.Disciplines.Add(discipline1);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public DisciplineRepository DisciplineRepository
        {
            get
            {
                return new DisciplineRepository(_context);
            }
        }

    }
}
