using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZawodyWin.Pdf.DataModels
{
    public class ContestantScoreRow
    {
        public string Rank { get; internal set; }
        public string ClubName { get; internal set; }
        public string Name { get; internal set; }
        public string Surname { get; internal set; }
        public string SeriesCells { get; internal set; }
        public string TotalScore { get; internal set; }
        public string Notes { get; internal set; }
    }
}
