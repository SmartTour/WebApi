using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_tour_api.Migrations
{
    public partial class AddEntities3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LiveTourZone",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    LiveTourID = table.Column<int>(nullable: false),
                    DetectionElementID = table.Column<int>(nullable: false),
                    NextIndicationID = table.Column<int>(nullable: false),
                    ContentID = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTourZone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTourZone_Contents_ContentID",
                        column: x => x.ContentID,
                        principalTable: "Contents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiveTourZone_DetectionElements_DetectionElementID",
                        column: x => x.DetectionElementID,
                        principalTable: "DetectionElements",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiveTourZone_LiveTours_LiveTourID",
                        column: x => x.LiveTourID,
                        principalTable: "LiveTours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LiveTourZone_Indications_NextIndicationID",
                        column: x => x.NextIndicationID,
                        principalTable: "Indications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LiveTourZone_ContentID",
                table: "LiveTourZone",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTourZone_DetectionElementID",
                table: "LiveTourZone",
                column: "DetectionElementID");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTourZone_LiveTourID",
                table: "LiveTourZone",
                column: "LiveTourID");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTourZone_NextIndicationID",
                table: "LiveTourZone",
                column: "NextIndicationID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LiveTourZone");
        }
    }
}
