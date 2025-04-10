﻿// <auto-generated />
using System;
using GerenciamentodeEventos.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GerenciamentodeEventos.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoriaEvento", b =>
                {
                    b.Property<int>("CategoriasId")
                        .HasColumnType("integer");

                    b.Property<int>("EventosId")
                        .HasColumnType("integer");

                    b.HasKey("CategoriasId", "EventosId");

                    b.HasIndex("EventosId");

                    b.ToTable("CategoriaEvento");
                });

            modelBuilder.Entity("EventoOrganizador", b =>
                {
                    b.Property<int>("EventosId")
                        .HasColumnType("integer");

                    b.Property<int>("OrganizadoresId")
                        .HasColumnType("integer");

                    b.HasKey("EventosId", "OrganizadoresId");

                    b.HasIndex("OrganizadoresId");

                    b.ToTable("EventoOrganizador");
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Evento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Local")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("QuantidadeMaximaParticipantes")
                        .HasColumnType("integer");

                    b.Property<int>("QuantidadeParticipantes")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Evento");
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Organizador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Ativo")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.ToTable("Organizador");
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Participante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("EventoId")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.HasKey("Id");

                    b.HasIndex("EventoId");

                    b.ToTable("Participante");
                });

            modelBuilder.Entity("CategoriaEvento", b =>
                {
                    b.HasOne("GerenciamentodeEventos.Model.Categoria", null)
                        .WithMany()
                        .HasForeignKey("CategoriasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciamentodeEventos.Model.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EventoOrganizador", b =>
                {
                    b.HasOne("GerenciamentodeEventos.Model.Evento", null)
                        .WithMany()
                        .HasForeignKey("EventosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GerenciamentodeEventos.Model.Organizador", null)
                        .WithMany()
                        .HasForeignKey("OrganizadoresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Participante", b =>
                {
                    b.HasOne("GerenciamentodeEventos.Model.Evento", "Evento")
                        .WithMany("Participantes")
                        .HasForeignKey("EventoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("GerenciamentodeEventos.Model.Evento", b =>
                {
                    b.Navigation("Participantes");
                });
#pragma warning restore 612, 618
        }
    }
}
