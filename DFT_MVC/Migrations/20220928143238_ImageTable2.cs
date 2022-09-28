using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class ImageTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData");

            migrationBuilder.CreateIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData",
                column: "KategorieId",
                unique: true,
                filter: "[KategorieId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData");

            migrationBuilder.CreateIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData",
                column: "KategorieId");
        }
    }
}
