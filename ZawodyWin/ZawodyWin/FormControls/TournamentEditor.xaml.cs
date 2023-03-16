using System.Windows.Controls;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for TournamentEditor.xaml
    /// </summary>
    public partial class TournamentEditor : UserControl
    {
        public TournamentViewModel Tournament
        {
            get { return (TournamentViewModel)DataContext; }
            set { DataContext = value; }
        }

        public TournamentEditor()
        {
            InitializeComponent();
            if (Tournament == null)
            {
                Tournament = new TournamentViewModel();
            }

            DataContext = Tournament;
        }
    }
}
