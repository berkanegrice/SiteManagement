using Microsoft.EntityFrameworkCore.Migrations;

namespace PermissionManagement.MVC.Data.Migrations
{
    public partial class UpdateDuesInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DuesInformations");

            migrationBuilder.DropColumn(
                name: "IsPayed",
                table: "DuesInformations");

            migrationBuilder.RenameColumn(
                name: "RoomNo",
                table: "DuesInformations",
                newName: "Debt");

            migrationBuilder.RenameColumn(
                name: "LeaserSurname",
                table: "DuesInformations",
                newName: "Credit");

            migrationBuilder.RenameColumn(
                name: "LeaserName",
                table: "DuesInformations",
                newName: "BalanceDebt");

            migrationBuilder.RenameColumn(
                name: "ApartmentName",
                table: "DuesInformations",
                newName: "BalanceCredit");

            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "DuesInformations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "DuesInformations");

            migrationBuilder.RenameColumn(
                name: "Debt",
                table: "DuesInformations",
                newName: "RoomNo");

            migrationBuilder.RenameColumn(
                name: "Credit",
                table: "DuesInformations",
                newName: "LeaserSurname");

            migrationBuilder.RenameColumn(
                name: "BalanceDebt",
                table: "DuesInformations",
                newName: "LeaserName");

            migrationBuilder.RenameColumn(
                name: "BalanceCredit",
                table: "DuesInformations",
                newName: "ApartmentName");

            migrationBuilder.AddColumn<float>(
                name: "Amount",
                table: "DuesInformations",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "IsPayed",
                table: "DuesInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
