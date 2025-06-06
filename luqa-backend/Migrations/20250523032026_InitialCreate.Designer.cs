﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using luqa_backend.Models;

#nullable disable

namespace luqa_backend.Migrations
{
    [DbContext(typeof(LuqaContext))]
    [Migration("20250523032026_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("luqa_backend.Models.Usuario", b =>
                {
                    b.Property<int>("UsuarioID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("CorreoElectronico")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("UsuarioID");

                    b.ToTable("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
