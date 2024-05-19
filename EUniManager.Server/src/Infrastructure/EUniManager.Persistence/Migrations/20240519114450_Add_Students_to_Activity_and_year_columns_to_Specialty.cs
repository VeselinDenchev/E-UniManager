using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_Students_to_Activity_and_year_columns_to_Specialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseScheduleUnits_Activities_ActivityId",
                table: "CourseScheduleUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "CurrentYear",
                table: "Specialties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "FirstAcademicYearStart",
                table: "Specialties",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ActivityId",
                table: "Students",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseScheduleUnits_Activities_ActivityId",
                table: "CourseScheduleUnits",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Activities_ActivityId",
                table: "Students",
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
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Activities_ActivityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ActivityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CurrentYear",
                table: "Specialties");

            migrationBuilder.DropColumn(
                name: "FirstAcademicYearStart",
                table: "Specialties");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseScheduleUnits_Activities_ActivityId",
                table: "CourseScheduleUnits",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_Activities_ActivityId",
                table: "Resources",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
