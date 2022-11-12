using System.Reflection.Metadata.Ecma335;

namespace EasyOilFilter.Domain.Extensions
{
    public static class StringExtension
    {
        public static string FixTextToManageDataBaseResult(this string text, bool allowWithSpaces = true) =>
            allowWithSpaces
                ? text.ToUpper()
                : text.Replace(" ", string.Empty).ToUpper();

        public static List<string> GetListText(this string text) =>
            new List<string>() { text };
    }
}