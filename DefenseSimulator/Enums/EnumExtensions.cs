    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;

namespace DefenseSimulator.Enums
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(Enum value)
        {
            // Get the type of the enum
            Type type = value.GetType();

            // Get the field info for the enum member
            FieldInfo field = type.GetField(value.ToString());

            // Get the EnumMember attribute
            EnumMemberAttribute attribute = field.GetCustomAttributes(typeof(EnumMemberAttribute), false)
                                                .Cast<EnumMemberAttribute>()
                                                .FirstOrDefault();

            // Return the value from the attribute or the enum's name if the attribute is not present
            return attribute != null ? attribute.Value : value.ToString();
        }
    }

}
