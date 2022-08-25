using Microsoft.EntityFrameworkCore.Migrations;

namespace RealEstateApp.Infrastructure.Persistence.Migrations
{
    public partial class AddImageUrlFieldsToThePropertyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl1",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl2",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl3",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl4",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favorites",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl1",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImageUrl2",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImageUrl3",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ImageUrl4",
                table: "Properties");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Favorites",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
