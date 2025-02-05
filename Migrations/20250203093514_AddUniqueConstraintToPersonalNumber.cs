using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalDataDirectory.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraintToPersonalNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Person_PersonalNumber",
                table: "PersonEntities",
                column: "PersonalNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_PersonalNumber",
                table: "PersonEntities");
        }
    }
}
