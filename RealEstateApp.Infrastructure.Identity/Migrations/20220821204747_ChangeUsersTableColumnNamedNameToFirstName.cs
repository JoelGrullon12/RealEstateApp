using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApp.Infrastructure.Identity.Migrations
{
    public partial class ChangeUsersTableColumnNamedNameToFirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "Identity",
                table: "Users",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                schema: "Identity",
                table: "Users",
                newName: "Name");
        }
    }
}
