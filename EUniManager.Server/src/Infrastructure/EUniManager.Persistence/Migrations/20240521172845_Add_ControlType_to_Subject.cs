using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_ControlType_to_Subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ControlType",
                table: "Subjects",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ControlType",
                table: "Subjects");
        }
    }
}
