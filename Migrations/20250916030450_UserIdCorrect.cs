using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cashcontrol.API.Migrations
{
    /// <inheritdoc />
    public partial class UserIdCorrect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Users_UserId1",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UserId1",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Accounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId1",
                table: "Accounts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Users_UserId1",
                table: "Accounts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
