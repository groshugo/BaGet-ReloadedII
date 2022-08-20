using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaGet.Database.Sqlite.Migrations
{
    public partial class ReloadedApiKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiKey",
                table: "Packages",
                type: "TEXT",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApiKey",
                table: "Packages");
        }
    }
}
