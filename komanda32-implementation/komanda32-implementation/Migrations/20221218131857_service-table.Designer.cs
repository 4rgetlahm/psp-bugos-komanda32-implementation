﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using komanda32_implementation.Database;

#nullable disable

namespace komanda32_implementation.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221218131857_service-table")]
    partial class servicetable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("komanda32_implementation.Models.Franchise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Franchises");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("DiscountType")
                        .HasColumnType("int");

                    b.Property<int>("FranciseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PriceBeforeTaxe")
                        .HasColumnType("decimal(65,30)");

                    b.Property<DateTime>("ReservationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("TaxeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.Property<bool>("isProduct")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("ProductServices");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Shift", b =>
                {
                    b.Property<int>("ShiftId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("WorkerId")
                        .HasColumnType("int");

                    b.HasKey("ShiftId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BankAccount")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Employment")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("FranchiseId")
                        .HasColumnType("int");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int?>("ReadAccess")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FranchiseId");

                    b.HasIndex("GroupId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Group", b =>
                {
                    b.HasOne("komanda32_implementation.Models.Franchise", "Franchise")
                        .WithMany("Groups")
                        .HasForeignKey("FranchiseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Franchise");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Worker", b =>
                {
                    b.HasOne("komanda32_implementation.Models.Franchise", "Franchise")
                        .WithMany()
                        .HasForeignKey("FranchiseId");

                    b.HasOne("komanda32_implementation.Models.Group", "Group")
                        .WithMany("Workers")
                        .HasForeignKey("GroupId");

                    b.Navigation("Franchise");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Franchise", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("komanda32_implementation.Models.Group", b =>
                {
                    b.Navigation("Workers");
                });
#pragma warning restore 612, 618
        }
    }
}
