using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.FilterViewModel
{
    public class AddFilterViewModel
    {
        public string Code { get; set; }
        public string Manufacturer { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }

        public static implicit operator Filter(AddFilterViewModel model) =>
            new(
                code: model.Code,
                manufacturer: model.Manufacturer,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                type: EnumUtility.GetEnumByDescription<FilterType>(model.Type)
                );
    }
}
