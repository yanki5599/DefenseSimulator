﻿// <auto-generated />
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
    [Migration("20240807060858_initial")]
    partial class initial
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

                    b.HasIndex("DefenseWeaponId");

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
#pragma warning restore 612, 618
        }
    }
}
