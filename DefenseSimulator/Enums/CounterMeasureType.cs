using System.Runtime.Serialization;

namespace DefenseSimulator.Enums
{
    public enum CounterMeasureType
    {
        [EnumMember(Value = "כיפת ברזל")]           IronDome            ,
        [EnumMember(Value = "שבש אלקטרוני")]        ElectronicJamming   ,
        [EnumMember(Value = "מערכת נגד אווירית")]   AntiAirSystem       ,
        [EnumMember(Value = "טיל יירוט")]           InterceptorMissile  ,
    }
}
