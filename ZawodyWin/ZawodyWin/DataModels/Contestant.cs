using System.Collections.Generic;

namespace ZawodyWin.DataModels
{
    public class Contestant
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string ClubName { get; set; }
        public long CompetitionId { get; set; }
        public IList<int> Scores { get; private set; } = new List<int>();
        public string Notes { get; set; }
    }
}
