using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZawodyWin.Pdf.DataModels
{
    public class CompetitionProtocol
    {
        public string Number { get; internal set; }
        public string Competition { get; internal set; }
        public string SeriesHeaderCells { get; internal set; }
        public IList<ContestantScoreRow> ScoreRows { get; } = new List<ContestantScoreRow>();
    }
}
