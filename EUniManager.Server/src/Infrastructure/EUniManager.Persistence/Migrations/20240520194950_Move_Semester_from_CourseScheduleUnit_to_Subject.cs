using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Move_Semester_from_CourseScheduleUnit_to_Subject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_CourseScheduleUnit_Semester",
                table: "CourseScheduleUnits");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "CourseScheduleUnits");

            migrationBuilder.AddColumn<byte>(
                name: "Semester",
                table: "Subjects",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Subject_Semester",
                table: "Subjects",
                sql: "Semester BETWEEN 1 AND 16");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Subject_Semester",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Subjects");

            migrationBuilder.AddColumn<byte>(
                name: "Semester",
                table: "CourseScheduleUnits",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddCheckConstraint(
                name: "CK_CourseScheduleUnit_Semester",
                table: "CourseScheduleUnits",
                sql: "Semester BETWEEN 1 AND 16");
        }
    }
}
