using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobApplicationsGR.Migrations
{
    public partial class AddDateApplied : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateApplied",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateApplied",
                table: "Jobs");
        }
    }
}
