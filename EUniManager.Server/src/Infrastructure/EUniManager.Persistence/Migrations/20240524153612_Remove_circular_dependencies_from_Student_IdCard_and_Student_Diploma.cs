using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Remove_circular_dependencies_from_Student_IdCard_and_Student_Diploma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diplomas_Students_Id",
                table: "Diplomas");

            migrationBuilder.DropForeignKey(
                name: "FK_IdCards_Students_StudentId",
                table: "IdCards");

            migrationBuilder.DropIndex(
                name: "IX_IdCards_StudentId",
                table: "IdCards");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "IdCards");

            migrationBuilder.AddColumn<Guid>(
                name: "DiplomaOwnedId",
                table: "Students",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Students_DiplomaOwnedId",
                table: "Students",
                column: "DiplomaOwnedId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Diplomas_DiplomaOwnedId",
                table: "Students",
                column: "DiplomaOwnedId",
                principalTable: "Diplomas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Diplomas_DiplomaOwnedId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DiplomaOwnedId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DiplomaOwnedId",
                table: "Students");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "IdCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_IdCards_StudentId",
                table: "IdCards",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diplomas_Students_Id",
                table: "Diplomas",
                column: "Id",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IdCards_Students_StudentId",
                table: "IdCards",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
