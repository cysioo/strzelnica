using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Score")]
    public class Score
    {
        public long Id {  get; set; }
        public long ContestantId { get; set; }
        public long CompetitionId { get; set; }
        public long Round { get; set; }
        public long Points { get; set; }
        public Competition Competition { get; set; }
        public Contestant Contestant { get; set; }
    }
}
