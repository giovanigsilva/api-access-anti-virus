using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ValidaArquivo.Web.Migrations
{
    public partial class campos_virustotal_response_v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnaliseArquivos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArquivoUploadId = table.Column<int>(type: "int", nullable: false),
                    IdArquivoUpload = table.Column<int>(type: "int", nullable: true),
                    Antivirus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detected = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Update = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnaliseArquivos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnaliseArquivos_ArquivoUploads_ArquivoUploadId",
                        column: x => x.ArquivoUploadId,
                        principalTable: "ArquivoUploads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnaliseArquivos_ArquivoUploadId",
                table: "AnaliseArquivos",
                column: "ArquivoUploadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnaliseArquivos");
        }
    }
}
