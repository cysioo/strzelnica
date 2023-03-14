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
    }
}
