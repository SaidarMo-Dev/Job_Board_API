using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeRelationBetweenCompanyAndFileResource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_FileResources_LogoFileId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoFileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "BannerUrl",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "LogoFileId",
                table: "Companies");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "FileResources",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_Owner_Category",
                table: "FileResources",
                columns: new[] { "OwnerId", "OwnerType", "Category" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FileResource_Owner_Category",
                table: "FileResources");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "FileResources");

            migrationBuilder.AddColumn<string>(
                name: "BannerUrl",
                table: "Companies",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoFileId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoFileId",
                table: "Companies",
                column: "LogoFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_FileResources_LogoFileId",
                table: "Companies",
                column: "LogoFileId",
                principalTable: "FileResources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
