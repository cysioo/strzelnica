using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;
using ZawodyWin.ViewModels;

namespace ZawodyWin.FormControls
{
    /// <summary>
    /// Interaction logic for Referees.xaml
    /// </summary>
    public partial class Referees : UserControl
    {
        private RefereeRepository _refereeRepository;
        private PersonRepository _personRepository;
        private Tournament _tournament;

        public RefereeListViewModel ViewModel { get; set; }

        public Tournament Tournament
        {
            get { return _tournament; }
            set
            {
                _tournament = value;
                ViewModel.LoadForTournament(_tournament.Id);
            }
        }

        public Referees()
        {
            InitializeComponent();
            _refereeRepository = new RefereeRepository();
            _personRepository = new PersonRepository();
            ViewModel = new RefereeListViewModel(_refereeRepository, _personRepository);
            DataContext = ViewModel;
        }

        private void refereeAdder_PersonAddClicked(object sender, FormControls.PersonAddClickedEventArgs e)
        {
            if (ViewModel.DoesRefereeExist(e.Person))
            {
                MessageBox.Show($"{e.Person.Name} {e.Person.Surname} już jest na liście zawodników");
            }
            else
            {
                var referee = new Referee
                {
                    TournamentId = _tournament.Id,
                    PersonId = e.Person.Id,
                };
                _refereeRepository.Add(referee);
                ViewModel.AddReferee(referee, e.Person);
            }
        }
    }
}
