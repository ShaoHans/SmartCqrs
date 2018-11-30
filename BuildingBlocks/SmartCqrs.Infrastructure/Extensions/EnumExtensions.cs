using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        public static string ToDescription<TEnum>(this TEnum value) where TEnum : struct
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static int ToInt32<TEnum>(this TEnum value) where TEnum : struct
        {
            return Convert.ToInt32(value);
        }
    }
}
