using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningTogether.Migrations
{
    public partial class ChaptersModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Unit_UnitId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Material",
                newName: "ChapterId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_UnitId",
                table: "Material",
                newName: "IX_Material_ChapterId");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Material",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaterialName",
                table: "Material",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Chapter",
                columns: table => new
                {
                    ChapterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ChapterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapter", x => x.ChapterId);
                    table.ForeignKey(
                        name: "FK_Chapter_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chapter_CourseId",
                table: "Chapter",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Chapter_ChapterId",
                table: "Material",
                column: "ChapterId",
                principalTable: "Chapter",
                principalColumn: "ChapterId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Material_Chapter_ChapterId",
                table: "Material");

            migrationBuilder.DropTable(
                name: "Chapter");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Material");

            migrationBuilder.DropColumn(
                name: "MaterialName",
                table: "Material");

            migrationBuilder.RenameColumn(
                name: "ChapterId",
                table: "Material",
                newName: "UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Material_ChapterId",
                table: "Material",
                newName: "IX_Material_UnitId");

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    UnitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.UnitId);
                    table.ForeignKey(
                        name: "FK_Unit_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Unit_CourseId",
                table: "Unit",
                column: "CourseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Material_Unit_UnitId",
                table: "Material",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "UnitId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
