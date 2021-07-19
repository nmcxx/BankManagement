﻿// <auto-generated />
using System;
using BankManagement.WebAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankManagement.WebAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.HasKey("CurrencyId");

                    b.ToTable("Currencies");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AccountBalance")
                        .HasColumnType("real");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrenciesCurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RolesRoleId")
                        .HasColumnType("int");

                    b.HasKey("CustomerId");

                    b.HasIndex("CurrenciesCurrencyId");

                    b.HasIndex("RolesRoleId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Deal", b =>
                {
                    b.Property<int>("DealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CurrenciesCurrencyId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerIdRevice")
                        .HasColumnType("int");

                    b.Property<int>("CustomerIdSend")
                        .HasColumnType("int");

                    b.Property<int?>("CustomersCustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Money")
                        .HasColumnType("real");

                    b.Property<int?>("ServicesServiceId")
                        .HasColumnType("int");

                    b.HasKey("DealId");

                    b.HasIndex("CurrenciesCurrencyId");

                    b.HasIndex("CustomersCustomerId");

                    b.HasIndex("ServicesServiceId");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.ExchangeRate", b =>
                {
                    b.Property<int>("ExchangeRateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CurrencyId")
                        .HasColumnType("int");

                    b.Property<string>("ExchangeRateName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ExchangeRateId");

                    b.HasIndex("CurrencyId");

                    b.ToTable("ExchangeRates");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Service", b =>
                {
                    b.Property<int>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ServiceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ServiceId");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Customer", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Currency", "Currencies")
                        .WithMany()
                        .HasForeignKey("CurrenciesCurrencyId");

                    b.HasOne("BankManagement.WebAPI.Entities.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("RolesRoleId");

                    b.Navigation("Currencies");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Deal", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Currency", "Currencies")
                        .WithMany()
                        .HasForeignKey("CurrenciesCurrencyId");

                    b.HasOne("BankManagement.WebAPI.Entities.Customer", "Customers")
                        .WithMany()
                        .HasForeignKey("CustomersCustomerId");

                    b.HasOne("BankManagement.WebAPI.Entities.Service", "Services")
                        .WithMany()
                        .HasForeignKey("ServicesServiceId");

                    b.Navigation("Currencies");

                    b.Navigation("Customers");

                    b.Navigation("Services");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.ExchangeRate", b =>
                {
                    b.HasOne("BankManagement.WebAPI.Entities.Currency", "Currency")
                        .WithMany("exchangeRates")
                        .HasForeignKey("CurrencyId");

                    b.Navigation("Currency");
                });

            modelBuilder.Entity("BankManagement.WebAPI.Entities.Currency", b =>
                {
                    b.Navigation("exchangeRates");
                });
#pragma warning restore 612, 618
        }
    }
}
