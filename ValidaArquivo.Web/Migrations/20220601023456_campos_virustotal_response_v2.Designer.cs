﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ValidaArquivo.Data;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    [DbContext(typeof(ValidaArquivoContext))]
    [Migration("20220601023456_campos_virustotal_response_v2")]
    partial class campos_virustotal_response_v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ValidaArquivo.Domain.Entities.ArquivoUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("Conteudo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DataEnvioVirusTotal")
                        .HasColumnType("datetime2");

                    b.Property<string>("ErroEnvioVirusTotal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeArquivo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VirusTotalPermalink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VirusTotalResource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VirusTotalResponseCode")
                        .HasColumnType("int");

                    b.Property<string>("VirusTotalSHA256")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VirusTotalScanId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VirusTotalVerboseMessage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArquivoUploads");
                });
#pragma warning restore 612, 618
        }
    }
}
