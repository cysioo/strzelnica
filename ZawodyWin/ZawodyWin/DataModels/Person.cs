using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Person")]
    public class Person
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? ClubName { get; set; }
        public ICollection<Contestant> Contestants { get; set; }
        public ICollection<Referee> Referees { get; set; }
    }
}
