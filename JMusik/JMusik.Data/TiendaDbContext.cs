using System;
using System.Collections.Generic;
using JMusik.Data.Configuracion;
using JMusik.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JMusik.Data
{
    public partial class TiendaDbContext : DbContext
    {
        public TiendaDbContext()
        {
        }

        public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetalleOrden> DetallesOrden { get; set; } = null!;
        public virtual DbSet<Orden> Ordenes { get; set; } = null!;
        public virtual DbSet<Perfil> Perfiles { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DetalleOrdenConfiguracion());
            modelBuilder.ApplyConfiguration(new OrdenConfiguracion());
            modelBuilder.ApplyConfiguration(new PerfilConfiguracion());
            modelBuilder.ApplyConfiguration(new ProductoConfiguracion());
            modelBuilder.ApplyConfiguration(new UsuarioConfiguracion());
        }
    }
}
