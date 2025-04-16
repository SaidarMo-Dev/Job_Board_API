using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsSkills",
                table: "JobsSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobsSkills_SkillId",
                table: "JobsSkills");

            migrationBuilder.DropColumn(
                name: "JobSkillId",
                table: "JobsSkills");

            migrationBuilder.DropColumn(
                name: "CreatedbyUserId",
                table: "Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsSkills",
                table: "JobsSkills",
                columns: new[] { "SkillId", "JobListingId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JobsSkills",
                table: "JobsSkills");

            migrationBuilder.AddColumn<int>(
                name: "JobSkillId",
                table: "JobsSkills",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CreatedbyUserId",
                table: "Companies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobsSkills",
                table: "JobsSkills",
                column: "JobSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSkills_SkillId",
                table: "JobsSkills",
                column: "SkillId");
        }
    }
}
