using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Extensions;

namespace EasyOilFilter.Domain.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }
        public string UnitOfMeasurement { get; set; }
        public string Viscosity { get; set; }
        public string OilType { get; set; }
        public string Manufacturer { get; set; }
        public string FilterType { get; set; }

        public static IEnumerable<ProductViewModel> MapMany(IEnumerable<Product> filters) =>
            filters.Select(filter => (ProductViewModel)filter);

        public static implicit operator ProductViewModel(Product product) =>
            new()
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Price = product.DefaultPrice,
                StockQuantity = product.StockQuantity,
                Type = product.Type.GetDescription(),
                UnitOfMeasurement = product.DefaultUoM.GetDescription(),
                Viscosity = product?.Viscosity ?? string.Empty,
                OilType = product?.OilType.GetDescription() ?? string.Empty,
                Manufacturer = product?.Manufacturer ?? string.Empty,
                FilterType = product?.FilterType.GetDescription() ?? string.Empty,
            };
    }
}