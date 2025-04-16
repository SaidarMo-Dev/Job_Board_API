using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Bdv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_ContactsInformations_ContactinformationId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "ContactsInformations");

            migrationBuilder.DropIndex(
                name: "IX_Companies_ContactinformationId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ContactinformationId",
                table: "Companies");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "ContactinformationId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContactsInformations",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(55)", maxLength: 55, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactsInformations", x => x.ContactId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ContactinformationId",
                table: "Companies",
                column: "ContactinformationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_ContactsInformations_ContactinformationId",
                table: "Companies",
                column: "ContactinformationId",
                principalTable: "ContactsInformations",
                principalColumn: "ContactId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
