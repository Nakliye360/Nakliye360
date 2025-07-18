﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nakliye360.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AppUserAddFieldLastRemoteIp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastLoginIpAddress",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginIpAddress",
                table: "AspNetUsers");
        }
    }
}
