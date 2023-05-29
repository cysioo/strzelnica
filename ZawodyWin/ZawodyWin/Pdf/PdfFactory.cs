using iText.Html2pdf;
using iText.Kernel.Events;
using iText.Kernel.Pdf;
using System.IO;
using System.Linq;
using System.Text;
using ZawodyWin.DataModels;
using ZawodyWin.Pdf.DataModels;
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

        public void CreateTournamentPdf(Tournament tournament)
        {
            var data = GetPdfData(tournament);
            var html = CreateTournamentHtml(data);
            CreatePdf(html, data.FooterModel, "C:\\temp\\strzelnica\\wyniki.pdf");
        }

        private PdfData GetPdfData(Tournament tournament)
        {
            var result = new PdfData();
            result.SetTournamentData(tournament);
            var organizer = _shootingClubRepository.Get(tournament.OrganizerId.Value);
            result.SetOrganizer(organizer);
            var referees = _refereeRepository.GetRefereesForTournament(tournament.Id);
            result.SetReferees(referees);
            var competitions = _competitionRepository.GetWithScoresByTournamentId(tournament.Id);
            var competitionIds = competitions.Select(x => x.Id);
            var contestants = _contestantRepository.GetContestantsForCompetitions(competitionIds);
            result.SetCompetitions(competitions, contestants);

            return result;
        }

        public string CreateTournamentHtml(PdfData tournamentData)
        {
            var tournamentProtocolTemplate = File.ReadAllText("Pdf\\Templates\\ResultsTemplate.html");
            var tournamentProtocol = new StringBuilder(tournamentProtocolTemplate);
            tournamentProtocol.Replace("[[organizer-name]]", tournamentData.OrganizerName);
            tournamentProtocol.Replace("[[organizer-license]]", tournamentData.OrganizerLicense);
            tournamentProtocol.Replace("[[organizer-addressLine1]]", tournamentData.OrganizerAddressLine1);
            tournamentProtocol.Replace("[[organizer-addressLine2]]", tournamentData.OrganizerAddressLine2);
            tournamentProtocol.Replace("[[logoPath]]", tournamentData.OrganizerLogoPath);

            tournamentProtocol.Replace("[[tournament-name]]", tournamentData.TournamentName);
            tournamentProtocol.Replace("[[city]]", tournamentData.TournamentCity);
            tournamentProtocol.Replace("[[time]]", tournamentData.TournamentDate);
            tournamentProtocol.Replace("[[place-full-address]]", tournamentData.TournamentFullAddress);

            var competitionResultsTemplate = File.ReadAllText("Pdf\\Templates\\CompetitionResultsTemplate.html");
            var competitionRowTemplate = File.ReadAllText("Pdf\\Templates\\CompetitionRowTemplate.html");
            var tournamentResults = new StringBuilder();
            foreach (var competition in tournamentData.Competitions) 
            { 
                var competitionResults = new StringBuilder(competitionResultsTemplate);

                competitionResults.Replace("[[competition-no]]", competition.Number);
                competitionResults.Replace("[[competition-name]]", competition.Competition);
                competitionResults.Replace("<!--[[competition-series-header-cell]]-->", competition.SeriesHeaderCells);

                var competitionRows = new StringBuilder();
                foreach (var row in competition.ScoreRows) 
                {
                    var competitionRow = new StringBuilder(competitionRowTemplate);
                    competitionRow.Replace("[[contestant-rank]]", row.Rank);
                    competitionRow.Replace("[[contestant-name]]", row.Name);
                    competitionRow.Replace("[[contestant-surname]]", row.Surname);
                    competitionRow.Replace("[[contestant-club]]", row.ClubName);
                    competitionRow.Replace("[[contestant-total]]", row.TotalScore);
                    competitionRow.Replace("[[contestant-notes]]", row.Notes);

                    competitionRow.Replace("<!--[[contestant-series-cells]]-->", row.SeriesCells);
                    competitionRows.Append(competitionRow);
                }
                competitionResults.Replace("<!--[[competition-rows]]-->", competitionRows.ToString());
                tournamentResults.Append(competitionResults);
            }
            tournamentProtocol.Replace("<!--[[competition-results]]-->", tournamentResults.ToString());
            return tournamentProtocol.ToString();
        }

        private void CreatePdf(string html, FooterModel footerModel, string savePath)
        {
            PdfWriter writer = new PdfWriter(savePath);
            PdfDocument pdfDocument = new PdfDocument(writer);
            var footerHandler = new FooterHandler(footerModel);
            pdfDocument.AddEventHandler(PdfDocumentEvent.END_PAGE, footerHandler);
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(html, pdfDocument, converterProperties);
        }
    }
}
