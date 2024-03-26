using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Extensions;

namespace EasyOilFilter.Domain.ViewModels;

public record ProductViewModel
(
    string Id,
    string Name,
    decimal DefaultPrice,
    decimal AlternativePrice,
    decimal StockQuantity,
    string Type,
    string DefaultUoM,
    string AlternativeUoM,
    bool HasAlternative
)
{
    public static IEnumerable<ProductViewModel> MapMany(List<Product> products) =>
        products.Select(filter => (ProductViewModel)filter);

    public static implicit operator ProductViewModel(Product product) =>
         new
        (
            Id: product.Id.ToString(),
            Name: product.Name,
            DefaultPrice: product.DefaultPrice,
            AlternativePrice: product.AlternativePrice,
            StockQuantity: product.StockQuantity,
            Type: product.Type.GetDescription(),
            DefaultUoM: product.DefaultUoM.GetDescription(),
            AlternativeUoM: product.AlternativeUoM.GetDescription(),
            HasAlternative: product.HasAlternative
        );

}