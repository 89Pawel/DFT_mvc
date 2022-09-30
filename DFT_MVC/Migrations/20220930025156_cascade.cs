using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageData_Kategoria_KategoriaId",
                table: "ImageData");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageData_Kategoria_KategoriaId",
                table: "ImageData",
                column: "KategoriaId",
                principalTable: "Kategoria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageData_Kategoria_KategoriaId",
                table: "ImageData");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageData_Kategoria_KategoriaId",
                table: "ImageData",
                column: "KategoriaId",
                principalTable: "Kategoria",
                principalColumn: "Id");
        }
    }
}
