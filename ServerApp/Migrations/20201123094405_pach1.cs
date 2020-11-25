using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class pach1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "DoubleLine",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "DoubleLine",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
