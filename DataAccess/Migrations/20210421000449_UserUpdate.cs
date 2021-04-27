using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class UserUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Pictures_PictureID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PictureID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PictureID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "Users");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "PictureID",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "Users",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PictureID",
                table: "Users",
                column: "PictureID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Pictures_PictureID",
                table: "Users",
                column: "PictureID",
                principalTable: "Pictures",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
