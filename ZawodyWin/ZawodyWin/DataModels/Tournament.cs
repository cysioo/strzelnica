using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{

    [Table("Tournament")]
    public class Tournament
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public long? OrganizerId { get; set; }
        public string? City { get; set; }
        public string? FullAddress { get; set; }

        public ShootingClub Organizer { get; set; }
        public ICollection<Competition> Competitions { get; set; }
        public ICollection<Referee> Referees { get; set; }
    }
}
