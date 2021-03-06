﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerApp.Models;

namespace ServerApp.Migrations
{
    [DbContext(typeof(StoreContext))]
    [Migration("20201228194103_UniqueString2")]
    partial class UniqueString2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServerApp.Models.BoolLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("GroupValuesId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PropertyId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Value")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GroupValuesId");

                    b.HasIndex("PropertyId");

                    b.ToTable("BoolLine");
                });

            modelBuilder.Entity("ServerApp.Models.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NikName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ServerApp.Models.Description", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Description");
                });

            modelBuilder.Entity("ServerApp.Models.DoubleLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("GroupValuesId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PropertyId")
                        .HasColumnType("bigint");

                    b.Property<double>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("GroupValuesId");

                    b.HasIndex("PropertyId");

                    b.ToTable("DoubleLine");
                });

            modelBuilder.Entity("ServerApp.Models.GroupProperty", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("GroupProperty");
                });

            modelBuilder.Entity("ServerApp.Models.GroupValues", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("GroupPropertyId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("GroupPropertyId");

                    b.HasIndex("ProductId");

                    b.ToTable("GroupValues");
                });

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CategoryId")
                        .HasColumnType("bigint");

                    b.Property<bool?>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ServerApp.Models.Property", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("GroupPropertyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GroupPropertyId");

                    b.ToTable("Property");
                });

            modelBuilder.Entity("ServerApp.Models.StrLine", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("GroupValuesId")
                        .HasColumnType("bigint");

                    b.Property<long?>("PropertyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GroupValuesId");

                    b.HasIndex("PropertyId");

                    b.ToTable("StrLine");
                });

            modelBuilder.Entity("ServerApp.Models.UniqueString", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("PropertyId")
                        .HasColumnType("bigint");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UniqueStrings");
                });

            modelBuilder.Entity("ServerApp.Models.BoolLine", b =>
                {
                    b.HasOne("ServerApp.Models.GroupValues", null)
                        .WithMany("BoolProps")
                        .HasForeignKey("GroupValuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerApp.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId");
                });

            modelBuilder.Entity("ServerApp.Models.Description", b =>
                {
                    b.HasOne("ServerApp.Models.Product", null)
                        .WithOne("Description")
                        .HasForeignKey("ServerApp.Models.Description", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.DoubleLine", b =>
                {
                    b.HasOne("ServerApp.Models.GroupValues", null)
                        .WithMany("DoubleProps")
                        .HasForeignKey("GroupValuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerApp.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId");
                });

            modelBuilder.Entity("ServerApp.Models.GroupProperty", b =>
                {
                    b.HasOne("ServerApp.Models.Category", null)
                        .WithMany("GroupProperties")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.GroupValues", b =>
                {
                    b.HasOne("ServerApp.Models.GroupProperty", "GroupProperty")
                        .WithMany()
                        .HasForeignKey("GroupPropertyId");

                    b.HasOne("ServerApp.Models.Product", null)
                        .WithMany("GroupsValues")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.Product", b =>
                {
                    b.HasOne("ServerApp.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("ServerApp.Models.Property", b =>
                {
                    b.HasOne("ServerApp.Models.GroupProperty", null)
                        .WithMany("Properties")
                        .HasForeignKey("GroupPropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServerApp.Models.StrLine", b =>
                {
                    b.HasOne("ServerApp.Models.GroupValues", null)
                        .WithMany("StrProps")
                        .HasForeignKey("GroupValuesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServerApp.Models.Property", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId");
                });
#pragma warning restore 612, 618
        }
    }
}
