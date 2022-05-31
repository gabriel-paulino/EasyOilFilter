using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class OilViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string OilType { get; set; }
        public string UnitOfMeasurement { get; set; }

        public static implicit operator OilViewModel(Oil oil) =>
           new()
           {
               Id = oil.Id,
               Name = oil.Name,
               Viscosity = oil.Viscosity,
               Price = oil.Price,
               StockQuantity = oil.StockQuantity,
               OilType = oil.OilType.GetDescription(),
               UnitOfMeasurement = oil.UnitOfMeasurement.GetDescription(),
           };

        public static implicit operator Oil(OilViewModel model) =>
            new(
                id: model.Id,
                name: model.Name,
                viscosity: model.Viscosity,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                oilType: EnumUtility.GetEnumByDescription<OilType>(model.OilType),
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement)
                );

        public static IEnumerable<OilViewModel> MapMany(IEnumerable<Oil> oils) =>
            oils.Select(oil => (OilViewModel)oil);
    }
}