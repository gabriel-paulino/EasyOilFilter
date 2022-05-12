using EasyOilFilter.Domain.Extensions;

namespace EasyOilFilter.Domain.Shared.Utils
{
    public static class EnumUtility
    {
        public static IList<T> EnumToList<T>()
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T isn't an enumerated type");

            var list = new List<T>();
            var type = typeof(T);
            if (type != null)
            {
                Array enumValues = Enum.GetValues(type);
                foreach (T value in enumValues)
                    list.Add(value);
            }

            return list;
        }

        public static T GetEnumByDescription<T>(string description)
        {
            if (!typeof(T).IsEnum)
                throw new Exception("T isn't an enumerated type");

            var list = EnumToList<T>();

            foreach (T item in list)
            {
                if (((Enum)Enum.Parse(typeof(T), item.ToString())).GetDescription() == description)
                    return item;
            }

            throw new Exception("The description is invalid");
        }
    }
}