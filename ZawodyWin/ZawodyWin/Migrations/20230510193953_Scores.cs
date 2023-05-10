using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZawodyWin.Migrations
{
    /// <inheritdoc />
    public partial class Scores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Scores",
                table: "Contestant");

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ContestantId = table.Column<long>(type: "INTEGER", nullable: false),
                    CompetitionId = table.Column<long>(type: "INTEGER", nullable: false),
                    Round = table.Column<long>(type: "INTEGER", nullable: false),
                    Points = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.AddColumn<string>(
                name: "Scores",
                table: "Contestant",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
