﻿// <auto-generated />
using System;
using IMedXUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IMedXUtilities.Migrations
{
    [DbContext(typeof(IMedXDBContext))]
    partial class IMedXDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IMedXModels.Input.IMedXPatientData", b =>
                {
                    b.Property<double>("AMT")
                        .HasColumnType("float");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DOC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ICD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NDC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PA")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("IMedXPatientData","dbo");
                });

            modelBuilder.Entity("IMedXModels.Input.InputPatientICD", b =>
                {
                    b.Property<string>("DOC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ICD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PA")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("InputPatientICD","dbo");
                });

            modelBuilder.Entity("IMedXModels.Input.InputPatientNDC", b =>
                {
                    b.Property<string>("AMT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NDC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PA")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("InputPatientNDC","dbo");
                });
#pragma warning restore 612, 618
        }
    }
}