using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantsCompetitionViewModel : INotifyPropertyChanged
    {
        private long _totalScore;

        public long Id { get; set; }
        public string Name { get; set; }
        public long TotalScore
        {
            get { return _totalScore; }
            set
            {
                _totalScore = value;
                OnPropertyChanged(nameof(TotalScore));
            }
        }

        public ObservableCollection<ScoreViewModel> Scores { get; set; } = new ObservableCollection<ScoreViewModel>();

        public ContestantsCompetitionViewModel()
        {
            Scores.CollectionChanged += Scores_CollectionChanged;
        }

        private void Scores_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            TotalScore = Scores.Sum(x => x.Points);
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ScoreViewModel item in e.OldItems)
                {
                    //Removed items
                    item.PropertyChanged -= ScoreViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ScoreViewModel item in e.NewItems)
                {
                    //Added items
                    item.PropertyChanged += ScoreViewModelPropertyChanged;
                }
            }
        }

        public void ScoreViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TotalScore = Scores.Sum(x => x.Points);
        }

        public void SetFromDbModel(Competition competition)
        {
            Id = competition.Id;
            Name = competition.Name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
