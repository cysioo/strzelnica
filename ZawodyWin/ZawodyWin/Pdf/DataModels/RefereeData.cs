using System.Collections.Generic;

namespace ZawodyWin.Pdf.DataModels
{
    public class RefereeData
    {
        public static Dictionary<long, string> RomanNumberLookup = new Dictionary<long, string>
        {
            { 1, "I" }, { 2, "II" }, { 3,  "III" }
        };

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Class { get; set; }
    }
}
