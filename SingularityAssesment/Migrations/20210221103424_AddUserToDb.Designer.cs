﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SingularityAssesment.Data;

namespace SingularityAssesment.Migrations
{
    [DbContext(typeof(SingularityAssesmentContext))]
    [Migration("20210221103424_AddUserToDb")]
    partial class AddUserToDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12");

            modelBuilder.Entity("SingularityAssesment.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("SingularityAssesment.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            Description = "Product 101",
                            Name = "Product 101",
                            Price = 10.0
                        },
                        new
                        {
                            Id = 202,
                            Description = "Product 102",
                            Name = "Product 103",
                            Price = 20.0
                        },
                        new
                        {
                            Id = 303,
                            Description = "Product 103",
                            Name = "Product 102",
                            Price = 30.0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
