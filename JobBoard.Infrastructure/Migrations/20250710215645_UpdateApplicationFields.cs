using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateApplicationFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobsListings_JobListingId",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "JobListingId",
                table: "Applications",
                newName: "JobId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Applications",
                newName: "CoverLetter");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_JobListingId",
                table: "Applications",
                newName: "IX_Applications_JobId");

            migrationBuilder.AddColumn<string>(
                name: "Availability",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Portfolio",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobsListings_JobId",
                table: "Applications",
                column: "JobId",
                principalTable: "JobsListings",
                principalColumn: "JobId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_JobsListings_JobId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Availability",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Portfolio",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "JobId",
                table: "Applications",
                newName: "JobListingId");

            migrationBuilder.RenameColumn(
                name: "CoverLetter",
                table: "Applications",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                newName: "IX_Applications_JobListingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_JobsListings_JobListingId",
                table: "Applications",
                column: "JobListingId",
                principalTable: "JobsListings",
                principalColumn: "JobId");
        }
    }
}
