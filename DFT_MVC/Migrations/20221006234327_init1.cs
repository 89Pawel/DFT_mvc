using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageDatas_Subcategories_SubcategoryId",
                table: "ImageDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageDatas_Subcategories_SubcategoryId",
                table: "ImageDatas",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageDatas_Subcategories_SubcategoryId",
                table: "ImageDatas");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageDatas_Subcategories_SubcategoryId",
                table: "ImageDatas",
                column: "SubcategoryId",
                principalTable: "Subcategories",
                principalColumn: "Id");
        }
    }
}
