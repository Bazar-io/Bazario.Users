using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bazario.Users.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserPropertiesUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDateUpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EmailUpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstNameUpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastNameUpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PhoneNumberUpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDateUpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EmailUpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FirstNameUpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastNameUpdatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumberUpdatedAt",
                table: "Users");
        }
    }
}
