using Microsoft.EntityFrameworkCore.Migrations;

namespace ServerApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupProperty",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupProperty_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    InStock = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PropType = table.Column<int>(nullable: false),
                    GroupPropertyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_GroupProperty_GroupPropertyId",
                        column: x => x.GroupPropertyId,
                        principalTable: "GroupProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupValues",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupPropertyId = table.Column<long>(nullable: true),
                    ProductId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupValues_GroupProperty_GroupPropertyId",
                        column: x => x.GroupPropertyId,
                        principalTable: "GroupProperty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupValues_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoolLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<bool>(nullable: false),
                    PropertyId = table.Column<long>(nullable: false),
                    GroupValuesId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoolLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoolLine_GroupValues_GroupValuesId",
                        column: x => x.GroupValuesId,
                        principalTable: "GroupValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoolLine_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(nullable: false),
                    PropertyId = table.Column<long>(nullable: true),
                    GroupValuesId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IntLine_GroupValues_GroupValuesId",
                        column: x => x.GroupValuesId,
                        principalTable: "GroupValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntLine_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrLine",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Values = table.Column<string>(nullable: true),
                    PropertyId = table.Column<long>(nullable: false),
                    GroupValuesId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StrLine_GroupValues_GroupValuesId",
                        column: x => x.GroupValuesId,
                        principalTable: "GroupValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StrLine_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoolLine_GroupValuesId",
                table: "BoolLine",
                column: "GroupValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_BoolLine_PropertyId",
                table: "BoolLine",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupProperty_CategoryId",
                table: "GroupProperty",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupValues_GroupPropertyId",
                table: "GroupValues",
                column: "GroupPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupValues_ProductId",
                table: "GroupValues",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IntLine_GroupValuesId",
                table: "IntLine",
                column: "GroupValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_IntLine_PropertyId",
                table: "IntLine",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Property_GroupPropertyId",
                table: "Property",
                column: "GroupPropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_StrLine_GroupValuesId",
                table: "StrLine",
                column: "GroupValuesId");

            migrationBuilder.CreateIndex(
                name: "IX_StrLine_PropertyId",
                table: "StrLine",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoolLine");

            migrationBuilder.DropTable(
                name: "IntLine");

            migrationBuilder.DropTable(
                name: "StrLine");

            migrationBuilder.DropTable(
                name: "GroupValues");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "GroupProperty");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
