using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EUniManager.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Rename_EducationalAndQualificationalDegree_to_EducationalAndQualificationDegree_in_Diploma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducationalAndQualificationalDegree",
                table: "Diplomas",
                newName: "EducationalAndQualificationDegree");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EducationalAndQualificationDegree",
                table: "Diplomas",
                newName: "EducationalAndQualificationalDegree");
        }
    }
}
