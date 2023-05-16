using Domain.Enums;

namespace Utils {
    public static class EnumUtils {
        public static string? GetString(this Enum value) {
            var type = value.GetType();
            var name = Enum.GetName(type, value);
            var field = type.GetField(name!);
            return Attribute.GetCustomAttribute(field!, typeof(StringValueAttribute)) is StringValueAttribute attribute ? attribute.Value : name;
        }
    }
}
