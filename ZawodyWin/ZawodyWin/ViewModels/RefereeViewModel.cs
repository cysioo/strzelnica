using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;
using ZawodyWin.Repositories;

namespace ZawodyWin.ViewModels
{
    public class RefereeViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string _refereeFunction;
        private long? _refereeClass;
        private bool? _isLeading;
        private RefereeRepository _refereeRepository;
        private PersonRepository _personRepository;

        public RefereeViewModel(RefereeRepository refereeRepository, PersonRepository personRepository)
        {
            _refereeRepository = refereeRepository;
            _personRepository = personRepository;
        }

        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                var person = _personRepository.Get(PersonId);
                person.Name = value;
                _personRepository.Update(person);
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                var person = _personRepository.Get(PersonId);
                person.Surname = value;
                _personRepository.Update(person);
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string? RefereeFunction
        {
            get { return _refereeFunction; }
            set
            {
                _refereeFunction = value;
                var referee = _refereeRepository.Get(Id);
                referee.RefereeFunction = value;
                _refereeRepository.Update(referee);
                OnPropertyChanged(nameof(RefereeFunction));
            }
        }

        public long? RefereeClass
        {
            get { return _refereeClass; }
            set
            {
                _refereeClass = value;
                var contestant = _refereeRepository.Get(Id);
                contestant.RefereeClass = value;
                _refereeRepository.Update(contestant);
                OnPropertyChanged(nameof(RefereeClass));
            }
        }
        public bool? IsLeadingTournament
        {
            get { return _isLeading; }
            set
            {
                _isLeading = value;
                var referee = _refereeRepository.Get(Id);
                referee.IsLeading = value ?? false;
                _refereeRepository.Update(referee);
                OnPropertyChanged(nameof(RefereeFunction));
            }
        }

        public void SetFromDbModel(Referee referee, Person person)
        {
            Id = referee.Id;
            PersonId = person.Id;
            Name = person.Name;
            Surname = person.Surname;
            RefereeClass = referee.RefereeClass;
            RefereeFunction = referee.RefereeFunction;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}