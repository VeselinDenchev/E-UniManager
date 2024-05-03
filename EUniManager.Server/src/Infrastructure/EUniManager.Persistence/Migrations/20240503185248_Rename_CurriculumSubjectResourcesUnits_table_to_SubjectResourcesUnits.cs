using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_CurriculumSubjectResourcesUnits_table_to_SubjectResourcesUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_CurriculumSubjectResourcesUnits_CurriculumSubjectResourcesUnitId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "CurriculumSubjectResourcesUnits");

            migrationBuilder.RenameColumn(
                name: "CurriculumSubjectResourcesUnitId",
                table: "Resources",
                newName: "SubjectResourcesUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_CurriculumSubjectResourcesUnitId",
                table: "Resources",
                newName: "IX_Resources_SubjectResourcesUnitId");

            migrationBuilder.CreateTable(
                name: "SubjectResourcesUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectResourcesUnits", x => x.Id);
                    table.CheckConstraint("CK_SubjectResourcesUnit_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_SubjectResourcesUnits_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectResourcesUnits_SubjectId",
                table: "SubjectResourcesUnits",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources",
                column: "SubjectResourcesUnitId",
                principalTable: "SubjectResourcesUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_SubjectResourcesUnits_SubjectResourcesUnitId",
                table: "Resources");

            migrationBuilder.DropTable(
                name: "SubjectResourcesUnits");

            migrationBuilder.RenameColumn(
                name: "SubjectResourcesUnitId",
                table: "Resources",
                newName: "CurriculumSubjectResourcesUnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Resources_SubjectResourcesUnitId",
                table: "Resources",
                newName: "IX_Resources_CurriculumSubjectResourcesUnitId");

            migrationBuilder.CreateTable(
                name: "CurriculumSubjectResourcesUnits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Semester = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumSubjectResourcesUnits", x => x.Id);
                    table.CheckConstraint("CK_CurriculumSubjectResourcesUnit_Semester", "Semester BETWEEN 1 AND 16");
                    table.ForeignKey(
                        name: "FK_CurriculumSubjectResourcesUnits_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumSubjectResourcesUnits_SubjectId",
                table: "CurriculumSubjectResourcesUnits",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_CurriculumSubjectResourcesUnits_CurriculumSubjectResourcesUnitId",
                table: "Resources",
                column: "CurriculumSubjectResourcesUnitId",
                principalTable: "CurriculumSubjectResourcesUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
