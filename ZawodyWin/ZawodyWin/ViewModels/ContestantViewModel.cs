using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZawodyWin.DataModels;

namespace ZawodyWin.ViewModels
{
    public class ContestantViewModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? ClubName { get; set; }
        public string? Notes { get; set; }
        public ObservableCollection<ContestantsCompetitionViewModel> Competitions { get; set; }

        public void SetFromDbModel(Contestant contestant, Person person)
        {
            Id = contestant.Id;
            Name = person.Name;
            Surname = person.Surname;
            ClubName = contestant.ClubName;
            Notes = contestant.Notes;
        }
    }
}