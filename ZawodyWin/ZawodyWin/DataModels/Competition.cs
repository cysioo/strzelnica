namespace ZawodyWin.DataModels
{
    public class Competition
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long NumberOfRounds { get; set; }
        public long TournamentId { get; set; }
    }
}
