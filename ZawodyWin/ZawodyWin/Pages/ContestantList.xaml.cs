using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.Migrations;
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
        private ScoreRepository _scoreRepository;
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
            _scoreRepository = new ScoreRepository();
            ViewModel = new ContestantListViewModel();
            DataContext = ViewModel;
        }

        private void PopulateViewModel(IEnumerable<Competition> competitions)
        {
            var competitionIds = competitions.Select(x => x.Id);
            var contestants = _contestantRepository.GetContestantsForCompetitions(competitionIds);
            var scores = _scoreRepository.GetScoresForCompetitions(competitionIds);
            var numberOfRounds = competitions.Max(x => x.NumberOfRounds);
            foreach (var contestant in contestants)
            {
                var person = _personRepository.Get(contestant.PersonId);
                var contestantModel = new ContestantViewModel(_contestantRepository, _personRepository, _scoreRepository);
                contestantModel.SetFromDbModel(contestant, person);
                //contestantModel.CompetitionsDataTable.Columns.Add("Konkurencja");
                //for (int i = 0; i < numberOfRounds; i++)
                //{
                //    contestantModel.CompetitionsDataTable.Columns.Add($"Runda {i}");
                //}
                //contestantModel.CompetitionsDataTable.Columns.Add("Razem");
                foreach (var competition in competitions)
                {
                    //var row = new List<object>();
                    //var rowRow = new DataGridTemplateColumn();
                    //row.Add(competition.Name);
                    //for (var i = 0; i < numberOfRounds; ++i)
                    //{
                    //    row.Add(0);
                    //}
                    //row.Add(2); // total
                    //contestantModel.CompetitionsDataTable.Rows.Add(row);

                    var contestantScores = scores.Where(s => s.ContestantId == contestant.Id && s.CompetitionId == competition.Id);
                    var competitionModel = new ContestantsCompetitionViewModel();
                    competitionModel.SetFromDbModel(competition);

                    foreach (Score score in contestantScores.OrderBy(x => x.Round))
                    {
                        var scoreModel = new ScoreViewModel(_scoreRepository);
                        scoreModel.SetFromDbModel(score);
                        competitionModel.Scores.Add(scoreModel);
                    }

                    contestantModel.Competitions.Add(competitionModel);
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
                    var scores = _scoreRepository.AddContestantToCompetition(contestant.Id, competition);
                    var contestantViewModel = new ContestantViewModel(_contestantRepository, _personRepository, _scoreRepository);
                    contestantViewModel.SetFromDbModel(contestant, e.Person);
                    // TODO: ?? probably set competition and scores on the model
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

        private void tbPointsInRound_LostFocus(object sender, RoutedEventArgs e)
        {
            var tbPoints = (TextBox)sender;
            var oldPoints = tbPoints.DataContext;
            var newPointsTxt = tbPoints.Text;
        }
    }
}
