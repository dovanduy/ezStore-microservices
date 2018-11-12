﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ezStore.Product.Infrastructure;

namespace ezStore.Product.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.Manufacture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Manufacture");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("FullDescription");

                    b.Property<string>("Name");

                    b.Property<bool>("Published");

                    b.Property<string>("SKU");

                    b.Property<string>("ShortDescription");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductAttribute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ProductAttribute");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductAttributeValue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("AttributeId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttributeValue");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ProductCategory");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductCategoryMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategoryMapping");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductManufactureMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ManufactureId");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ManufactureId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductManufactureMapping");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductTag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Name");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.ToTable("ProductTag");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductTapMapping", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime?>("CreatedDate");

                    b.Property<bool>("Deleted");

                    b.Property<Guid>("ProductId");

                    b.Property<Guid>("ProductTagId");

                    b.Property<string>("UpdatedBy");

                    b.Property<DateTime?>("UpdatedDate");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ProductTagId");

                    b.ToTable("ProductTapMapping");
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductAttributeValue", b =>
                {
                    b.HasOne("ezStore.Product.Infrastructure.Entities.ProductAttribute", "ProductAttribute")
                        .WithMany()
                        .HasForeignKey("AttributeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ezStore.Product.Infrastructure.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductCategoryMapping", b =>
                {
                    b.HasOne("ezStore.Product.Infrastructure.Entities.ProductCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ezStore.Product.Infrastructure.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductManufactureMapping", b =>
                {
                    b.HasOne("ezStore.Product.Infrastructure.Entities.Manufacture", "Manufacture")
                        .WithMany()
                        .HasForeignKey("ManufactureId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ezStore.Product.Infrastructure.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ezStore.Product.Infrastructure.Entities.ProductTapMapping", b =>
                {
                    b.HasOne("ezStore.Product.Infrastructure.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ezStore.Product.Infrastructure.Entities.ProductTag", "ProductTag")
                        .WithMany()
                        .HasForeignKey("ProductTagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
