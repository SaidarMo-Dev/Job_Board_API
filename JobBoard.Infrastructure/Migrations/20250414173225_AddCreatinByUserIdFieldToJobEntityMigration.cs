using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class AddCreatinByUserIdFieldToJobEntityMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "jobCreationLogs");

			migrationBuilder.AddColumn<int>(
				name: "CreatedByUserId",
				table: "JobsListings",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.CreateIndex(
				name: "IX_JobsListings_CreatedByUserId",
				table: "JobsListings",
				column: "CreatedByUserId");

			migrationBuilder.AddForeignKey(
				name: "FK_JobsListings_AspNetUsers_CreatedByUserId",
				table: "JobsListings",
				column: "CreatedByUserId",
				principalTable: "AspNetUsers",
				principalColumn: "Id",
				onDelete: ReferentialAction.NoAction);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_JobsListings_AspNetUsers_CreatedByUserId",
				table: "JobsListings");

			migrationBuilder.DropIndex(
				name: "IX_JobsListings_CreatedByUserId",
				table: "JobsListings");

			migrationBuilder.DropColumn(
				name: "CreatedByUserId",
				table: "JobsListings");

			migrationBuilder.CreateTable(
				name: "jobCreationLogs",
				columns: table => new
				{
					LogId = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					JobId = table.Column<int>(type: "int", nullable: false),
					UserId = table.Column<int>(type: "int", nullable: false),
					CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_jobCreationLogs", x => x.LogId);
					table.ForeignKey(
						name: "FK_jobCreationLogs_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_jobCreationLogs_JobsListings_JobId",
						column: x => x.JobId,
						principalTable: "JobsListings",
						principalColumn: "JobId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_jobCreationLogs_JobId",
				table: "jobCreationLogs",
				column: "JobId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_jobCreationLogs_UserId",
				table: "jobCreationLogs",
				column: "UserId");
		}
	}
}
