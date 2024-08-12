using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_unique_constraint_in_Specialties_to_Name_EducationType_FirstAcademicYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.CreateIndex(
                name: "IX_Name_EducationType_FirstAcademicYearStart",
                table: "Specialties",
                columns: new[] { "Name", "EducationType", "FirstAcademicYearStart" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Name_EducationType_FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.CreateIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties",
                columns: new[] { "Name", "FirstAcademicYearStart" },
                unique: true);
        }
    }
}
