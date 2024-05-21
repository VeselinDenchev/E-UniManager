using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_InsuranceNumber_and_Citizenship_in_Student_PersonalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsuraceNumber",
                table: "Students",
                newName: "InsuranceNumber");

            migrationBuilder.RenameColumn(
                name: "Citizienship",
                table: "Students",
                newName: "Citizenship");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InsuranceNumber",
                table: "Students",
                newName: "InsuraceNumber");

            migrationBuilder.RenameColumn(
                name: "Citizenship",
                table: "Students",
                newName: "Citizienship");
        }
    }
}
