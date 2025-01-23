﻿// <auto-generated />
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EmployeeManagement.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250123140924_nullableDepartment")]
    partial class nullableDepartment
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeManagement.Models.Domain.Department", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Department");

                    b.HasData(
                        new
                        {
                            id = 1,
                            name = "Finance"
                        },
                        new
                        {
                            id = 2,
                            name = "IT"
                        },
                        new
                        {
                            id = 3,
                            name = "HR"
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.Domain.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DeptId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeptId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Department = "HR",
                            DeptId = 2,
                            Email = "ahmad",
                            Name = "John",
                            address = "1234 Elm St",
                            salary = 10000
                        },
                        new
                        {
                            Id = 2,
                            Department = "HR",
                            DeptId = 2,
                            Email = "ahmad",
                            Name = "John",
                            address = "1234 Elm St",
                            salary = 10000
                        },
                        new
                        {
                            Id = 3,
                            Department = "HR",
                            DeptId = 3,
                            Email = "ahmad",
                            Name = "John",
                            address = "1234 Elm St",
                            salary = 10000
                        });
                });

            modelBuilder.Entity("EmployeeManagement.Models.Domain.Employee", b =>
                {
                    b.HasOne("EmployeeManagement.Models.Domain.Department", "dept")
                        .WithMany()
                        .HasForeignKey("DeptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("dept");
                });
#pragma warning restore 612, 618
        }
    }
}
