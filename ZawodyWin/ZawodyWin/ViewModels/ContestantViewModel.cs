using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantViewModel: INotifyPropertyChanged
    {
        private string _name;
        private string _surname;
        private string _clubName;
        private string _notes;
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged(nameof(Surname)); }
        }
        public string? ClubName
        {
            get { return _clubName; }
            set { _clubName = value; OnPropertyChanged(nameof(ClubName)); }
        }

        public string? Notes
        {
            get { return _notes; }
            set { _notes = value; OnPropertyChanged(nameof(Notes)); }
        }
        public ObservableCollection<ContestantsCompetitionViewModel> Competitions { get; set; }

        public void SetFromDbModel(Contestant contestant, Person person)
        {
            Id = contestant.Id;
            Name = person.Name;
            Surname = person.Surname;
            ClubName = contestant.ClubName;
            Notes = contestant.Notes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}