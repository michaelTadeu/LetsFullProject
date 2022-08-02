using LetsFullProject.Configurations;
using LetsFullProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsFullProject.Data
{
    public class LetsFullProjectContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<UsuarioLikeProjeto> UsuariosLikeProjetos { get; set; }
        public LetsFullProjectContext(DbContextOptions<LetsFullProjectContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connection = @"Server=DESKTOP-1RISHQ4\SQLEXPRESS;Database=letLike_FullProject;Trusted_Connection=True;";
                
                optionsBuilder.UseSqlServer(connection);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new ProjetoConfiguration());
            modelBuilder.ApplyConfiguration(new UsuarioLikeProjetoConfiguration());
        }


    }
}
