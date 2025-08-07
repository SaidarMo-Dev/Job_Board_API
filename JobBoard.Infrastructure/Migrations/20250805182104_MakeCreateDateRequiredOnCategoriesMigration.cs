using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class MakeCreateDateRequiredOnCategoriesMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateOnly>(
				name: "CreateDate",
				table: "Categories",
				type: "date",
				nullable: false,
				defaultValue: new DateOnly(2025, 1, 1),
				oldClrType: typeof(DateOnly),
				oldType: "date",
				oldNullable: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<DateOnly>(
				name: "CreateDate",
				table: "Categories",
				type: "date",
				nullable: true,
				oldClrType: typeof(DateOnly),
				oldType: "date");
		}
	}
}
