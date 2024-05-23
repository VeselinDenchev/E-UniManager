using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_relationship_between_Students_and_IndividualProtocols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StudentId",
                table: "IndividualProtocols",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_IndividualProtocols_StudentId",
                table: "IndividualProtocols",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualProtocols_Students_StudentId",
                table: "IndividualProtocols",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndividualProtocols_Students_StudentId",
                table: "IndividualProtocols");

            migrationBuilder.DropIndex(
                name: "IX_IndividualProtocols_StudentId",
                table: "IndividualProtocols");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "IndividualProtocols");
        }
    }
}
