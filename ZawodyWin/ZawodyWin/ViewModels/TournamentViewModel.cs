using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class TournamentViewModel : INotifyPropertyChanged
    {
        private string _name;
        private DateTime? _date;
        private long? _organizerId;
        private string? _place;
        private long? _leadingRefereeId;

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

        public string? Place
        {
            get { return _place; }
            set { _place = value; OnPropertyChanged(); }
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
