using DefenseSimulator.Enums;
using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public class AttackWeapon : Weapon
    {
       
        [Required]
        public AttackWeaponType Type { get; set; }

        public List<DefenseWeapon>? defenseWeapons { get; set; }
    }
}
