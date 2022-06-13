using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    public partial class campos_virustotal_response_v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErroRetornoVirusTotal",
                table: "ArquivoUploads",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErroRetornoVirusTotal",
                table: "ArquivoUploads");
        }
    }
}
