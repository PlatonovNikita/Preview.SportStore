using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class pach1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Values",
                table: "StrLine");

            migrationBuilder.AddColumn<string>(
                name: "Value",
                table: "StrLine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "StrLine");

            migrationBuilder.AddColumn<string>(
                name: "Values",
                table: "StrLine",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
