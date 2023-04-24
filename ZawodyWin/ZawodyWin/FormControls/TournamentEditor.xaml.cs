using System.Windows;
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

        private void AddNewCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TournamentViewModel viewModel)
            {
                string name = newCompetitionNameTextBox.Text;
                int numRounds = int.Parse(newCompetitionNumRoundsTextBox.Text);
                viewModel.AddCompetition(name, numRounds);
            }
        }

        private void CompetitionListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is TournamentViewModel viewModel)
            {
                viewModel.SelectedCompetition = competitionListView.SelectedItem as CompetitionViewModel;
            }
        }

        private void DeleteCompetitionButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is TournamentViewModel viewModel && sender is Button button)
            {
                var competition = button.DataContext as CompetitionViewModel;
                viewModel.DeleteCompetition(competition);
            }
        }
    }
}
