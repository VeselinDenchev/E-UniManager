using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_unique_constraints_to_Subject_and_Specialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Protocol",
                table: "Subjects",
                column: "Protocol",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties",
                columns: new[] { "Name", "FirstAcademicYearStart" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Protocol",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties");
        }
    }
}
