using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantsCompetitionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int TotalScore { get; set; }
        public ObservableCollection<ScoreViewModel> Scores { get; set; } = new ObservableCollection<ScoreViewModel>();

        public void SetFromDbModel(Competition competition)
        {
            Id = competition.Id;
            Name = competition.Name;
        }
    }
}
