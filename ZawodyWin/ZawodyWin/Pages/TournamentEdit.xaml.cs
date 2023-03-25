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

        public TournamentEdit(Tournament tournament)
        {
            InitializeComponent();
            _tournamentRepository = new TournamentRepository();
            _tournamentId = tournament.Id;
            tournamentEditor.Tournament = new TournamentViewModel();
            tournamentEditor.Tournament.SetDbModel(tournament);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var tournament = tournamentEditor.Tournament.CreateDbModel();
            tournament.Id = _tournamentId;
            var updateSucceeded = _tournamentRepository.Update(tournament);
            if (updateSucceeded) { MessageBox.Show("Turniej zapisany."); }
        }
    }
}
