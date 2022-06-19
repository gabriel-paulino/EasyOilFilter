using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Extensions;

namespace EasyOilFilter.Domain.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal AlternativePrice { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }
        public string DefaultUoM { get; set; }
        public string AlternativeUoM { get; set; }
        public string Viscosity { get; set; }
        public string OilType { get; set; }
        public string Manufacturer { get; set; }
        public string FilterType { get; set; }
        public bool HasAlternative { get; set; }

        public static IEnumerable<ProductViewModel> MapMany(IEnumerable<Product> filters) =>
            filters.Select(filter => (ProductViewModel)filter);

        public static implicit operator ProductViewModel(Product product) =>
            new()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                DefaultPrice = product.DefaultPrice,
                AlternativePrice = product.AlternativePrice,
                StockQuantity = product.StockQuantity,
                Type = product.Type.GetDescription(),
                DefaultUoM = product.DefaultUoM.GetDescription(),
                AlternativeUoM = product.AlternativeUoM.GetDescription(),
                Viscosity = product?.Viscosity ?? string.Empty,
                OilType = product?.OilType.GetDescription() ?? string.Empty,
                Manufacturer = product?.Manufacturer ?? string.Empty,
                FilterType = product?.FilterType.GetDescription() ?? string.Empty,
                HasAlternative = product.HasAlternative
            };
    }
}