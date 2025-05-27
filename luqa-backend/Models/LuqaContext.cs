using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace luqa_backend.Models;

public partial class LuqaContext : DbContext
{
    public LuqaContext()
    {
    }

    public LuqaContext(DbContextOptions<LuqaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Answers> Answers { get; set; }

    public virtual DbSet<Course> Course { get; set; }

    public virtual DbSet<Coursepaths> Coursepaths { get; set; }

    public virtual DbSet<Efmigrationshistory> Efmigrationshistory { get; set; }

    public virtual DbSet<Lessons> Lessons { get; set; }

    public virtual DbSet<Questions> Questions { get; set; }

    public virtual DbSet<Route> Route { get; set; }

    public virtual DbSet<Userprogress> Userprogress { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Tecsup2025;database=luqa_db", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.42-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Answers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("answers");

            entity.HasIndex(e => e.QuestionId, "QuestionId");

            entity.Property(e => e.Text).HasColumnType("text");

            entity.HasOne(d => d.Question).WithMany(p => p.Answers)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("answers_ibfk_1");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("course");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(100);
        });

        modelBuilder.Entity<Coursepaths>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("coursepaths");

            entity.HasIndex(e => e.CourseId, "CourseId");

            entity.HasIndex(e => e.RouteId, "RouteId");

            entity.HasOne(d => d.Course).WithMany(p => p.Coursepaths)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("coursepaths_ibfk_2");

            entity.HasOne(d => d.Route).WithMany(p => p.Coursepaths)
                .HasForeignKey(d => d.RouteId)
                .HasConstraintName("coursepaths_ibfk_1");
        });

        modelBuilder.Entity<Efmigrationshistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId).HasName("PRIMARY");

            entity.ToTable("__efmigrationshistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<Lessons>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lessons");

            entity.HasIndex(e => e.CourseId, "CourseId");

            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.TypeLesson).HasColumnType("enum('Standar','Premium')");
            entity.Property(e => e.VideoUrl).HasMaxLength(500);

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("lessons_ibfk_1");
        });

        modelBuilder.Entity<Questions>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("questions");

            entity.HasIndex(e => e.LessonId, "LessonId");

            entity.Property(e => e.Text).HasColumnType("text");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Questions)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("questions_ibfk_1");
        });

        modelBuilder.Entity<Route>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("route");

            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Userprogress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("userprogress");

            entity.HasIndex(e => e.LessonId, "LessonId");

            entity.HasIndex(e => e.UserId, "UserId");

            entity.Property(e => e.ProgressDate).HasMaxLength(6);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Lesson).WithMany(p => p.Userprogress)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("userprogress_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.Userprogress)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userprogress_ibfk_1");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.UsuarioID).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property<object>(e => e.UsuarioID).HasColumnName("UsuarioID");
            entity.Property<object>(e => e.Contraseña).HasMaxLength(100);
            entity.Property<object>(e => e.CorreoElectronico).HasMaxLength(255);
            entity.Property<object>(e => e.FechaRegistro).HasMaxLength(6);
            entity.Property(e => e.NombreCompleto).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
