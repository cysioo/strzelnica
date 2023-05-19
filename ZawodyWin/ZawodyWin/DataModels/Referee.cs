using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Referee")]
    public class Referee
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string? RefereeFunction { get; set; }
        public long? RefereeClass { get; set; }
        public long TournamentId { get; set; }
        public bool IsLeading { get; set; }
        public Person Person { get; set; }
        public Tournament Tournament { get; set; }
    }
}
