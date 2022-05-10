using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class AddOilViewModel
    {
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public int UnitOfMeasurement { get; set; }

        public static implicit operator Oil(AddOilViewModel model) =>
            new(
                name: model.Name,
                viscosity: model.Viscosity,
                price: model.Price,
                type: (OilType)model.Type,
                unitOfMeasurement: (UoM)model.UnitOfMeasurement
                );
    }
}