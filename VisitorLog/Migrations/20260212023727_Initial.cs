using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorLog.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "qrcodes",
                columns: table => new
                {
                    QRCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QRCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QRCodeAlias = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qrcodes", x => x.QRCodeId);
                });

            migrationBuilder.CreateTable(
                name: "visitors",
                columns: table => new
                {
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurposeOfVisit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visitors", x => x.VisitorId);
                });

            migrationBuilder.CreateTable(
                name: "logs",
                columns: table => new
                {
                    LogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    QRCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_logs_qrcodes_QRCodeId",
                        column: x => x.QRCodeId,
                        principalTable: "qrcodes",
                        principalColumn: "QRCodeId");
                    table.ForeignKey(
                        name: "FK_logs_visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "visitors",
                        principalColumn: "VisitorId");
                });

            migrationBuilder.CreateTable(
                name: "qrsets",
                columns: table => new
                {
                    QRSetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QRCodeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qrsets", x => x.QRSetId);
                    table.ForeignKey(
                        name: "FK_qrsets_qrcodes_QRCodeId",
                        column: x => x.QRCodeId,
                        principalTable: "qrcodes",
                        principalColumn: "QRCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_qrsets_visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "visitors",
                        principalColumn: "VisitorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_logs_QRCodeId",
                table: "logs",
                column: "QRCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_logs_VisitorId",
                table: "logs",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_qrsets_QRCodeId",
                table: "qrsets",
                column: "QRCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_qrsets_VisitorId",
                table: "qrsets",
                column: "VisitorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "logs");

            migrationBuilder.DropTable(
                name: "qrsets");

            migrationBuilder.DropTable(
                name: "qrcodes");

            migrationBuilder.DropTable(
                name: "visitors");
        }
    }
}
