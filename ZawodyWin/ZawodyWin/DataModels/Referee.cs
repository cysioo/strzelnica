namespace ZawodyWin.DataModels
{
    public class Referee
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string Function { get; set; }
        public int Class { get; set; }
        public int TournamentId { get; set; }
    }
}
