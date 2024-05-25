using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_check_constraint_for_Specialty_FirstAcademicYearStart_plus_CurrentYear_less_than_or_equal_to_current_year : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart_CurrentYear",
                table: "Specialties",
                sql: "FirstAcademicYearStart + CurrentYear <= YEAR(GETDATE()) + 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart_CurrentYear",
                table: "Specialties");
        }
    }
}
