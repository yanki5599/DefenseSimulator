using DefenseSimulator.Enums;
using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public class DefenseWeapon : Weapon
    {
        [Required]
        public CounterMeasureType Type { get; set; }
        public List<AttackWeapon>? VulnerableWeapons { get; set; }
    }
}
