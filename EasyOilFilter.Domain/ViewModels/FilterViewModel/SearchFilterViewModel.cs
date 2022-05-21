using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel
{
    public class SearchFilterViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public FilterType Type { get; set; } = FilterType.All;
    }
}