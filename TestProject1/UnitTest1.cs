using Microsoft.EntityFrameworkCore;
using RESTfull.Domain;

namespace RESTfull.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Registry> Registries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Устанавливаем свойство RegistryId как внешний ключ для связи между Discipline и Registry
            modelBuilder.Entity<Discipline>()
                        .HasOne(d => d.Registry)
                        .WithMany(r => r.Disciplines)
                        .HasForeignKey(d => d.RegistryId);
        }
    }
}
