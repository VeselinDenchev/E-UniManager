using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_CertifiedSemesters_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertifiedSemesters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertifiedSemesters", x => x.Id);
                    table.CheckConstraint("CK_CertifiedSemester_ModifiedAt", "ModifiedAt >= CreatedAt");
                    table.CheckConstraint("CK_CertifiedSemester_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_CertifiedSemesters_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CertifiedSemesters_StudentId",
                table: "CertifiedSemesters",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CertifiedSemesters");
        }
    }
}
