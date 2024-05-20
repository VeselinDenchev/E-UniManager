using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Move_Protocol_from_Course_to_Subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Protocol",
                table: "Courses");

            migrationBuilder.AddColumn<string>(
                name: "Protocol",
                table: "Subjects",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Protocol",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "Protocol",
                table: "Courses",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: false,
                defaultValue: "");
        }
    }
}
