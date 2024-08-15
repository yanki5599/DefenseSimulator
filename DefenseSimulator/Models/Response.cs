using DefenseSimulator.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public DateTime LaunchTime { get; set; } = DateTime.Now;
        public DateTime? InterceptTime { get; set; }
        public int DefenseWeaponId { get; set; }
        public DefenseWeapon? DefenseWeapon { get; set; }
        public ResponseStatus status { get; set; }


        [NotMapped]
        public DateTime KaboomboomTime
        {
            get
            {
                
                var DistanceFromThreat = Threat.OriginThreat.Distanse - Threat.TravledDistance;

                var threatSpeed = Threat.AttackWeapon.Speed;

                var defenseSpeed = DefenseWeapon.Speed;

                var relativeSpeed = threatSpeed + defenseSpeed;


                var interceptTimeInSeconds = DistanceFromThreat / relativeSpeed;

                return LaunchTime.AddSeconds(interceptTimeInSeconds);
            }
        }

    }
}
