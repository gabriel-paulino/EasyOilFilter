using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel
{
    public class FilterViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal StockQuantity { get; set; }
        public string FilterType { get; set; }

        public static implicit operator FilterViewModel(Filter filter) =>
           new()
           {
               Id = filter.Id,
               Name = filter.Name,
               Manufacturer = filter.Manufacturer,
               DefaultPrice = filter.DefaultPrice,
               StockQuantity = filter.StockQuantity,
               FilterType = filter.FilterType.GetDescription(),
           };

        public static implicit operator Filter(FilterViewModel model) =>
            new(
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
}