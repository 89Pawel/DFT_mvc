using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DFT_MVC.Migrations
{
    public partial class ImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageModel");

            migrationBuilder.CreateTable(
                name: "ImagData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    FullscreenContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailBigContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ThumbnailSmallContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    KategorieId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImagData_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImagData_KategorieId",
                table: "ImagData",
                column: "KategorieId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImagData");

            migrationBuilder.CreateTable(
                name: "ImageModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KategorieId = table.Column<int>(type: "int", nullable: true),
                    MediumSizeContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OriginalContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OriginalType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailContent = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageModel_Kategorie_KategorieId",
                        column: x => x.KategorieId,
                        principalTable: "Kategorie",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageModel_KategorieId",
                table: "ImageModel",
                column: "KategorieId");
        }
    }
}
