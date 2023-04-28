using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContextRepro.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kontakt",
                columns: table => new
                {
                    KontaktId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KontaktGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "('00000000-0000-0000-0000-000000000000')"),
                    KundenNummer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Kontakt", x => x.KontaktId);
                });

            migrationBuilder.CreateTable(
                name: "Sequence",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Sequence", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BelegAdresse",
                columns: table => new
                {
                    AdressId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdressGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "('00000000-0000-0000-0000-000000000000')"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktId = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.BelegAdresse", x => x.AdressId);
                    table.ForeignKey(
                        name: "FK_dbo.BelegAdresse_dbo.Kontakt_KontaktId",
                        column: x => x.KontaktId,
                        principalTable: "Kontakt",
                        principalColumn: "KontaktId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vorgang",
                columns: table => new
                {
                    VorgangId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VorgangGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KontaktId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Vorgang", x => x.VorgangId);
                    table.ForeignKey(
                        name: "FK_dbo.Vorgang_dbo.Kontakt_KontaktId",
                        column: x => x.KontaktId,
                        principalTable: "Kontakt",
                        principalColumn: "KontaktId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beleg",
                columns: table => new
                {
                    BelegId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BelegGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VorgangId = table.Column<long>(type: "bigint", nullable: true),
                    BelegAdresse_AdressId = table.Column<long>(type: "bigint", nullable: true),
                    VersandAdresse_AdressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo.Beleg", x => x.BelegId);
                    table.ForeignKey(
                        name: "FK_Beleg_dbo.BelegAdresse_BelegAdresse_AdressId",
                        column: x => x.BelegAdresse_AdressId,
                        principalTable: "BelegAdresse",
                        principalColumn: "AdressId");
                    table.ForeignKey(
                        name: "FK_Beleg_dbo.BelegAdresse_VersandAdresse_AdressId",
                        column: x => x.VersandAdresse_AdressId,
                        principalTable: "BelegAdresse",
                        principalColumn: "AdressId");
                    table.ForeignKey(
                        name: "FK_Beleg_dbo.Vorgang_VorgangId",
                        column: x => x.VorgangId,
                        principalTable: "Vorgang",
                        principalColumn: "VorgangId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BelegAdresse_AdressId",
                table: "Beleg",
                column: "BelegAdresse_AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_VersandAdresse_AdressId",
                table: "Beleg",
                column: "VersandAdresse_AdressId");

            migrationBuilder.CreateIndex(
                name: "IX_VorgangId",
                table: "Beleg",
                column: "VorgangId");

            migrationBuilder.CreateIndex(
                name: "IX_KontaktId",
                table: "BelegAdresse",
                column: "KontaktId");

            migrationBuilder.CreateIndex(
                name: "IX_KontaktId",
                table: "Vorgang",
                column: "KontaktId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Beleg");

            migrationBuilder.DropTable(
                name: "Sequence");

            migrationBuilder.DropTable(
                name: "BelegAdresse");

            migrationBuilder.DropTable(
                name: "Vorgang");

            migrationBuilder.DropTable(
                name: "Kontakt");
        }
    }
}
