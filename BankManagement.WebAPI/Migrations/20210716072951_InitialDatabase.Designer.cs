﻿// <auto-generated />
using System;
using BankManagement.WebAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankManagement.WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210716072951_InitialDatabase")]
    partial class InitialDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Currency", b =>
                {
                    b.Property<int>("CurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrencyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DealId")
                        .HasColumnType("int");

                    b.Property<int?>("ExchangeRateId")
                        .HasColumnType("int");

                    b.HasKey("CurrencyId");

                    b.HasIndex("DealId");

                    b.HasIndex("ExchangeRateId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AccountBalancce")
                        .HasColumnType("real");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DealId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("DealId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Deal", b =>
                {
                    b.Property<int>("DealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerIdRevice")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.HasKey("DealId");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.ExchangeRate", b =>
                {
                    b.Property<int>("ExchangeRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ExchangeRateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExchangeRateId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DealId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.HasIndex("DealId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Currency", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Deal", null)
                        .WithMany("Currencies")
                        .HasForeignKey("DealId");

                    b.HasOne("BankManagement.WebAPI.Entities.ExchangeRate", null)
                        .WithMany("Currencies")
                        .HasForeignKey("ExchangeRateId");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Customer", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Deal", null)
                        .WithMany("Customers")
                        .HasForeignKey("DealId");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Role", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Customer", null)
                        .WithMany("Roles")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Service", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Deal", null)
                        .WithMany("Services")
                        .HasForeignKey("DealId");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Customer", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Deal", b =>
                {
                    b.Navigation("Currencies");

                    b.Navigation("Customers");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.ExchangeRate", b =>
                {
                    b.Navigation("Currencies");
                });
#pragma warning restore 612, 618
        }
    }
}
