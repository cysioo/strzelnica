using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ShootingClubViewModel : INotifyPropertyChanged
    {
        private string _name;
        private string? _shortName;
        private string? _license;
        private string? _addressLine1;
        private string? _addressLine2;
        private string? _logoPath;
        private string? _logoPathToUpload;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string? ShortName
        {
            get { return _shortName; }
            set { _shortName = value; OnPropertyChanged(nameof(ShortName)); }
        }

        public string? License
        {
            get { return _license; }
            set { _license = value; OnPropertyChanged(nameof(License)); }
        }

        public string? AddressLine1
        {
            get { return _addressLine1; }
            set { _addressLine1 = value; OnPropertyChanged(nameof(AddressLine1)); }
        }

        public string? AddressLine2
        {
            get { return _addressLine2; }
            set { _addressLine2 = value; OnPropertyChanged(nameof(AddressLine2)); }
        }

        public string? LogoPathExisting
        {
            get { return _logoPath; }
            set { _logoPath = value; OnPropertyChanged(nameof(LogoPathExisting)); }
        }

        public string? LogoPathToUpload
        {
            get { return _logoPathToUpload; }
            set { _logoPathToUpload = value; OnPropertyChanged(nameof(LogoPathToUpload)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ShootingClub ToDbModel()
        {
            return new ShootingClub
            {
                Name = Name,
                ShortName = ShortName,
                License = License,
                AddressLine1 = AddressLine1,
                AddressLine2 = AddressLine2,
                LogoPath = LogoPathExisting
            };
        }

        public void SetFromDbModel(ShootingClub tournament)
        {
            _name = tournament.Name;
            _shortName = tournament.ShortName;
            _license = tournament.License;
            _addressLine1 = tournament.AddressLine1;
            _addressLine2 = tournament.AddressLine2;
            _logoPath = tournament.LogoPath;
        }
    }
}
