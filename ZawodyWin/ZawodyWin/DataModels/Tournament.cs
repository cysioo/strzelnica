using System;

namespace ZawodyWin.DataModels
{

    public class Tournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public int? OrganizerId { get; set; }
        public string? Place { get; set; }
        public int? LeadingRefereeId { get; set; }
    }
}
