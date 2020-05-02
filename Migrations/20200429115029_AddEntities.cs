using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace smart_tour_api.Migrations
{
    public partial class AddEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AgencyID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseTours_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AgencyID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    ContentHtml = table.Column<string>(nullable: true),
                    UrlImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetectionElements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AgencyID = table.Column<int>(nullable: false),
                    Tecnology = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetectionElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetectionElements_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Indications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LiveTours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastModifiedDate = table.Column<DateTime>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    AgencyID = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveTours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LiveTours_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseTourZone",
                columns: table => new
                {
                    ContentID = table.Column<int>(nullable: false),
                    BaseTourID = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTourZone", x => new { x.BaseTourID, x.ContentID });
                    table.ForeignKey(
                        name: "FK_BaseTourZone_BaseTours_BaseTourID",
                        column: x => x.BaseTourID,
                        principalTable: "BaseTours",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaseTourZone_Contents_ContentID",
                        column: x => x.ContentID,
                        principalTable: "Contents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseTours_AgencyID",
                table: "BaseTours",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseTourZone_ContentID",
                table: "BaseTourZone",
                column: "ContentID");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_AgencyID",
                table: "Contents",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_DetectionElements_AgencyID",
                table: "DetectionElements",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_LiveTours_AgencyID",
                table: "LiveTours",
                column: "AgencyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseTourZone");

            migrationBuilder.DropTable(
                name: "DetectionElements");

            migrationBuilder.DropTable(
                name: "Indications");

            migrationBuilder.DropTable(
                name: "LiveTours");

            migrationBuilder.DropTable(
                name: "BaseTours");

            migrationBuilder.DropTable(
                name: "Contents");
        }
    }
}
