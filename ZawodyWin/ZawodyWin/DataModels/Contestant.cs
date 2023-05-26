using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Contestant")]
    public class Contestant
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string? ClubName { get; set; }
        public long CompetitionId { get; set; }
        public string? Notes { get; set; }
        public Person Person { get; set; }
        public Competition Competition { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}
