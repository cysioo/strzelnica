using System;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
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
            var tournament = GetTournamentFromViewModel(tournamentEditor.Tournament);
            MessageBox.Show(tournament.Name);
        }

        private Tournament GetTournamentFromViewModel(TournamentViewModel tournament)
        {
            var result = new Tournament();
            result.Name = tournament.Name;
            result.Place = tournament.Place;
            result.Date = tournament.Date;
            result.LeadingRefereeId = tournament.LeadingRefereeId;
            result.OrganizerId = tournament.OrganizerId;
            return result;
        }
    }
}
