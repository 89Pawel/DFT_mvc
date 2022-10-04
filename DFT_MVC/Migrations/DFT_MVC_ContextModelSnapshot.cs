﻿// <auto-generated />
using System;
using DFT_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DFT_MVC.Migrations
{
    [DbContext(typeof(DFT_MVC_Context))]
    partial class DFT_MVC_ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DFT_MVC.Data.ImageData", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("FullscreenContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("OriginalContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OriginalType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ThumbnailBigContent")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("ThumbnailSmallContent")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId")
                        .IsUnique();

                    b.ToTable("ImageData");
                });

            modelBuilder.Entity("DFT_MVC.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("DFT_MVC.Data.ImageData", b =>
                {
                    b.HasOne("DFT_MVC.Models.Category", "Category")
                        .WithOne("ImageData")
                        .HasForeignKey("DFT_MVC.Data.ImageData", "CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DFT_MVC.Models.Category", b =>
                {
                    b.Navigation("ImageData");
                });
#pragma warning restore 612, 618
        }
    }
}
