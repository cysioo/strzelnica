using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;

namespace ZawodyWin.ViewModels
{
    public class RefereeListViewModel : INotifyPropertyChanged
    {
        private RefereeRepository _refereeRepository;
        private PersonRepository _personRepository;
        private ObservableCollection<RefereeViewModel> _referees;

        public ObservableCollection<RefereeViewModel> Referees
        {
            get { return _referees; }
            set { _referees = value; OnPropertyChanged(); }
        }

        public RefereeListViewModel(RefereeRepository refereeRepository, PersonRepository personRepository)
        {
            _refereeRepository = refereeRepository;
            _personRepository = personRepository;
            Referees = new ObservableCollection<RefereeViewModel>();
        }

        public void LoadForTournament(long tournamentId)
        {
            var dbReferees = _refereeRepository.GetRefereesForTournament(tournamentId);
            foreach (var referee in dbReferees)
            {
                AddReferee(referee, referee.Person);
            }
        }

        public bool DoesRefereeExist(Person person)
        {
            return Referees.Any(x => x.PersonId == person.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void AddReferee(Referee referee, Person person)
        {
            var model = new RefereeViewModel(_refereeRepository, _personRepository)
            {
                Id = referee.Id,
                PersonId = referee.PersonId,
                Name = person.Name,
                Surname = person.Surname,
                RefereeClass = referee.RefereeClass
            };
            _referees.Add(model);
        }

        public IEnumerable<Referee> ToDbModels()
        {
            foreach (var model in _referees)
            {
                var dbModel = new Referee
                {
                    Id = model.Id,
                    PersonId = model.PersonId,
                    RefereeFunction = model.RefereeFunction,
                    RefereeClass = model.RefereeClass,
                };

                yield return dbModel;
            }
        }
    }
}
