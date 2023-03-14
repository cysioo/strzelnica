using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for TournamentEditor.xaml
    /// </summary>
    public partial class TournamentEditor : UserControl
    {
        public static readonly DependencyProperty TournamentProperty =
            DependencyProperty.Register("Tournament", typeof(TournamentViewModel), typeof(TournamentEditor), new PropertyMetadata(null));

        public TournamentViewModel Tournament
        {
            get { return (TournamentViewModel)GetValue(TournamentProperty); }
            set { SetValue(TournamentProperty, value); }
        }

        public TournamentEditor()
        {
            InitializeComponent();
        }
    }
}
