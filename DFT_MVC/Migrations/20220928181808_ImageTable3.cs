using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class ImageTable3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImagData_Kategorie_KategorieId",
                table: "ImagData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImagData",
                table: "ImagData");

            migrationBuilder.DropIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData");

            migrationBuilder.RenameTable(
                name: "ImagData",
                newName: "ImageData");

            migrationBuilder.AlterColumn<int>(
                name: "KategorieId",
                table: "ImageData",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageData",
                table: "ImageData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImageData_KategorieId",
                table: "ImageData",
                column: "KategorieId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageData_Kategorie_KategorieId",
                table: "ImageData",
                column: "KategorieId",
                principalTable: "Kategorie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageData_Kategorie_KategorieId",
                table: "ImageData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageData",
                table: "ImageData");

            migrationBuilder.DropIndex(
                name: "IX_ImageData_KategorieId",
                table: "ImageData");

            migrationBuilder.RenameTable(
                name: "ImageData",
                newName: "ImagData");

            migrationBuilder.AlterColumn<int>(
                name: "KategorieId",
                table: "ImagData",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImagData",
                table: "ImagData",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData",
                column: "KategorieId",
                unique: true,
                filter: "[KategorieId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_ImagData_Kategorie_KategorieId",
                table: "ImagData",
                column: "KategorieId",
                principalTable: "Kategorie",
                principalColumn: "Id");
        }
    }
}
