﻿// <auto-generated />
using System;
using MiCasa.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MiCasa.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220408230402_FirstSprint1")]
    partial class FirstSprint1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MiCasa.Models.Entity.Administrateur", b =>
                {
                    b.Property<int>("AdministrateurId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("AdministrateurId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AdministrateurId"));

                    b.Property<int>("CompteId")
                        .HasColumnType("integer");

                    b.Property<byte>("IsActive")
                        .HasColumnType("smallint")
                        .HasColumnName("IsActive");

                    b.HasKey("AdministrateurId");

                    b.HasIndex("CompteId");

                    b.ToTable("Administrateur");
                });

            modelBuilder.Entity("MiCasa.Models.Entity.Agence", b =>
                {
                    b.Property<int>("AgenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("AgenceId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AgenceId"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Adresse");

                    b.Property<int>("CompteId")
                        .HasColumnType("integer");

                    b.Property<float>("Latitude")
                        .HasColumnType("real")
                        .HasColumnName("Latitude");

                    b.Property<float>("Longitude")
                        .HasColumnType("real")
                        .HasColumnName("Longitude");

                    b.Property<int>("Signalement")
                        .HasColumnType("integer")
                        .HasColumnName("Signalement");

                    b.HasKey("AgenceId");

                    b.HasIndex("CompteId");

                    b.ToTable("Agence");
                });

            modelBuilder.Entity("MiCasa.Models.Entity.Compte", b =>
                {
                    b.Property<int>("CompteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("CompteId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CompteId"));

                    b.Property<string>("DateInscription")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DateInscription");

                    b.Property<byte>("IsBlocked")
                        .HasColumnType("smallint")
                        .HasColumnName("IsBlocked");

                    b.Property<byte>("IsConnected")
                        .HasColumnType("smallint")
                        .HasColumnName("IsConnected");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Mail");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Nom");

                    b.Property<string>("NumeroTelephone")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("character varying(13)")
                        .HasColumnName("NumeroTelephone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Username");

                    b.HasKey("CompteId");

                    b.ToTable("Comptes");
                });

            modelBuilder.Entity("MiCasa.Models.Entity.ContratAgence", b =>
                {
                    b.Property<int>("ContratId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ContratId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ContratId"));

                    b.Property<int>("AgenceId")
                        .HasColumnType("integer")
                        .HasColumnName("AgenceId");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DateCreation");

                    b.Property<byte>("IsActive")
                        .HasColumnType("smallint")
                        .HasColumnName("IsActive");

                    b.HasKey("ContratId");

                    b.ToTable("ContratAgence");
                });

            modelBuilder.Entity("MiCasa.Models.Entity.Administrateur", b =>
                {
                    b.HasOne("MiCasa.Models.Entity.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("CompteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });

            modelBuilder.Entity("MiCasa.Models.Entity.Agence", b =>
                {
                    b.HasOne("MiCasa.Models.Entity.Compte", "Compte")
                        .WithMany()
                        .HasForeignKey("CompteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Compte");
                });
#pragma warning restore 612, 618
        }
    }
}
