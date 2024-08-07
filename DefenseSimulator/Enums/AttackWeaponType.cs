using System.Runtime.Serialization;

namespace DefenseSimulator.Enums
{
    public enum AttackWeaponType
    {
        [EnumMember(Value = "טיל בליסטי")]          BallisticMissile,
        [EnumMember(Value = "טיל")]                 Missile,
        [EnumMember(Value = "כטבם")]                Rocket,
        
    }



}

