using System.Collections.Generic;

namespace ZawodyWin.Models
{
    public class Contestant
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string ClubName { get; set; }
        public int CompetitionId { get; set; }
        public IList<int> Scores { get; private set; } = new List<int>();
        public string Notes { get; set; }
    }
}
