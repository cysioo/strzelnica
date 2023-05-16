using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantsCompetitionViewModel : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TotalScore { get; set; }
        public ObservableCollection<ScoreViewModel> Scores { get; set; } = new ObservableCollection<ScoreViewModel>();

        public ContestantsCompetitionViewModel()
        {
            Scores.CollectionChanged += Scores_CollectionChanged;
        }

        public void SetFromDbModel(Competition competition)
        {
            Id = competition.Id;
            Name = competition.Name;
            //for (int i = 0; i < competition.NumberOfRounds; i++)
            //{
            //    Scores.Add(0);
            //}
        }

        private void Scores_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (var item in e.OldItems)
                {
                    //Removed items
                    //item.PropertyChanged -= EntityViewModelPropertyChanged;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    //Added items
                    //item.PropertyChanged += EntityViewModelPropertyChanged;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
