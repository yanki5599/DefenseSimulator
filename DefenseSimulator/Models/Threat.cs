using System.ComponentModel.DataAnnotations;
using DefenseSimulator.Enums;
namespace DefenseSimulator.Models
{
    public class Threat
    {
        [Key]
        public int ThreatId { get; set; }
        [Required]
        public int OriginThreatId { get; set; }
        
        public OriginThreat? OriginThreat { get; set; }
        [DisplayFormat(NullDisplayText ="Not Launched Yet")]
        public DateTime? LaunchTime { get; set; } = null;
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }
        public bool IsActive { get; set; } = false;
        public string? ActiveID { get; set; }
        public bool IsInterceptedOrExploded { get; set; } = false ;
        public int AttackWeaponId { get; set; }
        public AttackWeapon? AttackWeapon { get; set; }
      
    }
}
