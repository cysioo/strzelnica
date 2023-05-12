using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using ZawodyWin.DataModels;
using System.Linq;

namespace ZawodyWin.ViewModels
{
    public class ContestantListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ContestantViewModel> _contestants;
        private ContestantViewModel _selectedContestant;
        private bool _isEditing;

        public ObservableCollection<ContestantViewModel> Contestants
        {
            get { return _contestants; }
            set { _contestants = value; OnPropertyChanged(); }
        }

        //public ContestantViewModel SelectedContestant
        //{
        //    get { return _selectedContestant; }
        //    set { _selectedContestant = value; OnPropertyChanged(); }
        //}

        //public bool IsEditing
        //{
        //    get { return _isEditing; }
        //    set { _isEditing = value; OnPropertyChanged(); }
        //}

        //public ICommand EditCommand { get; private set; }
        //public ICommand SaveCommand { get; private set; }

        public ContestantListViewModel()
        {
            Contestants = new ObservableCollection<ContestantViewModel>();
            
            Contestants.Add(new ContestantViewModel { Name = "John", Surname = "Doe", 
                Competitions = new ObservableCollection<ContestantsCompetitionViewModel> { 
                    new ContestantsCompetitionViewModel { Name = "Math", Scores = new ObservableCollection<long> { 10, 20, 30 } }, 
                    new ContestantsCompetitionViewModel { Name = "Science", Scores = new ObservableCollection<long> { 40, 50, 60 } } } });

            //EditCommand = new RelayCommand<ContestantViewModel>(EditContestant);
            //SaveCommand = new RelayCommand<ContestantViewModel>(SaveContestant);
        }

        //private void EditContestant(ContestantViewModel contestant)
        //{
        //    SelectedContestant = contestant;
        //    IsEditing = true;
        //}

        //private void SaveContestant(Contestant parameter)
        //{
        //    SelectedContestant = null;
        //    IsEditing = false;
        //}

        public bool DoesContestantExist(Person person)
        {
            return Contestants.Any(x => x.PersonId == person.Id);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
