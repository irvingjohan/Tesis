using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RIEGO.Shared.Entidades;


namespace RIEGO.Server
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> dbContextOptions)
           : base(dbContextOptions)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Horario> Horario { get; set; }
        public DbSet<Llave> Llave { get; set; }
        public DbSet<RelacionLlaveHorario> RelacionLlaveHorario { get; set; }
        public DbSet<Riego> Riego { get; set; }
        public DbSet<Seccion> Seccion { get; set; }

        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Tablilla> Tablilla { get; set; }


    }
}
