using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShop.DataAccessLayer.Migrations
{
    public partial class Setting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SiteDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SiteKeys = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmsApi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SmsSender = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    MailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");
        }
    }
}
