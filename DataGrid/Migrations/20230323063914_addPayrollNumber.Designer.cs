﻿// <auto-generated />
using System;
using DataGrid.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataGrid.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230323063914_addPayrollNumber")]
    partial class addPayrollNumber
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataGrid.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("employee_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("address");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address2");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("EmailHome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email_home");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("forename");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("mobile");

                    b.Property<string>("PayrollNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("payroll_number");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("telephone");

                    b.Property<string>("Postcode")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("postcode");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_date");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("surname");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}
