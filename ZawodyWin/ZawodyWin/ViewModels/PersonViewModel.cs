using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class PersonViewModel
    {
        private string _name;
        private string _surname;
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
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Person ToDbModel()
        {
            return new Person
            {
                Name = Name,
                Surname = Surname
            };
        }

        public void SetFromDbModel(Person person)
        {
            _name = person.Name;
            _surname = person.Surname;
        }
    }
}
