using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirUberProjeto.Data.Migrations
{
    public partial class companhiaLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanhiaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdministrador",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanhiaId",
                table: "AspNetUsers",
                column: "CompanhiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Companhia_CompanhiaId",
                table: "AspNetUsers",
                column: "CompanhiaId",
                principalTable: "Companhia",
                principalColumn: "CompanhiaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Companhia_CompanhiaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanhiaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanhiaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdministrador",
                table: "AspNetUsers");
        }
    }
}
