using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public abstract class Weapon
    {
        [Key]
        public int Id { get; set; }
        public int Speed { get; set; }
        public int Range { get; set; }
    }
}
