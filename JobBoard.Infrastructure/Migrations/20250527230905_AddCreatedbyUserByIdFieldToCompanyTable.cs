using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedbyUserByIdFieldToCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedByUserId",
                table: "Companies",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedByUserId",
                table: "Companies",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedByUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CreatedByUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Companies");
        }
    }
}
