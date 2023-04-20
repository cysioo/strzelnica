using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class OrganizerViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string _license;
        private string _addressLine1;
        private string _addressLine2;
        private string _logoPath;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string License
        {
            get { return _license; }
            set { _license = value; OnPropertyChanged(nameof(License)); }
        }

        public string AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; OnPropertyChanged(nameof(AddressLine1)); }
        }

        public string AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged(nameof(AddressLine2)); }
        }

        public string LogoPath
        {
            get { return _logoPath; }
            set { _logoPath = value; OnPropertyChanged(nameof(LogoPath)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Organizer ToDbModel()
        {
            return new Organizer
            {
                Name = Name,
                License = License,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                LogoPath = LogoPath
            };
        }

        public void SetFromDbModel(Organizer tournament)
        {
            _name = tournament.Name;
            _license = tournament.License;
            _addressLine1 = tournament.AddressLine1;
            _addressLine2 = tournament.AddressLine2;
            _logoPath = tournament.LogoPath;
        }
    }
}
