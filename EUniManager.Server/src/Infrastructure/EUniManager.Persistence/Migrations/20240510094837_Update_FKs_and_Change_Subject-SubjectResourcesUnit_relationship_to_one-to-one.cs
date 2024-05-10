using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Update_FKs_and_Change_SubjectSubjectResourcesUnit_relationship_to_onetoone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_Id",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_PayedTaxes_Students_StudentId",
                table: "PayedTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectResourcesUnits_Subjects_SubjectId",
                table: "SubjectResourcesUnits");

            migrationBuilder.DropIndex(
                name: "IX_SubjectResourcesUnits_SubjectId",
                table: "SubjectResourcesUnits");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "SubjectResourcesUnits");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_Id",
                table: "Exams",
                column: "Id",
                principalTable: "Subjects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PayedTaxes_Students_StudentId",
                table: "PayedTaxes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources",
                column: "SubjectResourcesUnitId",
                principalTable: "SubjectResourcesUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students",
                column: "CourseScheduleId",
                principalTable: "CourseSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectResourcesUnits_Subjects_Id",
                table: "SubjectResourcesUnits",
                column: "Id",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Exams_Subjects_Id",
                table: "Exams");

            migrationBuilder.DropForeignKey(
                name: "FK_PayedTaxes_Students_StudentId",
                table: "PayedTaxes");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectResourcesUnits_Subjects_Id",
                table: "SubjectResourcesUnits");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "SubjectResourcesUnits",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SubjectResourcesUnits_SubjectId",
                table: "SubjectResourcesUnits",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exams_Subjects_Id",
                table: "Exams",
                column: "Id",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PayedTaxes_Students_StudentId",
                table: "PayedTaxes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources",
                column: "SubjectResourcesUnitId",
                principalTable: "SubjectResourcesUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Specialties_Faculties_FacultyId",
                table: "Specialties",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_CourseSchedules_CourseScheduleId",
                table: "Students",
                column: "CourseScheduleId",
                principalTable: "CourseSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectResourcesUnits_Subjects_SubjectId",
                table: "SubjectResourcesUnits",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
