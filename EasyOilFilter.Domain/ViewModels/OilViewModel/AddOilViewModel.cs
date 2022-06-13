using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class AddOilViewModel
    {
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public string Api { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string OilType { get; set; }
        public string UnitOfMeasurement { get; set; }

        public static implicit operator Oil(AddOilViewModel model) =>
            new(
                name: model.Name,
                viscosity: model.Viscosity,
                api: model.Api,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                oilType: EnumUtility.GetEnumByDescription<OilType>(model.OilType),
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement)
                );
    }
}