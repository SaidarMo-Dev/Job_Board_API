using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories");

            migrationBuilder.DropIndex(
                name: "IX_JobCategories_JobListingId",
                table: "JobCategories");

            migrationBuilder.DropColumn(
                name: "JobCategoryId",
                table: "JobCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories",
                columns: new[] { "JobListingId", "CategoryId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories");

            migrationBuilder.AddColumn<int>(
                name: "JobCategoryId",
                table: "JobCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobCategories",
                table: "JobCategories",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobCategories_JobListingId",
                table: "JobCategories",
                column: "JobListingId");
        }
    }
}
