using iText.Html2pdf;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;

namespace ZawodyWin.Pdf
{
    public class PdfFactory
    {
        private TournamentRepository _tournamentRepository;
        private ShootingClubRepository _shootingClubRepository;
        private CompetitionRepository _competitionRepository;
        private ContestantRepository _contestantRepository;
        private PersonRepository _personRepository;
        private RefereeRepository _refereeRepository;
        private ScoreRepository _scoreRepository;

        public PdfFactory()
        {
            _competitionRepository = new CompetitionRepository();
            _personRepository = new PersonRepository();
            _scoreRepository = new ScoreRepository();
            _contestantRepository = new ContestantRepository();
            _refereeRepository = new RefereeRepository();
            _tournamentRepository = new TournamentRepository();
            _shootingClubRepository = new ShootingClubRepository();
        }
        //public string CreateTournamentPdf(long tournamentId)
        //{
        //    // Cre
        //}

        public string CreateTournamentHtml(Tournament tournament)
        {
            var organizer = _shootingClubRepository.Get(tournament.OrganizerId.Value);
            var tournamentProtocolTemplate = File.ReadAllText("Pdf\\Templates\\ResultsTemplate.html");
            var tournamentProtocol = new StringBuilder(tournamentProtocolTemplate);
            tournamentProtocol.Replace("[[organizer-name]]", organizer.Name);
            tournamentProtocol.Replace("[[organizer-license]]", organizer.License);
            tournamentProtocol.Replace("[[organizer-addressLine1]]", organizer.AddressLine1);
            tournamentProtocol.Replace("[[organizer-addressLine2]]", organizer.AddressLine2);
            tournamentProtocol.Replace("[[logoPath]]", organizer.LogoPath);

            tournamentProtocol.Replace("[[tournament-name]]", tournament.Name);
            tournamentProtocol.Replace("[[city]]", tournament.City);
            tournamentProtocol.Replace("[[time]]", tournament.Date.ToString());
            tournamentProtocol.Replace("[[place-full-address]]", tournament.FullAddress);

            var competitionResultsTemplate = File.ReadAllText("Pdf\\Templates\\CompetitionResultsTemplate.html");
            var competitionRowTemplate = File.ReadAllText("Pdf\\Templates\\CompetitionRowTemplate.html");
            var competitions = _competitionRepository.GetWithScoresByTournamentId(tournament.Id);
            var competitionIds = competitions.Select(x => x.Id);
            var contestants = _contestantRepository.GetContestantsForCompetitions(competitionIds);
            //var scores = _scoreRepository.GetScoresForCompetitions(competitionIds);
            var tournamentResults = new StringBuilder();
            for (var i = 1; i <= competitions.Count(); i++) {
                var competition = competitions.ElementAt(i - 1);
                var competitionContestants = contestants.Where(x => x.CompetitionId == competition.Id);
                var competitionResults = new StringBuilder(competitionResultsTemplate);

                competitionResults.Replace("[[competition-no]]", i.ToString());
                competitionResults.Replace("[[competition-name]]", competition.Name);
                var seriesHeaderCells = new StringBuilder();
                for (var round = 1; round <= competition.NumberOfRounds; round++)
                {
                    seriesHeaderCells.AppendFormat("<td>Seria {0}</td>", round);
                }
                competitionResults.Replace("<!--[[competition-series-header-cell]]-->", seriesHeaderCells.ToString());

                var competitionRows = new StringBuilder();
                var orderedScores = competition.Scores.GroupBy(x => x.ContestantId).OrderByDescending(x => x.Sum(g => g.Points));
                for (var rank = 1; rank <= orderedScores.Count(); rank++)
                {
                    var competitorScores = orderedScores.ElementAt(rank - 1);
                    var competitionRow = new StringBuilder(competitionRowTemplate);
                    competitionRow.Replace("[[contestant-rank]]", rank.ToString());
                    var contestant = contestants.First(x => x.Id == competitorScores.Key);
                    competitionRow.Replace("[[contestant-name]]", contestant.Person.Name);
                    competitionRow.Replace("[[contestant-surname]]", contestant.Person.Surname);
                    competitionRow.Replace("[[contestant-club]]", contestant.Person.ClubName);
                    competitionRow.Replace("[[contestant-total]]", competitorScores.Sum(x => x.Points).ToString());
                    competitionRow.Replace("[[contestant-notes]]", contestant.Person.Surname);

                    var seriesCells = new StringBuilder();
                    for (var round = 1; round <= competition.NumberOfRounds; round++)
                    {
                        var score = competitorScores.FirstOrDefault(x => x.Round == round);
                        if (score != null)
                        {
                            seriesCells.AppendFormat("<td>{0}</td>", score.Points);
                        }
                        else
                        {
                            seriesCells.Append("<td>0</td>");
                        }
                    }
                    competitionRow.Replace("<!--[[contestant-series-cells]]-->", seriesCells.ToString());
                    competitionRows.Append(competitionRow);
                }
                competitionResults.Replace("<!--[[competition-rows]]-->", competitionRows.ToString());
                tournamentResults.Append(competitionResults);
            }
            tournamentProtocol.Replace("<!--[[competition-results]]-->", tournamentResults.ToString());
            return tournamentProtocol.ToString();
        }

        public void CreatePdf(string html, string savePath)
        {
            PdfWriter writer = new PdfWriter(savePath);
            PdfDocument pdfDocument = new PdfDocument(writer);
            var footerHandler = new FooterHandler();
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, footerHandler);
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(html, pdfDocument, converterProperties);
        }
    }
}
