using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_AssignmentsSolutions_table_to_AssignmentSolution_and_UploadedOn_column_to_SubmittedOn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_Assignments_AssignmentId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentsSolutions",
                table: "AssignmentsSolutions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AssignmentSolution_MarkedOn_UploadedOn",
                table: "AssignmentsSolutions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AssignmentSolution_UploadedOn_SeenOn",
                table: "AssignmentsSolutions");

            migrationBuilder.RenameTable(
                name: "AssignmentsSolutions",
                newName: "AssignmentSolutions");

            migrationBuilder.RenameColumn(
                name: "UploadedOn",
                table: "AssignmentSolutions",
                newName: "SubmittedOn");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentsSolutions_StudentId",
                table: "AssignmentSolutions",
                newName: "IX_AssignmentSolutions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentsSolutions_FileId",
                table: "AssignmentSolutions",
                newName: "IX_AssignmentSolutions_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentsSolutions_AssignmentId",
                table: "AssignmentSolutions",
                newName: "IX_AssignmentSolutions_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentSolutions",
                table: "AssignmentSolutions",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AssignmentSolution_MarkedOn_SubmittedOn",
                table: "AssignmentSolutions",
                sql: "MarkedOn IS NULL OR MarkedOn > SubmittedOn");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AssignmentSolution_SubmittedOn_SeenOn",
                table: "AssignmentSolutions",
                sql: "SubmittedOn IS NULL OR SubmittedOn > SeenOn");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSolutions_Assignments_AssignmentId",
                table: "AssignmentSolutions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSolutions_CloudinaryFiles_FileId",
                table: "AssignmentSolutions",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentSolutions_Students_StudentId",
                table: "AssignmentSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSolutions_Assignments_AssignmentId",
                table: "AssignmentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSolutions_CloudinaryFiles_FileId",
                table: "AssignmentSolutions");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentSolutions_Students_StudentId",
                table: "AssignmentSolutions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentSolutions",
                table: "AssignmentSolutions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AssignmentSolution_MarkedOn_SubmittedOn",
                table: "AssignmentSolutions");

            migrationBuilder.DropCheckConstraint(
                name: "CK_AssignmentSolution_SubmittedOn_SeenOn",
                table: "AssignmentSolutions");

            migrationBuilder.RenameTable(
                name: "AssignmentSolutions",
                newName: "AssignmentsSolutions");

            migrationBuilder.RenameColumn(
                name: "SubmittedOn",
                table: "AssignmentsSolutions",
                newName: "UploadedOn");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentSolutions_StudentId",
                table: "AssignmentsSolutions",
                newName: "IX_AssignmentsSolutions_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentSolutions_FileId",
                table: "AssignmentsSolutions",
                newName: "IX_AssignmentsSolutions_FileId");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentSolutions_AssignmentId",
                table: "AssignmentsSolutions",
                newName: "IX_AssignmentsSolutions_AssignmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentsSolutions",
                table: "AssignmentsSolutions",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AssignmentSolution_MarkedOn_UploadedOn",
                table: "AssignmentsSolutions",
                sql: "MarkedOn IS NULL OR MarkedOn > UploadedOn");

            migrationBuilder.AddCheckConstraint(
                name: "CK_AssignmentSolution_UploadedOn_SeenOn",
                table: "AssignmentsSolutions",
                sql: "UploadedOn IS NULL OR UploadedOn > SeenOn");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_Assignments_AssignmentId",
                table: "AssignmentsSolutions",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_Students_StudentId",
                table: "AssignmentsSolutions",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
