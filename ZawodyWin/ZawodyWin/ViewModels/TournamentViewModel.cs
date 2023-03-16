using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ZawodyWin.ViewModels
{
    public class TournamentViewModel : INotifyPropertyChanged
    {
        private string? _name;
        private DateTime? _date;
        private int? _organizerId;
        private string? _place;
        private int? _leadingRefereeId;

        public string? Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public DateTime? Date
        {
            get { return _date; }
            set { _date = value; OnPropertyChanged(); }
        }

        public int? OrganizerId
        {
            get { return _organizerId; }
            set { _organizerId = value; OnPropertyChanged(); }
        }

        public string? Place
        {
            get { return _place; }
            set { _place = value; OnPropertyChanged(); }
        }

        public int? LeadingRefereeId
        {
            get { return _leadingRefereeId; }
            set { _leadingRefereeId = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
