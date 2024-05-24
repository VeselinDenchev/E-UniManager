using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_check_constraint_in_Specialty_for_FirstAcademicYear_between_2020_and_2099 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties",
                sql: "FirstAcademicYearStart BETWEEN 2020 AND 2099");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties",
                sql: "FirstAcademicYearStart BETWEEN 2024 AND 2099");
        }
    }
}
