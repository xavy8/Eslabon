using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WSAPIEslabon.Datos.Mapping.UsuariosEslabon;
using WSAPIEslabon.Entidades.UsuariosEslabon;

namespace WSAPIEslabon.Datos
{
    public class DbContextEslabon : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }

        public DbContextEslabon(DbContextOptions<DbContextEslabon> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UsuariosMap());
        }
    }
}
