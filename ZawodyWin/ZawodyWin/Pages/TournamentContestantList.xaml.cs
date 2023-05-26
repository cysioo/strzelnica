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
    /// Interaction logic for TournamentContestantList.xaml
    /// </summary>
    public partial class TournamentContestantList : Page
    {
        private CompetitionRepository _competitionRepository;
        private ContestantRepository _contestantRepository;
        private PersonRepository _personRepository;
        private ScoreRepository _scoreRepository;
        private Tournament _tournament;
        private IEnumerable<Competition> _competitions;

        public ContestantListViewModel ViewModel { get; set; }

        public TournamentContestantList(Tournament tournament)
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
                var person = contestant.Person; // _personRepository.Get(contestant.PersonId);
                var contestantModel = new ContestantViewModel(_contestantRepository, _personRepository, _scoreRepository);
                contestantModel.SetFromDbModel(contestant, person);
                foreach (var competition in competitions)
                {
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
    }
}
