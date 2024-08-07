using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public class Arsenal
    {
        [Key]
        public int Id { get; set; }
        [Range(0,int.MaxValue)]
        public int Amount { get; set; }
        public int DefenseWeaponId { get; set; }
        public DefenseWeapon? DefenseWeapon { get; set; }
    }
}
