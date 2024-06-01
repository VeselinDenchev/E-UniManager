using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Make_Activity_Student_relationship_many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Activities_ActivityId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_ActivityId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Students");

            migrationBuilder.CreateTable(
                name: "ActivityStudent",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStudent", x => new { x.ActivitiesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_ActivityStudent_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityStudent_Students_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStudent_StudentsId",
                table: "ActivityStudent",
                column: "StudentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityStudent");

            migrationBuilder.AddColumn<Guid>(
                name: "ActivityId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_ActivityId",
                table: "Students",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Activities_ActivityId",
                table: "Students",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");
        }
    }
}
