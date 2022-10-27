using System;
using Pomelo.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tools.Models;
using Tools.Data.Mappings;

namespace Tools.Data
{
    public class ToolsDataContext : DbContext
    {
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Instrutor> Instrutores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Database=Tools;Uid=root;Pwd=root";
            optionsBuilder.UseMySql(
                connectionString,
                ServerVersion.AutoDetect(connectionString)
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CursoMap());
            modelBuilder.ApplyConfiguration(new InstrutorMap());
        }
    }
}