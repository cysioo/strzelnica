using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZawodyWin.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadingRefereeId",
                table: "Tournament");

            migrationBuilder.AddColumn<bool>(
                name: "IsLeading",
                table: "Referee",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_OrganizerId",
                table: "Tournament",
                column: "OrganizerId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_CompetitionId",
                table: "Score",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_ContestantId",
                table: "Score",
                column: "ContestantId");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_PersonId",
                table: "Referee",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Referee_TournamentId",
                table: "Referee",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Contestant_CompetitionId",
                table: "Contestant",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Contestant_PersonId",
                table: "Contestant",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Competition_TournamentId",
                table: "Competition",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Competition_Tournament_TournamentId",
                table: "Competition",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contestant_Competition_CompetitionId",
                table: "Contestant",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contestant_Person_PersonId",
                table: "Contestant",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Referee_Person_PersonId",
                table: "Referee",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Referee_Tournament_TournamentId",
                table: "Referee",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Competition_CompetitionId",
                table: "Score",
                column: "CompetitionId",
                principalTable: "Competition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Contestant_ContestantId",
                table: "Score",
                column: "ContestantId",
                principalTable: "Contestant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_ShootingClub_OrganizerId",
                table: "Tournament",
                column: "OrganizerId",
                principalTable: "ShootingClub",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competition_Tournament_TournamentId",
                table: "Competition");

            migrationBuilder.DropForeignKey(
                name: "FK_Contestant_Competition_CompetitionId",
                table: "Contestant");

            migrationBuilder.DropForeignKey(
                name: "FK_Contestant_Person_PersonId",
                table: "Contestant");

            migrationBuilder.DropForeignKey(
                name: "FK_Referee_Person_PersonId",
                table: "Referee");

            migrationBuilder.DropForeignKey(
                name: "FK_Referee_Tournament_TournamentId",
                table: "Referee");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Competition_CompetitionId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Contestant_ContestantId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_ShootingClub_OrganizerId",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_OrganizerId",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Score_CompetitionId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_ContestantId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Referee_PersonId",
                table: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_Referee_TournamentId",
                table: "Referee");

            migrationBuilder.DropIndex(
                name: "IX_Contestant_CompetitionId",
                table: "Contestant");

            migrationBuilder.DropIndex(
                name: "IX_Contestant_PersonId",
                table: "Contestant");

            migrationBuilder.DropIndex(
                name: "IX_Competition_TournamentId",
                table: "Competition");

            migrationBuilder.DropColumn(
                name: "IsLeading",
                table: "Referee");

            migrationBuilder.AddColumn<long>(
                name: "LeadingRefereeId",
                table: "Tournament",
                type: "INTEGER",
                nullable: true);
        }
    }
}
