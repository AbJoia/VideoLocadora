﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using src.Api.Data.Context;

namespace Api.Data.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20210107233315_FirstMigrations")]
    partial class FirstMigrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("src.Api.Domain.Entities.FilmeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("QtdLocacao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("cadastradorId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("locatarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("cadastradorId");

                    b.HasIndex("locatarioId");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.UsuarioEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Email")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("tipoUsuario")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UsuarioEntity");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.FuncionarioEntity", b =>
                {
                    b.HasBaseType("src.Api.Domain.Entities.UsuarioEntity");

                    b.Property<long>("matricula")
                        .HasColumnType("bigint");

                    b.Property<string>("senha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasDiscriminator().HasValue("FuncionarioEntity");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.FilmeEntity", b =>
                {
                    b.HasOne("src.Api.Domain.Entities.FuncionarioEntity", "cadastrador")
                        .WithMany("FilmesCadastrados")
                        .HasForeignKey("cadastradorId");

                    b.HasOne("src.Api.Domain.Entities.UsuarioEntity", "locatario")
                        .WithMany("FilmesAlugados")
                        .HasForeignKey("locatarioId");
                });
#pragma warning restore 612, 618
        }
    }
}
