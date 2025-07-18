﻿// <auto-generated />
using System;
using EcomBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EcomBackend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250707110136_SeedSampleData")]
    partial class SeedSampleData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.17")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("EcomBackend.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("EcomBackend.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Computers",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 3,
                            Name = "Laptops",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 4,
                            Name = "Desktops",
                            ParentId = 2
                        },
                        new
                        {
                            Id = 5,
                            Name = "Smartphones",
                            ParentId = 1
                        },
                        new
                        {
                            Id = 6,
                            Name = "Home Appliances"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Kitchen",
                            ParentId = 6
                        },
                        new
                        {
                            Id = 8,
                            Name = "Refrigerators",
                            ParentId = 7
                        });
                });

            modelBuilder.Entity("EcomBackend.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AvailabilityQty")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AvailabilityQty = 10,
                            CategoryId = 3,
                            Name = "Gaming Laptop X1",
                            Price = 1299.99m
                        },
                        new
                        {
                            Id = 2,
                            AvailabilityQty = 5,
                            CategoryId = 3,
                            Name = "Ultrabook Z3",
                            Price = 999.99m
                        },
                        new
                        {
                            Id = 3,
                            AvailabilityQty = 7,
                            CategoryId = 4,
                            Name = "All-in-One Desktop A5",
                            Price = 749.50m
                        },
                        new
                        {
                            Id = 4,
                            AvailabilityQty = 15,
                            CategoryId = 5,
                            Name = "Smartphone S20",
                            Price = 799.00m
                        },
                        new
                        {
                            Id = 5,
                            AvailabilityQty = 3,
                            CategoryId = 8,
                            Name = "Mini Fridge M100",
                            Price = 199.99m
                        });
                });

            modelBuilder.Entity("EcomBackend.Models.CartItem", b =>
                {
                    b.HasOne("EcomBackend.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcomBackend.Models.Category", b =>
                {
                    b.HasOne("EcomBackend.Models.Category", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("EcomBackend.Models.Product", b =>
                {
                    b.HasOne("EcomBackend.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("EcomBackend.Models.Category", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
