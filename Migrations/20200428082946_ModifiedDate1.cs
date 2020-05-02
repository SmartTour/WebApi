using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_tour_api.Migrations
{
    public partial class ModifiedDate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agencies",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Agencies",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Agencies",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Agencies",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Agencies");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Agencies");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Agencies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
