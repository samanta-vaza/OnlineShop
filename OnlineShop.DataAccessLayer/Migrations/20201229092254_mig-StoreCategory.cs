using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.DataAccessLayer.Migrations
{
    public partial class migStoreCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoreCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Time = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Img = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreCategories_Stores_UserId",
                        column: x => x.UserId,
                        principalTable: "Stores",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_CategoryId",
                table: "StoreCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreCategories_UserId",
                table: "StoreCategories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreCategories");
        }
    }
}
