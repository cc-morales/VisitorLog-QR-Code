using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisitorLog.Migrations
{
    /// <inheritdoc />
    public partial class entrytype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "logs",
                newName: "Timestamp");

            migrationBuilder.AddColumn<int>(
                name: "EntryType",
                table: "logs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryType",
                table: "logs");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "logs",
                newName: "DateCreated");
        }
    }
}
