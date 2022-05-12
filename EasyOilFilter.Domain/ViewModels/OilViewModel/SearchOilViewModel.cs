namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class SearchOilViewModel
    {
        public string Name { get; set; } = string.Empty;
        public string Viscosity { get; set; } = string.Empty;
        public int Type { get; set; }
    }
}