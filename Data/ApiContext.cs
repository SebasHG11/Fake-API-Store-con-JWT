using System.Security.Cryptography;
using Api1.Models;
using Microsoft.EntityFrameworkCore;

namespace Api1.Data{
    public class ApiContext : DbContext{
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) {}

        public DbSet<Producto> Productos{get; set;}
        public DbSet<Categoria> Categorias{get; set;}

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
        }
    }
}