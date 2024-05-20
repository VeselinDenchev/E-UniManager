using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_default_value_for_CurrentYear_and_check_constraint_for_FirstAcademicYear_to_Specialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "CurrentYear",
                table: "Specialties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties",
                sql: "FirstAcademicYearStart BETWEEN 2024 AND 2099");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.AlterColumn<byte>(
                name: "CurrentYear",
                table: "Specialties",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)1);
        }
    }
}
