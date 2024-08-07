using System.ComponentModel.DataAnnotations;

namespace DefenseSimulator.Models
{
    public class OriginThreat
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Distance (KM)")]
        public int Distanse { get; set; }
    }
}
