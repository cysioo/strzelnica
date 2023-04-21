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
        private OrganizerRepository _organizerRepository;

        public TournamentAdd()
        {
            InitializeComponent();

            _organizerRepository = new OrganizerRepository();
            var tournament = new TournamentViewModel();
            tournamentEditor.Tournament = tournament;
            var allOrganizers = _organizerRepository.GetAll();
            tournamentEditor.Tournament.PopulateOrganizers(allOrganizers);
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tournament = tournamentEditor.Tournament.ToDbModel();
            var repo = new TournamentRepository();
            var id = repo.Add(tournament);
            tournament.Id = id;
            NavigationService.Navigate(new TournamentEdit(tournament));
        }
    }
}
