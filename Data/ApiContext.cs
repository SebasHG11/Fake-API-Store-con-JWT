using System.Security.Cryptography;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Data{
    public class ApiContext : DbContext{
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

        public DbSet<Producto> Productos{get; set;}
        public DbSet<Categoria> Categorias{get; set;}
        public DbSet<Usuario> Usuarios{get; set;}
        public DbSet<Orden> Ordens{get; set;}
        public DbSet<OrdenProducto> OrdenProductos {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Producto>(entity =>{
                entity.ToTable("Producto");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(80);
                entity.Property(p => p.Precio).IsRequired();
                entity.HasOne(p => p.Categoria)
                    .WithMany(c => c.Productos)
                    .HasForeignKey(p => p.IdCategoria)
                    .OnDelete(DeleteBehavior.Cascade);
                entity.Property(p => p.Imagen).IsRequired();
            });

            modelBuilder.Entity<Categoria>(entity =>{
                entity.ToTable("Categoria");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired();
                entity.Property(c => c.Imagen).IsRequired();
            });

            modelBuilder.Entity<Usuario>(entity =>{
                entity.ToTable("Usuario");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Nombre).IsRequired();
                entity.Property(u => u.Contraseña).IsRequired();
                entity.Property(u => u.Rol).IsRequired();
            });

            modelBuilder.Entity<Orden>(entity=>{
                entity.ToTable("Orden");
                entity.HasKey(o => o.Id);
                entity.Property(o => o.Fecha).IsRequired();
                // foreign key a usuario
                entity.HasOne(o => o.Usuario)
                .WithMany(u => u.Ordenes)
                .HasForeignKey(o => o.UsuarioId);
            });

            modelBuilder.Entity<OrdenProducto>(entity=>{
                entity.ToTable("OrdenProducto");
                entity.HasKey(op => new {op.OrdenId, op.ProductoId});
                // Foreign Key a orden
                entity.HasOne(op => op.Orden)
                .WithMany(o => o.OrdenProductos)
                .HasForeignKey(op => op.OrdenId);
                // Foreign Key a productos
                entity.HasOne(op => op.Producto)
                .WithMany(p => p.OrdenProductos)
                .HasForeignKey(op => op.ProductoId);
            });
        }
    }
}