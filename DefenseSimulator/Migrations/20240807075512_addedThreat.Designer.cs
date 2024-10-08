﻿// <auto-generated />
using System;
using DefenseSimulator.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DefenseSimulator.Migrations
{
    [DbContext(typeof(DefenseSimulatorContext))]
    [Migration("20240807075512_addedThreat")]
    partial class addedThreat
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AttackWeaponDefenseWeapon", b =>
                {
                    b.Property<int>("VulnerableWeaponsId")
                        .HasColumnType("int");

                    b.Property<int>("defenseWeaponsId")
                        .HasColumnType("int");

                    b.HasKey("VulnerableWeaponsId", "defenseWeaponsId");

                    b.HasIndex("defenseWeaponsId");

                    b.ToTable("AttackWeaponDefenseWeapon");
                });

            modelBuilder.Entity("DefenseSimulator.Models.Arsenal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("DefenseWeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DefenseWeaponId")
                        .IsUnique();

                    b.ToTable("Arsenal");
                });

            modelBuilder.Entity("DefenseSimulator.Models.AttackWeapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EffectiveDistance")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AttackWeapon");
                });

            modelBuilder.Entity("DefenseSimulator.Models.DefenseWeapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EffectiveDistance")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DefenseWeapon");
                });

            modelBuilder.Entity("DefenseSimulator.Models.OriginThreat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Distanse")
                        .HasColumnType("int");

                    b.Property<string>("Origin")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Origin")
                        .IsUnique();

                    b.ToTable("OriginThreat");
                });

            modelBuilder.Entity("DefenseSimulator.Models.Threat", b =>
                {
                    b.Property<int>("ThreatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ThreatId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("AttackWeaponId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LaunchTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("OriginThreatId")
                        .HasColumnType("int");

                    b.HasKey("ThreatId");

                    b.HasIndex("AttackWeaponId");

                    b.HasIndex("OriginThreatId");

                    b.ToTable("Threat");
                });

            modelBuilder.Entity("AttackWeaponDefenseWeapon", b =>
                {
                    b.HasOne("DefenseSimulator.Models.AttackWeapon", null)
                        .WithMany()
                        .HasForeignKey("VulnerableWeaponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DefenseSimulator.Models.DefenseWeapon", null)
                        .WithMany()
                        .HasForeignKey("defenseWeaponsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DefenseSimulator.Models.Arsenal", b =>
                {
                    b.HasOne("DefenseSimulator.Models.DefenseWeapon", "DefenseWeapon")
                        .WithMany()
                        .HasForeignKey("DefenseWeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DefenseWeapon");
                });

            modelBuilder.Entity("DefenseSimulator.Models.Threat", b =>
                {
                    b.HasOne("DefenseSimulator.Models.AttackWeapon", "AttackWeapon")
                        .WithMany()
                        .HasForeignKey("AttackWeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DefenseSimulator.Models.OriginThreat", "OriginThreat")
                        .WithMany()
                        .HasForeignKey("OriginThreatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AttackWeapon");

                    b.Navigation("OriginThreat");
                });
#pragma warning restore 612, 618
        }
    }
}
