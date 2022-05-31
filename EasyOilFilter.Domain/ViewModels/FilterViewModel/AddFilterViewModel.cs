using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel
{
    public class AddFilterViewModel
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string FilterType { get; set; }

        public static implicit operator Filter(AddFilterViewModel model) =>
            new(
                name: model.Name,
                manufacturer: model.Manufacturer,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                filterType: EnumUtility.GetEnumByDescription<FilterType>(model.FilterType)
                );
    }
}
