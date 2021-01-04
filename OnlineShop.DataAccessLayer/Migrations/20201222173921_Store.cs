using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.DataAccessLayer.Migrations
{
    public partial class Store : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Des = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileActivate = table.Column<bool>(type: "bit", nullable: false),
                    MailActivate = table.Column<bool>(type: "bit", nullable: false),
                    MailActivateCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Wallet = table.Column<long>(type: "bigint", nullable: false),
                    BankCard = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Stores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
