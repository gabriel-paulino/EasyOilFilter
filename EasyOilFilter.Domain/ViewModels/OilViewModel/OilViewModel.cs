using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class OilViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Viscosity { get; set; }
        public decimal Price { get; set; }
        public int Type { get; set; }
        public int UnitOfMeasurement { get; set; }

        public static implicit operator OilViewModel(Oil oil) =>
           new()
           {
               Id = oil.Id,
               Name = oil.Name,
               Viscosity = oil.Viscosity,
               Price = oil.Price,
               Type = (int)oil.Type,
               UnitOfMeasurement = (int)oil.UnitOfMeasurement,
           };

        public static implicit operator Oil(OilViewModel model) =>
            new(
                id: model.Id,
                name: model.Name ?? string.Empty,
                viscosity: model.Viscosity ?? string.Empty,
                price: model.Price,
                type: (OilType)model.Type,
                unitOfMeasurement: (UoM)model.UnitOfMeasurement
                );

        public static IEnumerable<OilViewModel> MapMany(IEnumerable<Oil> oils) =>      
            oils.Select(oil => (OilViewModel)oil);            
    }
}
