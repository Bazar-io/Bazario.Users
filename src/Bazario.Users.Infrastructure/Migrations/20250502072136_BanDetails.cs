using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bazario.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BanDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BanDetails_BlockedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BanDetails_ExpiresAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BanDetails_Reason",
                table: "Users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BanDetails_BlockedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BanDetails_ExpiresAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BanDetails_Reason",
                table: "Users");
        }
    }
}
