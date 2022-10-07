using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageDatas_Categories_CategoryId",
                table: "ImageDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageDatas_Categories_CategoryId",
                table: "ImageDatas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageDatas_Categories_CategoryId",
                table: "ImageDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageDatas_Categories_CategoryId",
                table: "ImageDatas",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
