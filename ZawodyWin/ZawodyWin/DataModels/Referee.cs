namespace ZawodyWin.DataModels
{
    public class Referee
    {
        public long Id { get; set; }
        public long PersonId { get; set; }
        public string Function { get; set; }
        public int Class { get; set; }
        public long TournamentId { get; set; }
    }
}
