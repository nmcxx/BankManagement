using Microsoft.EntityFrameworkCore.Migrations;

namespace BankManagement.WebAPI.Migrations
{
    public partial class Exchage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Currencies_CurrenciesCurrencyId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CurrenciesCurrencyId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CurrenciesCurrencyId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "AccountBalance",
                table: "Customers",
                newName: "AccountBalancce");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "AccountBalancce",
                table: "Customers",
                newName: "AccountBalance");

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "Deals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrenciesCurrencyId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CurrenciesCurrencyId",
                table: "Customers",
                column: "CurrenciesCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Currencies_CurrenciesCurrencyId",
                table: "Customers",
                column: "CurrenciesCurrencyId",
                principalTable: "Currencies",
                principalColumn: "CurrencyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
