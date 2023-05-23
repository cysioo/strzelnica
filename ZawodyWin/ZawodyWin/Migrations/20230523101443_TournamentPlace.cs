using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZawodyWin.Migrations
{
    /// <inheritdoc />
    public partial class TournamentPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Tournament",
                newName: "FullAddress");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Tournament",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Tournament");

            migrationBuilder.RenameColumn(
                name: "FullAddress",
                table: "Tournament",
                newName: "Place");
        }
    }
}
