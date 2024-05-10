using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Refactor_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseScheduleUnits_Subjects_SubjectId",
                table: "CourseScheduleUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestApplications_Students_StudentId",
                table: "RequestApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "CourseScheduleCourseScheduleUnit");

            migrationBuilder.DropTable(
                name: "SubjectResourcesUnits");

            migrationBuilder.DropTable(
                name: "CourseSchedules");

            migrationBuilder.DropIndex(
                name: "IX_Students_CourseScheduleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "CourseScheduleId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Courses");

            migrationBuilder.RenameColumn(
                name: "SubjectResourcesUnitId",
                table: "Resources",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_SubjectResourcesUnitId",
                table: "Resources",
                newName: "IX_Resources_ActivityId");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "CourseScheduleUnits",
                newName: "ActivityId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseScheduleUnits_SubjectId",
                table: "CourseScheduleUnits",
                newName: "IX_CourseScheduleUnits_ActivityId");

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Subjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.CheckConstraint("CK_Activity_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_Activities_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_SubjectId",
                table: "Activities",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TeacherId",
                table: "Activities",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseScheduleUnits_Activities_ActivityId",
                table: "CourseScheduleUnits",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestApplications_Students_StudentId",
                table: "RequestApplications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseScheduleUnits_Activities_ActivityId",
                table: "CourseScheduleUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_RequestApplications_Students_StudentId",
                table: "RequestApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Subjects");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "Resources",
                newName: "SubjectResourcesUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_ActivityId",
                table: "Resources",
                newName: "IX_Resources_SubjectResourcesUnitId");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "CourseScheduleUnits",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseScheduleUnits_ActivityId",
                table: "CourseScheduleUnits",
                newName: "IX_CourseScheduleUnits_SubjectId");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Subjects",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CourseScheduleId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CourseSchedules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSchedules", x => x.Id);
                    table.CheckConstraint("CK_CourseSchedule_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.CreateTable(
                name: "SubjectResourcesUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectResourcesUnits", x => x.Id);
                    table.CheckConstraint("CK_SubjectResourcesUnit_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_SubjectResourcesUnits_Subjects_Id",
                        column: x => x.Id,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseScheduleCourseScheduleUnit",
                columns: table => new
                {
                    CourseSchedulesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseScheduleCourseScheduleUnit", x => new { x.CourseSchedulesId, x.UnitsId });
                    table.ForeignKey(
                        name: "FK_CourseScheduleCourseScheduleUnit_CourseScheduleUnits_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "CourseScheduleUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseScheduleCourseScheduleUnit_CourseSchedules_CourseSchedulesId",
                        column: x => x.CourseSchedulesId,
                        principalTable: "CourseSchedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseScheduleId",
                table: "Students",
                column: "CourseScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseScheduleCourseScheduleUnit_UnitsId",
                table: "CourseScheduleCourseScheduleUnit",
                column: "UnitsId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseScheduleUnits_Subjects_SubjectId",
                table: "CourseScheduleUnits",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RequestApplications_Students_StudentId",
                table: "RequestApplications",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources",
                column: "SubjectResourcesUnitId",
                principalTable: "SubjectResourcesUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students",
                column: "CourseScheduleId",
                principalTable: "CourseSchedules",
                principalColumn: "Id");
        }
    }
}
