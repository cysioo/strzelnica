using System.Windows;
using System.Windows.Controls;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.Pages
{
    /// <summary>
    /// Interaction logic for ContestantList.xaml
    /// </summary>
    public partial class ContestantList : Page
    {
        private CompetitionRepository _competitionRepository;
        private ContestantRepository _contestantRepository;
        private PersonRepository _personRepository;

        public ContestantListViewModel ViewModel { get; set; }

        public ContestantList()
        {
            InitializeComponent();
            _competitionRepository = new CompetitionRepository();
            _contestantRepository = new ContestantRepository();
            _personRepository = new PersonRepository();
            ViewModel = new ContestantListViewModel();
            DataContext = ViewModel;
        }

        public ContestantList(long tournamentId): this() 
        {
            var competitions = _competitionRepository.GetByTournamentId(tournamentId);
            var contestants = _contestantRepository.GetTournamentContestants(tournamentId);
            foreach ( var contestant in contestants)
            {
                var person = _personRepository.Get(contestant.PersonId);
                var contestantModel = new ContestantViewModel();
                contestantModel.SetFromDbModel(contestant, person);
                foreach (var competition in competitions)
                {
                    var competitionModel = new CompetitionViewModel();
                    competitionModel.SetFromDbModel(competition);
                }
            }
        }

        private void contestantAdder_PersonAddClicked(object sender, FormControls.PersonAddClickedEventArgs e)
        {
            // add contestant if not added yet
            // display message otherwise
            //ViewModel.AddContestant();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the button content to "Save" and enable editing mode for the row
            Button button = (Button)sender;
            button.Content = "Save";
            button.Visibility = Visibility.Collapsed;
            var row = (DataGridRow)button.TemplatedParent;
            row.IsSelected = true;
            gridContestants.BeginEdit();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the button content back to "Edit" and disable editing mode for the row
            Button button = (Button)sender;
            button.Content = "Edit";
            button.Visibility = Visibility.Collapsed;
            var row = (DataGridRow)button.TemplatedParent;
            gridContestants.CommitEdit();
            row.IsSelected = false;
        }
    }
}
