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
        public TournamentAdd()
        {
            InitializeComponent();

            var tournament = new TournamentViewModel();
            tournamentEditor.Tournament = tournament;
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tournament = tournamentEditor.Tournament.CreateDbModel();
            var repo = new TournamentRepository();
            var id = repo.Add(tournament);
            tournament.Id = id;
            NavigationService.Navigate(new TournamentEdit(tournament));
        }
    }
}
