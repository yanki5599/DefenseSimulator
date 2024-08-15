using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public ThreatStatus ThreatStatus { get; set; }
        public int AttackWeaponId { get; set; }
        public AttackWeapon? AttackWeapon { get; set; }



        [NotMapped]
        public bool IsActive { get { return ThreatStatus == ThreatStatus.Active; } }

        [NotMapped]
        ///
        /// remember to include <param>OriginThreat<param> and <param>AttackWeapon<param> before calling
        /// 
        public DateTime KaBoomTime
        {
            get
            {
                var distance = OriginThreat.Distanse;
                var speed = AttackWeapon.Speed;
                return LaunchTime.Value.AddSeconds( distance / speed);

            }
        }

        [NotMapped]
        public int TravledDistance
        {
            get
            {
                if(!LaunchTime.HasValue) return 0;
                return AttackWeapon.Speed * (DateTime.Now - LaunchTime.Value).Seconds;
            }
        }


      
    }
}
