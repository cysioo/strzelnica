using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantListViewModel
    {
        private ObservableCollection<ContestantViewModel> _contestants;

        public ObservableCollection<ContestantViewModel> Contestants
        {
            get { return _contestants; }
            set { _contestants = value; }
        }

        public ContestantListViewModel()
        {
            Contestants = new ObservableCollection<ContestantViewModel>();
        }

        public bool DoesContestantExist(Person person)
        {
            return Contestants.Any(x => x.PersonId == person.Id);
        }
    }
}
