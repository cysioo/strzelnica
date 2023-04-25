using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;
using ZawodyWin.FormControls;

namespace ZawodyWin.ViewModels
{
    public class TournamentViewModel : INotifyPropertyChanged
    {
        private string _name;
        private DateTime? _date;
        private long? _organizerId;
        private string? _place;
        private long? _leadingRefereeId;
        private ObservableCollection<CompetitionViewModel> _competitions = new ObservableCollection<CompetitionViewModel>();
        private CompetitionViewModel _selectedCompetition;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public long? OrganizerId
        {
            get { return _organizerId; }
            set { _organizerId = value; OnPropertyChanged(); }
        }

        public IDictionary<long, string> AvailableOrganizers { get; private set; } = new Dictionary<long, string>();

        public string? Place
        {
            get { return _place; }
            set { _place = value; OnPropertyChanged(); }
        }

        public ObservableCollection<CompetitionViewModel> Competitions
        {
            get { return _competitions; }
            set { _competitions = value; OnPropertyChanged(nameof(Competitions)); }
        }

        public CompetitionViewModel SelectedCompetition
        {
            get { return _selectedCompetition; }
            set { _selectedCompetition = value; OnPropertyChanged(nameof(SelectedCompetition)); }
        }

        public long? LeadingRefereeId
        {
            get { return _leadingRefereeId; }
            set { _leadingRefereeId = value; OnPropertyChanged(); }
        }

        public Tournament ToDbModel()
        {
            var result = new Tournament();
            result.Name = Name;
            result.Place = Place;
            result.Date = Date;
            result.LeadingRefereeId = LeadingRefereeId;
            result.OrganizerId = OrganizerId;
            return result;
        }

        public void SetFromDbModel(Tournament tournament)
        {
            _name = tournament.Name;
            _place = tournament.Place;
            _date = tournament.Date;
            _leadingRefereeId = tournament.LeadingRefereeId;
            _organizerId = tournament.OrganizerId;
        }

        public void PopulateOrganizers(IEnumerable<Organizer> organizers)
        {
            foreach (var organizer in organizers)
            {
                AvailableOrganizers[organizer.Id] = organizer.Name;
            }
        }

        public void AddCompetition(string name, long numRounds)
        {
            Competitions.Add(new CompetitionViewModel() { Name = name, NumberOfRounds = numRounds });
        }

        public void DeleteCompetition(CompetitionViewModel competition)
        {
            Competitions.Remove(competition);
        }

        public void PopulateCompetitions(IEnumerable<Competition> competitions)
        {
            foreach (var competition in competitions)
            {
                AddCompetition(competition.Name, competition.NumberOfRounds);
            }
        }

        public IEnumerable<Competition> GetDbCompetitions()
        {
            foreach (var competition in Competitions)
            {
                yield return competition.ToDbModel();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
