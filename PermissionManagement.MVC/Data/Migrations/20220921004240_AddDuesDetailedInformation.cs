using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Data.Migrations
{
    public partial class AddDuesDetailedInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DuesDetailedInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Debt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceDebt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BalanceCredit = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DuesDetailedInformations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DuesDetailedInformations");
        }
    }
}
