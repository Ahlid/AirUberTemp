using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirUberProjeto.Data.Migrations
{
    public partial class removerjetcash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Jetcash",
                table: "Companhia");

            migrationBuilder.DropColumn(
                name: "Jetcash",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Jetcash",
                table: "Companhia",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Jetcash",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
