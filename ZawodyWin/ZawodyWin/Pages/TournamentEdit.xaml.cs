using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for TournamentEdit.xaml
    /// </summary>
    public partial class TournamentEdit : Page
    {
        private long _tournamentId;
        private TournamentRepository _tournamentRepository;
        private ShootingClubRepository _shootingClubRepository;
        private CompetitionRepository _competitionRepository;

        public TournamentEdit(Tournament tournament)
        {
            InitializeComponent();
            _tournamentRepository = new TournamentRepository();
            _shootingClubRepository = new ShootingClubRepository();
            _competitionRepository = new CompetitionRepository();

            _tournamentId = tournament.Id;
            tournamentEditor.Tournament = new TournamentViewModel();
            tournamentEditor.Tournament.SetFromDbModel(tournament);
            var allOrganizers = _shootingClubRepository.GetAll();
            tournamentEditor.Tournament.PopulateOrganizers(allOrganizers);
            var competitions = _competitionRepository.GetByTournamentId(tournament.Id);
            tournamentEditor.Tournament.PopulateCompetitions(competitions);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var tournament = tournamentEditor.Tournament.ToDbModel();
            tournament.Id = _tournamentId;
            var updateSucceeded = _tournamentRepository.Update(tournament);

            _competitionRepository.SetTournamentCompetitions(_tournamentId, tournamentEditor.Tournament.Competitions);

            if (updateSucceeded) { MessageBox.Show("Turniej zapisany."); }
        }

        private void btnContestantsRedirect_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ContestantList(_tournamentId));
        }
    }
}
