using System;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for TournamentAdd.xaml
    /// </summary>
    public partial class TournamentAdd : Page
    {
        private ShootingClubRepository _shootingClubRepository;
        private TournamentRepository _tournamentRepository;
        private CompetitionRepository _competitionRepository;

        public TournamentAdd()
        {
            InitializeComponent();

            _shootingClubRepository = new ShootingClubRepository();
            _tournamentRepository = new TournamentRepository();
            _competitionRepository = new CompetitionRepository();

            var tournament = new TournamentViewModel();
            tournamentEditor.Tournament = tournament;
            var allOrganizers = _shootingClubRepository.GetAll();
            tournamentEditor.Tournament.PopulateOrganizers(allOrganizers);
        }

        private bool IsViewModelValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(tournamentEditor.Tournament.Name))
                {
                    MessageBox.Show("Wpisz nazwę turnieju");
                    return false;
                }
                return true;
            }
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (IsViewModelValid)
            {
                var tournament = tournamentEditor.Tournament.ToDbModel();
                var id = _tournamentRepository.Add(tournament);
                tournament.Id = id;

                _competitionRepository.SetTournamentCompetitions(id, tournamentEditor.Tournament.Competitions);

                NavigationService.Navigate(new TournamentEdit(tournament));
            }
        }
    }
}
