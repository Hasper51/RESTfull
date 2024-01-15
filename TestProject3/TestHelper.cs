using RESTfull.Infrastructure;
using RESTfull.Domain;

using Microsoft.EntityFrameworkCore;


namespace TestProject3
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
            Discipline discipline1 = new Discipline
            {
                Title = "Math", 
                Attestation = "Exam",
                Hours = 100
            };
            discipline1.AddSection(new Section { Title = "Equations", Content = "x=2y" });
            discipline1.AddSection(new Section { Title = "Graphs", Content = "hyperbola" });

            Registry reg = new Registry
            {
                Name = "IT"
            };
            reg.AddDiscipline(discipline1);
            _context.Registries.Add(reg);
            
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
        }

        public RegistryRepository RegistryRepository
        {
            get
            {
                return new RegistryRepository(_context);
            }
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
