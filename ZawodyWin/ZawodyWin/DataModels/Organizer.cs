using System.ComponentModel.DataAnnotations.Schema;

namespace ZawodyWin.DataModels
{
    [Table("Organizer")]
    public class Organizer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? License { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? LogoPath { get; set; }
    }
}
