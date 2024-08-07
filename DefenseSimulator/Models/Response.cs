using DefenseSimulator.Enums;
using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }
        [Required]
        public int ThreatId { get; set; }
        public Threat? Threat { get; set; }
        [Required]
        public DateTime LaunchTime { get; set; }
        public DateTime? InterceptTime { get; set; }
        public int DefenseWeaponId { get; set; }
        public DefenseWeapon? DefenseWeapon { get; set; }
        public ResponseStatus status { get; set; }
    }
}
