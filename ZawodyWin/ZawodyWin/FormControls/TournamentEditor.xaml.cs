using System.Linq;
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
                string errorMessage = null;
                int numRounds;
                if (string.IsNullOrWhiteSpace(newCompetitionNameTextBox.Text))
                {
                    errorMessage = "Podaj nazwę konkurencji\n";
                }
                else if (viewModel.Competitions.Any(x => x.Name == newCompetitionNameTextBox.Text.Trim()))
                {
                    errorMessage = "Ta konkurencja już istnieje\n";
                }

                if (string.IsNullOrWhiteSpace(newCompetitionNumRoundsTextBox.Text))
                {
                    errorMessage += "Podaj liczbę serii w konkurencji";
                }
                else if (!int.TryParse(newCompetitionNumRoundsTextBox.Text, out numRounds) || numRounds < 1)
                {
                    errorMessage += "Liczba serii musi być dodatnią liczbą całkowitą";
                }

                if (errorMessage != null)
                {
                    MessageBox.Show(errorMessage, "Błąd przy dodawaniu konkurencji");
                }
                else
                {
                    string name = newCompetitionNameTextBox.Text.Trim();
                    numRounds = int.Parse(newCompetitionNumRoundsTextBox.Text);
                    viewModel.AddCompetition(name, numRounds);
                }
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
