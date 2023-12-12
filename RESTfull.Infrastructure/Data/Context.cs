using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Win32;
using RESTfull.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Collections.Specialized.BitVector32;
using Registry = RESTfull.Domain.Registry;
using Section = RESTfull.Domain.Section;

namespace RESTfull.Infrastructure
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public DbSet<Section> Sections { get; set; }
        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Registry> Registries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>()
            .HasOne(d => d.Registry) // предполагаем, что у Discipline есть навигационное свойство Registry
            .WithMany()
            .HasForeignKey(d => d.RegistryId); // здесь указываем имя внешнего ключа, которое не вызовет конфликтов

        }

    }
}
