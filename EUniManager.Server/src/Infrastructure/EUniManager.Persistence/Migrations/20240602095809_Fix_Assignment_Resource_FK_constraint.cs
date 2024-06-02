using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fix_Assignment_Resource_FK_constraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments");

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "Assignments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ResourceId",
                table: "Assignments",
                column: "ResourceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Resources_ResourceId",
                table: "Assignments",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Resources_ResourceId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_ResourceId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Assignments");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Resources_Id",
                table: "Assignments",
                column: "Id",
                principalTable: "Resources",
                principalColumn: "Id");
        }
    }
}
