using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_web_api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAboutMeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email",
                table: "AboutMe");

            migrationBuilder.RenameColumn(
                name: "location",
                table: "AboutMe",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "fullName",
                table: "AboutMe",
                newName: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "AboutMe",
                newName: "location");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "AboutMe",
                newName: "fullName");

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "AboutMe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
