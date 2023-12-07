﻿// <auto-generated />
using System;
using Concurrency.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Concurrency.Migrations
{
    [DbContext(typeof(ConcurrencyContext))]
    [Migration("20231206101246_WithSeats")]
    partial class WithSeats
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Concurrency.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AgeLimit")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("Concurrency.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int?>("SeatId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("SeatId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Concurrency.Models.Seat", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("Id"));

                    b.Property<int?>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Taken")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Seat");
                });

            modelBuilder.Entity("Concurrency.Models.Order", b =>
                {
                    b.HasOne("Concurrency.Models.Movie", "Movie")
                        .WithMany("Orders")
                        .HasForeignKey("MovieId");

                    b.HasOne("Concurrency.Models.Seat", "Seat")
                        .WithMany()
                        .HasForeignKey("SeatId");

                    b.Navigation("Movie");

                    b.Navigation("Seat");
                });

            modelBuilder.Entity("Concurrency.Models.Seat", b =>
                {
                    b.HasOne("Concurrency.Models.Movie", null)
                        .WithMany("Seats")
                        .HasForeignKey("MovieId");
                });

            modelBuilder.Entity("Concurrency.Models.Movie", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}
