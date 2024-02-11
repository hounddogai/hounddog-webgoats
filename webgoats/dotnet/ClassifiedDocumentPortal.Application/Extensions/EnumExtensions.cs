using System.ComponentModel;

namespace ClassifiedDocumentPortal.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumValue) where T : Enum
        {
            var type = typeof(T);
            var member = type.GetMember(enumValue.ToString());
            var attributes = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            var description = ((DescriptionAttribute)attributes[0]).Description;

            return description;
        }

        public static T ConvertFromDescription<T>(string description)
        {
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var field = typeof(T).GetField(value.ToString());

                if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        return (T)value;
                    }
                }
                else if (value.ToString() == description)
                {
                    return (T)value;
                }
            }

            throw new ArgumentException($"'{description}' is not a valid value of {typeof(T).Name}.");
        }
    }
}
