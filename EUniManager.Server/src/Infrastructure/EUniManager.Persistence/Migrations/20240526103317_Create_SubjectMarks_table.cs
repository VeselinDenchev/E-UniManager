using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_SubjectMarks_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mark",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SubjectMarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Mark = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectMarks", x => x.Id);
                    table.CheckConstraint("CK_SubjectMark_Mark", "Mark BETWEEN 2 AND 6");
                    table.CheckConstraint("CK_SubjectMark_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.ForeignKey(
                        name: "FK_SubjectMarks_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectMarks_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMarks_StudentId",
                table: "SubjectMarks",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMarks_SubjectId",
                table: "SubjectMarks",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectMarks");

            migrationBuilder.AddColumn<int>(
                name: "Mark",
                table: "Subjects",
                type: "int",
                nullable: true);
        }
    }
}
