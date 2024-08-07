﻿// <auto-generated />
using System;
using ClimateSmartAgriculture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClimateSmartAgriculture.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240704001350_AddIrrigationSchedule")]
    partial class AddIrrigationSchedule
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClimateSmartAgriculture.Models.Crop", b =>
                {
                    b.Property<int>("CropId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CropId"));

                    b.Property<string>("CropType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<DateTime>("HarvestDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PlantingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("CropId");

                    b.HasIndex("FarmId");

                    b.ToTable("Crops");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.Farm", b =>
                {
                    b.Property<int>("FarmId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FarmId"));

                    b.Property<string>("ClimateZone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FarmId");

                    b.ToTable("Farms");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.SoilMoisture", b =>
                {
                    b.Property<int>("MoistureId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MoistureId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<double>("Level")
                        .HasColumnType("float");

                    b.HasKey("MoistureId");

                    b.HasIndex("FarmId");

                    b.ToTable("SoilMoisture");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ClimateSmartAgricultureSystem.Models.IrrigationSchedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("FarmId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("WaterAmount")
                        .HasColumnType("float");

                    b.HasKey("ScheduleId");

                    b.HasIndex("FarmId");

                    b.ToTable("IrrigationSchedules");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.Crop", b =>
                {
                    b.HasOne("ClimateSmartAgriculture.Models.Farm", "Farm")
                        .WithMany()
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.SoilMoisture", b =>
                {
                    b.HasOne("ClimateSmartAgriculture.Models.Farm", "Farm")
                        .WithMany("SoilMoistureReadings")
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("ClimateSmartAgricultureSystem.Models.IrrigationSchedule", b =>
                {
                    b.HasOne("ClimateSmartAgriculture.Models.Farm", "Farm")
                        .WithMany()
                        .HasForeignKey("FarmId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Farm");
                });

            modelBuilder.Entity("ClimateSmartAgriculture.Models.Farm", b =>
                {
                    b.Navigation("SoilMoistureReadings");
                });
#pragma warning restore 612, 618
        }
    }
}
