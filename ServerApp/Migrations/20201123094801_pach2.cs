using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class pach2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolLine_Property_PropertyId",
                table: "BoolLine");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine");

            migrationBuilder.DropForeignKey(
                name: "FK_StrLine_Property_PropertyId",
                table: "StrLine");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "StrLine",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "DoubleLine",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "BoolLine",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolLine_Property_PropertyId",
                table: "BoolLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StrLine_Property_PropertyId",
                table: "StrLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolLine_Property_PropertyId",
                table: "BoolLine");

            migrationBuilder.DropForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine");

            migrationBuilder.DropForeignKey(
                name: "FK_StrLine_Property_PropertyId",
                table: "StrLine");

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "StrLine",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "DoubleLine",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PropertyId",
                table: "BoolLine",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BoolLine_Property_PropertyId",
                table: "BoolLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoubleLine_Property_PropertyId",
                table: "DoubleLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StrLine_Property_PropertyId",
                table: "StrLine",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
