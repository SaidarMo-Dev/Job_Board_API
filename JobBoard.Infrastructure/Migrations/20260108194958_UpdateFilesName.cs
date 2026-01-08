using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class UpdateFilesName : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Applications_FileResource_ResumeFileId",
				table: "Applications");

			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_FileResource_ProfileImageFileId",
				table: "AspNetUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_Companies_FileResource_LogoFileId",
				table: "Companies");

			migrationBuilder.DropPrimaryKey(
				name: "PK_FileResource",
				table: "FileResource");

			migrationBuilder.RenameTable(
				name: "FileResource",
				newName: "FileResources");

			migrationBuilder.RenameIndex(
				name: "IX_FileResource_Path",
				table: "FileResources",
				newName: "IX_FileResources_Path");

			migrationBuilder.AddPrimaryKey(
				name: "PK_FileResources",
				table: "FileResources",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Applications_FileResources_ResumeFileId",
				table: "Applications",
				column: "ResumeFileId",
				principalTable: "FileResources",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_FileResources_ProfileImageFileId",
				table: "AspNetUsers",
				column: "ProfileImageFileId",
				principalTable: "FileResources",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Companies_FileResources_LogoFileId",
				table: "Companies",
				column: "LogoFileId",
				principalTable: "FileResources",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);



		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Applications_FileResources_ResumeFileId",
				table: "Applications");

			migrationBuilder.DropForeignKey(
				name: "FK_AspNetUsers_FileResources_ProfileImageFileId",
				table: "AspNetUsers");

			migrationBuilder.DropForeignKey(
				name: "FK_Companies_FileResources_LogoFileId",
				table: "Companies");

			migrationBuilder.DropPrimaryKey(
				name: "PK_FileResources",
				table: "FileResources");

			migrationBuilder.RenameTable(
				name: "FileResources",
				newName: "FileResource");

			migrationBuilder.RenameIndex(
				name: "IX_FileResources_Path",
				table: "FileResource",
				newName: "IX_FileResource_Path");

			migrationBuilder.AddPrimaryKey(
				name: "PK_FileResource",
				table: "FileResource",
				column: "Id");

			migrationBuilder.AddForeignKey(
				name: "FK_Applications_FileResource_ResumeFileId",
				table: "Applications",
				column: "ResumeFileId",
				principalTable: "FileResource",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_AspNetUsers_FileResource_ProfileImageFileId",
				table: "AspNetUsers",
				column: "ProfileImageFileId",
				principalTable: "FileResource",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_Companies_FileResource_LogoFileId",
				table: "Companies",
				column: "LogoFileId",
				principalTable: "FileResource",
				principalColumn: "Id",
				onDelete: ReferentialAction.Restrict);
		}
	}
}
