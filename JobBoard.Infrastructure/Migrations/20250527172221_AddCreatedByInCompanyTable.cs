using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCreatedByInCompanyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedUserId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_CreatedUserId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CreatedUserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "Companies");
        }
    }
}
