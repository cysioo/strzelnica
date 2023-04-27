using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Competition")]
    public class Competition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long NumberOfRounds { get; set; }
        public long TournamentId { get; set; }
    }
}
