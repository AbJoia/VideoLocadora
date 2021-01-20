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
    [Migration("20210119140255_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("src.Api.Domain.Entities.AluguelEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataDevolução")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("aluguel");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.FilmeEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FuncionarioId")
                        .HasColumnType("char(36)");

                    b.Property<int>("QtdLocacao")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("FuncionarioId");

                    b.ToTable("filme");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.ItemAluguelEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AluguelId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FilmeId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("AluguelId");

                    b.HasIndex("FilmeId");

                    b.ToTable("item_aluguel");
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
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.Property<int>("TipoUsuario")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("UsuarioEntity");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.FuncionarioEntity", b =>
                {
                    b.HasBaseType("src.Api.Domain.Entities.UsuarioEntity");

                    b.Property<long>("Matricula")
                        .HasColumnType("bigint");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(100) CHARACTER SET utf8mb4")
                        .HasMaxLength(100);

                    b.HasIndex("Matricula")
                        .IsUnique();

                    b.HasDiscriminator().HasValue("FuncionarioEntity");
                });

            modelBuilder.Entity("src.Api.Domain.Entities.AluguelEntity", b =>
                {
                    b.HasOne("src.Api.Domain.Entities.UsuarioEntity", "Usuario")
                        .WithMany("Alugueis")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("src.Api.Domain.Entities.FilmeEntity", b =>
                {
                    b.HasOne("src.Api.Domain.Entities.FuncionarioEntity", "Funcionario")
                        .WithMany("FilmesCadastrados")
                        .HasForeignKey("FuncionarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("src.Api.Domain.Entities.ItemAluguelEntity", b =>
                {
                    b.HasOne("src.Api.Domain.Entities.AluguelEntity", "Aluguel")
                        .WithMany("ItensAluguel")
                        .HasForeignKey("AluguelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("src.Api.Domain.Entities.FilmeEntity", "Filme")
                        .WithMany("ItensAluguel")
                        .HasForeignKey("FilmeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}