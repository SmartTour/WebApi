using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_tour_api.Migrations
{
    public partial class AddEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseTourZone",
                table: "BaseTourZone");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BaseTourZone",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "BaseTourZone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "BaseTourZone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseTourZone",
                table: "BaseTourZone",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BaseTourZone_BaseTourID",
                table: "BaseTourZone",
                column: "BaseTourID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseTourZone",
                table: "BaseTourZone");

            migrationBuilder.DropIndex(
                name: "IX_BaseTourZone_BaseTourID",
                table: "BaseTourZone");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BaseTourZone");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "BaseTourZone");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "BaseTourZone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseTourZone",
                table: "BaseTourZone",
                columns: new[] { "BaseTourID", "ContentID" });
        }
    }
}
