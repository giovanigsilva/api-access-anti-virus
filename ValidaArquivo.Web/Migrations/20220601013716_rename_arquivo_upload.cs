using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    public partial class rename_arquivo_upload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "ArquivoUploads",
                newName: "DataAlteracao");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ArquivoUploads",
                newName: "DataCriacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "ArquivoUploads",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "ArquivoUploads",
                newName: "UpdatedAt");
        }
    }
}
