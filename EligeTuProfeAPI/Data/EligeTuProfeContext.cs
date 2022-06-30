using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EligeTuProfeAPI.Models;

namespace EligeTuProfeAPI.Data
{
    public partial class EligeTuProfeContext : DbContext
    {
        public EligeTuProfeContext()
        {
        }

        public EligeTuProfeContext(DbContextOptions<EligeTuProfeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Asignatura> Asignaturas { get; set; } = null!;
        public virtual DbSet<Carrera> Carreras { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<InscripcionAlumno> InscripcionAlumnos { get; set; } = null!;
        public virtual DbSet<InscripcionProfesor> InscripcionProfesors { get; set; } = null!;
        public virtual DbSet<Profesor> Profesors { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Asignatura>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("Asignatura");

                entity.HasIndex(e => e.CodigoCarrera, "CodigoCarrera");

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(30);

                entity.HasOne(d => d.CodigoCarreraNavigation)
                    .WithMany(p => p.Asignaturas)
                    .HasForeignKey(d => d.CodigoCarrera)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Asignatura_ibfk_1");
            });

            modelBuilder.Entity<Carrera>(entity =>
            {
                entity.HasKey(e => e.Codigo)
                    .HasName("PRIMARY");

                entity.ToTable("Carrera");

                entity.Property(e => e.Codigo).ValueGeneratedNever();

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => new { e.Id, e.RutAlumno, e.RutProfesor, e.CodigoAsignatura })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("Comentario");

                entity.HasIndex(e => e.CodigoAsignatura, "CodigoAsignatura");

                entity.HasIndex(e => e.RutAlumno, "RutAlumno");

                entity.HasIndex(e => e.RutProfesor, "RutProfesor");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FechaCreacion).HasColumnType("timestamp");

                entity.Property(e => e.Periodo).HasColumnName("periodo");

                entity.Property(e => e.Texto).HasMaxLength(255);

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.CodigoAsignaturaNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.CodigoAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comentario_ibfk_3");

                entity.HasOne(d => d.RutAlumnoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.RutAlumno)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comentario_ibfk_1");

                entity.HasOne(d => d.RutProfesorNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.RutProfesor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comentario_ibfk_2");
            });

            modelBuilder.Entity<InscripcionAlumno>(entity =>
            {
                entity.HasKey(e => new { e.Rut, e.CodigoAsignatura, e.Year, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("InscripcionAlumno");

                entity.HasIndex(e => e.CodigoAsignatura, "CodigoAsignatura");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.Periodo).HasColumnName("periodo");

                entity.HasOne(d => d.CodigoAsignaturaNavigation)
                    .WithMany(p => p.InscripcionAlumnos)
                    .HasForeignKey(d => d.CodigoAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InscripcionAlumno_ibfk_2");

                entity.HasOne(d => d.RutNavigation)
                    .WithMany(p => p.InscripcionAlumnos)
                    .HasForeignKey(d => d.Rut)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InscripcionAlumno_ibfk_1");
            });

            modelBuilder.Entity<InscripcionProfesor>(entity =>
            {
                entity.HasKey(e => new { e.Rut, e.CodigoAsignatura, e.Year, e.Periodo })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

                entity.ToTable("InscripcionProfesor");

                entity.HasIndex(e => e.CodigoAsignatura, "CodigoAsignatura");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.Property(e => e.Periodo).HasColumnName("periodo");

                entity.HasOne(d => d.CodigoAsignaturaNavigation)
                    .WithMany(p => p.InscripcionProfesors)
                    .HasForeignKey(d => d.CodigoAsignatura)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InscripcionProfesor_ibfk_1");

                entity.HasOne(d => d.RutNavigation)
                    .WithMany(p => p.InscripcionProfesors)
                    .HasForeignKey(d => d.Rut)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("InscripcionProfesor_ibfk_2");
            });

            modelBuilder.Entity<Profesor>(entity =>
            {
                entity.HasKey(e => e.Rut)
                    .HasName("PRIMARY");

                entity.ToTable("Profesor");

                entity.Property(e => e.Rut).ValueGeneratedNever();

                entity.Property(e => e.Correo).HasMaxLength(255);

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Rut)
                    .HasName("PRIMARY");

                entity.ToTable("Usuario");

                entity.HasIndex(e => e.CodigoCarrera, "CodigoCarrera");

                entity.Property(e => e.Rut).ValueGeneratedNever();

                entity.Property(e => e.Correo).HasMaxLength(255);

                entity.Property(e => e.Id).HasMaxLength(30);

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.Property(e => e.UserPassword).HasMaxLength(255);

                entity.HasOne(d => d.CodigoCarreraNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.CodigoCarrera)
                    .HasConstraintName("Usuario_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
