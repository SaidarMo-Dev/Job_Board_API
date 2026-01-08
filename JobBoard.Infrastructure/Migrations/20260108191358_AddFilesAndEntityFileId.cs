using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobBoard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFilesAndEntityFileId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LogoFileId",
                table: "Companies",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfileImageFileId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResumeUrl",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "ResumeFileId",
                table: "Applications",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FileResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bucket = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OwnerType = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Visibility = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileResource", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_LogoFileId",
                table: "Companies",
                column: "LogoFileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfileImageFileId",
                table: "AspNetUsers",
                column: "ProfileImageFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ResumeFileId",
                table: "Applications",
                column: "ResumeFileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileResource_Path",
                table: "FileResource",
                column: "Path",
                unique: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "FileResource");

            migrationBuilder.DropIndex(
                name: "IX_Companies_LogoFileId",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfileImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Applications_ResumeFileId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LogoFileId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "ProfileImageFileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ResumeFileId",
                table: "Applications");

            migrationBuilder.AlterColumn<string>(
                name: "ResumeUrl",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
