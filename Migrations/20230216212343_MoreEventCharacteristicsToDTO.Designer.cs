﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SCEAPI.Data;

#nullable disable

namespace SCEventsAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230216212343_MoreEventCharacteristicsToDTO")]
    partial class MoreEventCharacteristicsToDTO
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SCEAPI.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("EndDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("EndTime")
                        .HasColumnType("time");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<TimeOnly?>("StartTime")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.ToTable("Events");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Description 1",
                            EndDate = new DateOnly(2023, 2, 6),
                            Name = "Red Festival",
                            StartDate = new DateOnly(2023, 1, 20)
                        },
                        new
                        {
                            Id = 2,
                            Description = "Description 2",
                            EndDate = new DateOnly(2023, 2, 15),
                            Name = "Coramor",
                            StartDate = new DateOnly(2023, 2, 11)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
