using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
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
        private Tournament _tournament;
        private IEnumerable<Competition> _competitions;

        public ContestantListViewModel ViewModel { get; set; }

        public ContestantList()
        {
            Initialize();
            contestantAdder.Visibility = Visibility.Collapsed;
            _competitions = _competitionRepository.GetAll();
            PopulateViewModel(_competitions);
        }

        public ContestantList(Tournament tournament)
        {
            Initialize();

            _tournament = tournament;
            _competitions = _competitionRepository.GetByTournamentId(tournament.Id);
            PopulateViewModel(_competitions);
        }

        private void Initialize()
        {
            InitializeComponent();
            _competitionRepository = new CompetitionRepository();
            _contestantRepository = new ContestantRepository();
            _personRepository = new PersonRepository();
            ViewModel = new ContestantListViewModel();
            DataContext = ViewModel;
        }

        private void PopulateViewModel(IEnumerable<Competition> competitions)
        {
            var contestants = _contestantRepository.GetContestantsForCompetitions(competitions.Select(x => x.Id));
            foreach (var contestant in contestants)
            {
                var person = _personRepository.Get(contestant.PersonId);
                var contestantModel = new ContestantViewModel(_contestantRepository, _personRepository);
                contestantModel.SetFromDbModel(contestant, person);
                foreach (var competition in competitions)
                {
                    var competitionModel = new CompetitionViewModel();
                    competitionModel.SetFromDbModel(competition);
                }

                ViewModel.Contestants.Add(contestantModel);
            }
        }

        private void contestantAdder_PersonAddClicked(object sender, FormControls.PersonAddClickedEventArgs e)
        {
            if (ViewModel.DoesContestantExist(e.Person))
            {
                MessageBox.Show($"{e.Person.Name} {e.Person.Surname} już jest na liście zawodników");
            }
            else
            {
                // TODO: set club name on person and contestants
                foreach (var competition in _competitions)
                {
                    var contestant = new Contestant { PersonId = e.Person.Id, CompetitionId = competition.Id };
                    _contestantRepository.Add(contestant);
                    var contestantViewModel = new ContestantViewModel(_contestantRepository, _personRepository);
                    contestantViewModel.SetFromDbModel(contestant, e.Person);
                    ViewModel.Contestants.Add(contestantViewModel);
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the button content to "Save" and enable editing mode for the row
            Button button = (Button)sender;
            button.Content = "Save";
            button.Visibility = Visibility.Collapsed;
            //var row = (DataGridRow)button.TemplatedParent;
            //row.IsSelected = true;
            gridContestants.BeginEdit();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the button content back to "Edit" and disable editing mode for the row
            Button button = (Button)sender;
            button.Content = "Edit";
            button.Visibility = Visibility.Collapsed;
            //var row = (DataGridRow)button.TemplatedParent;
            gridContestants.CommitEdit();
            //row.IsSelected = false;
        }
    }
}
