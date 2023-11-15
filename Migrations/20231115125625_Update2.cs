using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webcomic.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Comic_ComicId",
                table: "Chapter");

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComicTag",
                columns: table => new
                {
                    ComicId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicTag", x => new { x.ComicId, x.TagId });
                    table.ForeignKey(
                        name: "FK_ComicTag_Comic_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComicTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComicTag_TagId",
                table: "ComicTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Comic_ComicId",
                table: "Chapter",
                column: "ComicId",
                principalTable: "Comic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Comic_ComicId",
                table: "Chapter");

            migrationBuilder.DropTable(
                name: "ComicTag");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Comic_ComicId",
                table: "Chapter",
                column: "ComicId",
                principalTable: "Comic",
                principalColumn: "Id");
        }
    }
}
