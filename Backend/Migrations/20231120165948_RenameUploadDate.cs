using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webcomic.Migrations
{
    /// <inheritdoc />
    public partial class RenameUploadDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PublishDate",
                table: "Chapter",
                newName: "UploadDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadDate",
                table: "Chapter",
                newName: "PublishDate");
        }
    }
}
