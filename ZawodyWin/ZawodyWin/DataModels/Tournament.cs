using System;

namespace ZawodyWin.DataModels
{

    public class Tournament
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public long? OrganizerId { get; set; }
        public string? Place { get; set; }
        public long? LeadingRefereeId { get; set; }
    }
}
