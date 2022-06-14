/*
 * Clase autogenerada de Entity para la conexión a la Base de Datos
 */
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AplicacionWeb.Models
{
    public partial class RelacionesContext : DbContext
    {
        public RelacionesContext()
        {
        }

        public RelacionesContext(DbContextOptions<RelacionesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Parentesco> Parentescos { get; set; } = null!;
        public virtual DbSet<Persona> Personas { get; set; } = null!;
        public virtual DbSet<Tipo> Tipos { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parentesco>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.Property(e => e.PrimerApellido).HasMaxLength(25);

                entity.Property(e => e.PrimerNombre).HasMaxLength(25);

                entity.Property(e => e.SegundoApellido).HasMaxLength(25);

                entity.Property(e => e.SegundoNombre).HasMaxLength(25);

                entity.HasOne(d => d.FamiliarNavigation)
                    .WithMany(p => p.InverseFamiliarNavigation)
                    .HasForeignKey(d => d.Familiar)
                    .HasConstraintName("FKFamiliar");

                entity.HasOne(d => d.RelacionNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.Relacion)
                    .HasConstraintName("FKRelacion");

                entity.HasOne(d => d.TipoINavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.TipoI)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTipo");
            });

            modelBuilder.Entity<Tipo>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Contraseña).HasMaxLength(30);

                entity.Property(e => e.Usuario1)
                    .HasMaxLength(20)
                    .HasColumnName("Usuario");

                entity.HasOne(d => d.PersonaNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.Persona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPersona");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
