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
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=RESTfull")
                .Options;
            
            _context = new Context(contextOptions);
            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
            /*
            Discipline discipline1 = new Discipline
            {
                Title = "Философия", 
                Attestation = "Exam",
                Hours = 140
            };
            discipline1.AddSection(new Section { Title = "Древняя эпоха", Content = "Сократ" });
            discipline1.AddSection(new Section { Title = "Средние века", Content = "Религия" });

            Registry reg = new Registry
            {
                Name = "Гуманитарный"
            };
            reg.AddDiscipline(discipline1);
            _context.Registries.Add(reg);
            */



            

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
