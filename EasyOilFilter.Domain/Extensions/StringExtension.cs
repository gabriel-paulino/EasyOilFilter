namespace EasyOilFilter.Domain.Extensions
{
    public static class StringExtension
    {
        public static string FixTextToManageDataBaseResult(this string text, bool allowWithSpaces = true) =>
            allowWithSpaces 
                ? text.ToUpper()
                : text.Replace(" ", string.Empty).ToUpper();
    }
}