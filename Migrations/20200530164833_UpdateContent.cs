using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_tour_api.Migrations
{
    public partial class UpdateContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlContent",
                table: "Contents",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "UrlContent",
                table: "Contents");
        }
    }
}
