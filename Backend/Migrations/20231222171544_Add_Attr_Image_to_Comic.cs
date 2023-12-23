using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Webcomic.Migrations
{
    /// <inheritdoc />
    public partial class Add_Attr_Image_to_Comic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Comic",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comic");
        }
    }
}
