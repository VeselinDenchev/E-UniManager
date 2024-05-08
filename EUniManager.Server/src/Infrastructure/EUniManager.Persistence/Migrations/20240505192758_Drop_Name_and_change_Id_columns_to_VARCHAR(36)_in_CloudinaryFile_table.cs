using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Drop_Name_and_change_Id_columns_to_VARCHAR36_in_CloudinaryFile_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_CloudinaryFiles_FileId",
                table: "Resources");
            
            migrationBuilder.DropForeignKey(
                name: "FK_RequestApplications_CloudinaryFiles_FileId",
                table: "RequestApplications");
            
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions");

            migrationBuilder.DropTable(
                name: "CloudinaryFiles");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "Resources",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "RequestApplications",
                type: "varchar(36)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "FileId",
                table: "AssignmentsSolutions",
                type: "varchar(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
            
            migrationBuilder.CreateTable(
                name: "CloudinaryFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", nullable: false),
                    Extension = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryFiles", x => x.Id);
                    table.CheckConstraint("CK_CloudinaryFile_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_CloudinaryFiles_FileId",
                table: "Resources",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_RequestApplications_CloudinaryFiles_FileId",
                table: "RequestApplications",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");
            
            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_CloudinaryFiles_FileId",
                table: "Resources");
            
            migrationBuilder.DropForeignKey(
                name: "FK_RequestApplications_CloudinaryFiles_FileId",
                table: "RequestApplications");
            
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                table: "Resources",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                table: "RequestApplications",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36)");

            migrationBuilder.AlterColumn<Guid>(
                name: "FileId",
                table: "AssignmentsSolutions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldNullable: true);
            
            migrationBuilder.DropTable(
                name: "CloudinaryFiles");
            
            migrationBuilder.CreateTable(
                name: "CloudinaryFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Extension = table.Column<string>(type: "varchar(4)", unicode: false, maxLength: 4, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudinaryFiles", x => x.Id);
                    table.CheckConstraint("CK_CloudinaryFile_ModifiedAt", "ModifiedAt >= CreatedAt");
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_CloudinaryFiles_FileId",
                table: "Resources",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_RequestApplications_CloudinaryFiles_FileId",
                table: "RequestApplications",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentsSolutions_CloudinaryFiles_FileId",
                table: "AssignmentsSolutions",
                column: "FileId",
                principalTable: "CloudinaryFiles",
                principalColumn: "Id");
        }
    }
}
