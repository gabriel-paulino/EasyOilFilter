using System.ComponentModel;

namespace EasyOilFilter.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum item)
        {
            var type = item.GetType();
            var field = type.GetField(item.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes.Length > 0)
                return attributes[0].Description;

            return string.Empty;
        }
    }
}