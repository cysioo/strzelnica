using iText.Html2pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var htmlTemplate = File.ReadAllText("Pdf\\Templates\\ResultsTemplate.html");
            var htmlBuilder = new StringBuilder(htmlTemplate);
            htmlBuilder.Replace("[[organizer-name]]", organizer.Name);
            htmlBuilder.Replace("[[organizer-license]]", organizer.License);
            htmlBuilder.Replace("[[organizer-addressLine1]]", organizer.AddressLine1);
            htmlBuilder.Replace("[[organizer-addressLine2]]", organizer.AddressLine2);
            htmlBuilder.Replace("[[logoPath]]", organizer.LogoPath);

            htmlBuilder.Replace("[[tournament-name]]", tournament.Name);
            htmlBuilder.Replace("[[city]]", tournament.City);
            htmlBuilder.Replace("[[time]]", tournament.Date.ToString());
            htmlBuilder.Replace("[[place-full-address]]", tournament.FullAddress);
            htmlBuilder.Replace("[[tournament-name]]", tournament.Name);
            htmlBuilder.Replace("[[tournament-name]]", tournament.Name);

            return htmlBuilder.ToString();
        }

        public void CreatePdf(string html, string savePath)
        {
            var pdfStream = File.Create(savePath);
            ConverterProperties converterProperties = new ConverterProperties();
            HtmlConverter.ConvertToPdf(html, pdfStream, converterProperties);
        }
    }
}
