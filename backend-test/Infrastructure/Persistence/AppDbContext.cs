using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MarcaAuto> MarcaAutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarcaAuto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto { Id = 1, Nombre = "Toyota", PaisOrigen = "Japon" },
                new MarcaAuto { Id = 2, Nombre = "Ford", PaisOrigen = "Estados Unidos" },
                new MarcaAuto { Id = 3, Nombre = "Chevrolet", PaisOrigen = "Estados Unidos" },
                new MarcaAuto { Id = 4, Nombre = "Nissan", PaisOrigen = "Japon" }
            );
        }
    }
}
