using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel;

public record FilterViewModel
(
    Guid Id = default,
    string Name = default,
    string Manufacturer = default,
    decimal DefaultPrice = default,
    decimal StockQuantity = default,
    string FilterType = default
)
{
    public static implicit operator FilterViewModel(Filter filter) => new
        (

           Id: filter.Id,
           Name: filter.Name,
           Manufacturer: filter.Manufacturer,
           DefaultPrice: filter.DefaultPrice,
           StockQuantity: filter.StockQuantity,
           FilterType: filter.FilterType.GetDescription()
        );

    public static implicit operator Filter(FilterViewModel model) => new
        (
            id: model.Id,
            name: model.Name,
            manufacturer: model.Manufacturer,
            defaultPrice: model.DefaultPrice,
            stockQuantity: model.StockQuantity,
            filterType: EnumUtility.GetEnumByDescription<FilterType>(model.FilterType)
        );

    public static IEnumerable<FilterViewModel> MapMany(IEnumerable<Filter> filters) =>
        filters.Select(filter => (FilterViewModel)filter);
}