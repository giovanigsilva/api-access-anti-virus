using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    public partial class campos_virustotal_response_v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataResultadoVirusTotal",
                table: "ArquivoUploads",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataResultadoVirusTotal",
                table: "ArquivoUploads");
        }
    }
}
