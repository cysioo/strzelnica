using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZawodyWin.DataModels;

namespace ZawodyWin.Pdf.DataModels
{
    public class PdfData
    {
        public PdfData()
        {
        }

        public void SetTournamentData(Tournament tournament)
        {
            TournamentName = tournament.Name;
            TournamentDate = tournament.Date.Value.ToString("dd MMMM yyyy");
            TournamentFullAddress = tournament.FullAddress;
            TournamentCity = tournament.City;
        }

        public void SetOrganizer(ShootingClub organizer)
        {
            OrganizerName = organizer.Name;
            OrganizerLicense = organizer.License;
            OrganizerAddressLine1 = organizer.AddressLine1;
            OrganizerAddressLine2 = organizer.AddressLine2;
            OrganizerLogoPath = organizer.LogoPath;
        }

        public void SetReferees(IEnumerable<Referee> referees)
        {
            foreach (var referee in referees)
            {
                var refereeeForPdf = new RefereeData
                {
                    Name = referee.Person.Name,
                    Surname = referee.Person.Surname,
                    Class = RefereeData.RomanNumberLookup[referee.RefereeClass ?? 3]
                };
                switch (referee.RefereeFunction)
                {
                    case "Sędzia Główny Zawodów":
                        {
                            MainReferee = refereeeForPdf;
                            break;
                        }
                    case "Przewodniczący Komisji RTS":
                        {
                            RtsComissionLeader = refereeeForPdf;
                            break;
                        }
                    default:
                        {
                            OtherReferees.Add(refereeeForPdf);
                            break;
                        }
                }
            }
        }

        public void SetCompetitions(IEnumerable<Competition> competitions, IEnumerable<Contestant> allContestants)
        {
            for (var i = 1; i <= competitions.Count(); i++)
            {
                var protocol = new CompetitionProtocol();
                var competition = competitions.ElementAt(i - 1);
                var competitionContestants = allContestants.Where(x => x.CompetitionId == competition.Id);

                protocol.Number = i.ToString();
                protocol.Competition = competition.Name;
                var seriesHeaderCells = new StringBuilder();
                for (var round = 1; round <= competition.NumberOfRounds; round++)
                {
                    seriesHeaderCells.AppendFormat("<th>Seria {0}</th>", round);
                }
                protocol.SeriesHeaderCells = seriesHeaderCells.ToString();

                var competitionRows = new StringBuilder();
                var orderedScores = competition.Scores.GroupBy(x => x.ContestantId).OrderByDescending(x => x.Sum(g => g.Points));
                for (var rank = 1; rank <= orderedScores.Count(); rank++)
                {
                    var row = new ContestantScoreRow();
                    row.Rank = rank.ToString();
                    var competitorScores = orderedScores.ElementAt(rank - 1);
                    var contestant = allContestants.First(x => x.Id == competitorScores.Key);
                    row.Name = contestant.Person.Name;
                    row.Surname = contestant.Person.Surname;
                    row.ClubName = contestant.Person.ClubName ?? string.Empty;
                    row.TotalScore = competitorScores.Sum(x => x.Points).ToString();
                    // TODO: set row.Notes

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
                    row.SeriesCells = seriesCells.ToString();
                    protocol.ScoreRows.Add(row);
                }

                Competitions.Add(protocol);
            }
        }

        public FooterModel FooterModel
        {
            get
            {
                var model = new FooterModel
                {
                    TournamentName = TournamentName,
                    TournamentDate = TournamentDate
                };

                if (RtsComissionLeader != null)
                {
                    model.RefereeName = RtsComissionLeader.Name;
                    model.RefereeSurname = RtsComissionLeader.Surname;
                    model.RefereeClass = RtsComissionLeader.Class;
                }

                return model;
            }
        }

        public string TournamentName { get; set; }
        public string TournamentDate { get; set; }
        public string? TournamentCity { get; set; }
        public string? TournamentFullAddress { get; set; }

        public string  OrganizerName { get; internal set; }
        public string? OrganizerLicense { get; internal set; }
        public string? OrganizerAddressLine1 { get; internal set; }
        public string? OrganizerAddressLine2 { get; internal set; }
        public string? OrganizerLogoPath { get; internal set; }

        public RefereeData MainReferee { get; set; }
        public RefereeData RtsComissionLeader { get; set; }
        public IList<RefereeData> OtherReferees { get; } = new List<RefereeData>();
        public IList<CompetitionProtocol> Competitions { get; } = new List<CompetitionProtocol>();
    }
}