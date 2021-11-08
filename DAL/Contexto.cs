using Microsoft.EntityFrameworkCore;
using P2_AP2_Darianna_20190261.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2_AP2_Darianna_20190261.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Proyectos> Proyectos { get; set; }
        public DbSet<Tareas> Tareas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source = DATA/datosProyectos.Db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tareas>().HasData(
                    new Tareas() { TipoTarea = "Analisis", TareaId = 1 },
                    new Tareas() { TipoTarea = "Diseño", TareaId = 2 },
                    new Tareas() { TipoTarea = "Programación", TareaId = 3 },
                    new Tareas() { TipoTarea = "Prueba", TareaId = 4 }
                );
        }
    }
}
