using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrabajadoresPrueba.Models;

public partial class TrabajadoresPruebaContext : DbContext
{
    public TrabajadoresPruebaContext()
    {
    }

    public TrabajadoresPruebaContext(DbContextOptions<TrabajadoresPruebaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Trabajadores> Trabajadores { get; set; }
    public DbSet<ListadoTrabajadorDTO> ListadoTrabajadores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Trabajadores>(entity =>
        {
            modelBuilder.Entity<ListadoTrabajadorDTO>().HasNoKey();
            entity.HasKey(e => e.Id).HasName("PK__Trabajad__3214EC07BF850102");

            entity.Property(e => e.Nombres)
                    .HasMaxLength(100)
                    .IsUnicode(false);

            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(3)
                .IsUnicode(false);

            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.Property(e => e.Sexo)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date");

            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.Property(e => e.Foto)
                .HasMaxLength(250)
                .IsUnicode(false);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
