using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookstore.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    PurchaseId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: false),
                    AddressLine2 = table.Column<string>(nullable: true),
                    AddressLine3 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    Zip = table.Column<string>(nullable: false),
                    Country = table.Column<string>(nullable: false),
                    Gift = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.PurchaseId);
                });

            migrationBuilder.CreateTable(
                name: "BookLineItem",
                columns: table => new
                {
                    LineId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    PurchaseId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookLineItem", x => x.LineId);
                    table.ForeignKey(
                        name: "FK_BookLineItem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookLineItem_Purchases_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "Purchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookLineItem_BookId",
                table: "BookLineItem",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookLineItem_PurchaseId",
                table: "BookLineItem",
                column: "PurchaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookLineItem");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Purchases");
        }
    }
}
