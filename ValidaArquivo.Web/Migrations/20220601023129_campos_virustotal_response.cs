using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    public partial class campos_virustotal_response : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEnvioVirusTotal",
                table: "ArquivoUploads",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ErroEnvioVirusTotal",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VirusTotalPermalink",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VirusTotalResource",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "VirusTotalResponseCode",
                table: "ArquivoUploads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "VirusTotalSHA256",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VirusTotalScanId",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VirusTotalVerboseMessage",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEnvioVirusTotal",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "ErroEnvioVirusTotal",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalPermalink",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalResource",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalResponseCode",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalSHA256",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalScanId",
                table: "ArquivoUploads");

            migrationBuilder.DropColumn(
                name: "VirusTotalVerboseMessage",
                table: "ArquivoUploads");
        }
    }
}
