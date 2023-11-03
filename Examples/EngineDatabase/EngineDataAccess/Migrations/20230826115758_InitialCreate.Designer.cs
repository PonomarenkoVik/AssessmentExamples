﻿// <auto-generated />
using System;
using EngineDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EngineDataAccess.Migrations
{
    [DbContext(typeof(EngineDbContext))]
    [Migration("20230826115758_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EngineModels.EngineModels.Engine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EngineTypeId")
                        .HasColumnType("int");

                    b.Property<string>("FabricNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Engines");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EngineTypeId = 1,
                            FabricNumber = "F45GT76765",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            EngineTypeId = 2,
                            FabricNumber = "FFGHJH7765",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            EngineTypeId = 2,
                            FabricNumber = "FGJKLD6578",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("EngineModels.EngineModels.EngineType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DisplayType")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("EnginePower")
                        .HasColumnType("real");

                    b.Property<int>("EngineSpeed")
                        .HasColumnType("int");

                    b.Property<float?>("Height")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float?>("Weight")
                        .HasColumnType("real");

                    b.Property<float?>("Width")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("EngineTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayType = "АИР200м4, 37 kWt, 1500 1/s",
                            EnginePower = 37f,
                            EngineSpeed = 1500,
                            Type = "АИР200м4"
                        },
                        new
                        {
                            Id = 2,
                            DisplayType = "АИМ132м2, 11 kWt, 2900 1/s",
                            EnginePower = 11f,
                            EngineSpeed = 2900,
                            Type = "АИМ132м2"
                        },
                        new
                        {
                            Id = 3,
                            DisplayType = "АИИ160S6, 11 kWt, 1000 1/s",
                            EnginePower = 11f,
                            EngineSpeed = 1000,
                            Type = "АИИ160S6"
                        });
                });

            modelBuilder.Entity("EngineModels.OrderModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EngineId")
                        .HasColumnType("int");

                    b.Property<int>("RepairType")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EngineId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Common repairment",
                            EngineId = 1,
                            RepairType = 0,
                            State = 0,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Description = "Medium repairment",
                            EngineId = 2,
                            RepairType = 0,
                            State = 2,
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Overhaul repairment",
                            EngineId = 3,
                            RepairType = 2,
                            State = 3,
                            UserId = 1
                        });
                });

            modelBuilder.Entity("EngineModels.UserModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Surname");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .IsRequired()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Viktor",
                            LastName = "Ponomarenko"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Oleksiy",
                            LastName = "Babkin"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Serhiy",
                            LastName = "Lisovets"
                        });
                });

            modelBuilder.Entity("EngineModels.EngineModels.Engine", b =>
                {
                    b.HasOne("EngineModels.EngineModels.EngineType", "EngineType")
                        .WithMany()
                        .HasForeignKey("EngineTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EngineModels.UserModels.User", "User")
                        .WithMany("Engines")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EngineType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("EngineModels.OrderModels.Order", b =>
                {
                    b.HasOne("EngineModels.EngineModels.Engine", "Engine")
                        .WithMany("Orders")
                        .HasForeignKey("EngineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Engine");
                });

            modelBuilder.Entity("EngineModels.EngineModels.Engine", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("EngineModels.UserModels.User", b =>
                {
                    b.Navigation("Engines");
                });
#pragma warning restore 612, 618
        }
    }
}
