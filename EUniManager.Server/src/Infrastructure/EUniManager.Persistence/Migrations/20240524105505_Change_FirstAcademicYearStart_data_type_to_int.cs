using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Change_FirstAcademicYearStart_data_type_to_int : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties");
            
            migrationBuilder.DropCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties");
            
            migrationBuilder.AlterColumn<int>(
                name: "FirstAcademicYearStart",
                table: "Specialties",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");
            
            migrationBuilder.CreateIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties",
                columns: new[] { "Name", "FirstAcademicYearStart" },
                unique: true);
            
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
            
            migrationBuilder.DropIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties");
            
            migrationBuilder.AlterColumn<byte>(
                name: "FirstAcademicYearStart",
                table: "Specialties",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            
            migrationBuilder.CreateIndex(
                name: "IX_Name_FirstAcademicYearStart",
                table: "Specialties",
                columns: new[] { "Name", "FirstAcademicYearStart" },
                unique: true);
            
            migrationBuilder.AddCheckConstraint(
                name: "CK_Specialty_FirstAcademicYearStart",
                table: "Specialties",
                sql: "FirstAcademicYearStart BETWEEN 2024 AND 2099");
        }
    }
}
