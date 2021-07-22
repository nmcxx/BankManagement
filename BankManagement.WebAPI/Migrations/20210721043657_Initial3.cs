using Microsoft.EntityFrameworkCore.Migrations;

namespace BankManagement.WebAPI.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Roles_RolesRoleId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RolesRoleId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RolesRoleId",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "RolesRoleId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RolesRoleId",
                table: "Customers",
                column: "RolesRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Roles_RolesRoleId",
                table: "Customers",
                column: "RolesRoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
