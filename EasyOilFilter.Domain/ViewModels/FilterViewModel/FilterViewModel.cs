using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel
{
    public class FilterViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }

        public static implicit operator FilterViewModel(Filter filter) =>
           new()
           {
               Id = filter.Id,
               Code = filter.Code,
               Manufacturer = filter.Manufacturer,
               Price = filter.Price,
               StockQuantity = filter.StockQuantity,
               Type = filter.Type.GetDescription(),
           };

        public static implicit operator Filter(FilterViewModel model) =>
            new(
                id: model.Id,
                code: model.Code,
                manufacturer: model.Manufacturer,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                type: EnumUtility.GetEnumByDescription<FilterType>(model.Type)
                );

        public static IEnumerable<FilterViewModel> MapMany(IEnumerable<Filter> filters) =>
            filters.Select(filter => (FilterViewModel)filter);
    }
}