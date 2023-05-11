using System.Collections.ObjectModel;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantsCompetitionViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<long> Scores { get; set; }

        public void SetFromDbModel(Competition competition)
        {
            Id = competition.Id;
            Name = competition.Name;
            Scores = new ObservableCollection<long>();
            for (int i = 0; i < competition.NumberOfRounds; i++)
            {
                Scores.Add(0);
            }
        }
    }
}
