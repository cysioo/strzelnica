namespace ZawodyWin.ViewModels
{
    public class CompetitionViewModel
    {
        public string Name { get; set; }
        public long NumberOfRounds { get; set; }
        public string RoundsText => NumberOfRounds == 1 ? "1 seria" : $"{NumberOfRounds} serii";
    }
}
