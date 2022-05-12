using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel
{
    public class OilViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public decimal Price { get; set; }
        public decimal StockQuantity { get; set; }
        public string Type { get; set; }
        public string UnitOfMeasurement { get; set; }

        private static string GetOilTypeDescription(int type)
        {
            switch ((OilType)type)
            {
                case OilType.Mineral:
                    return "Mineral";
                case OilType.SemiSynthetic:
                    return "Semissintético";
                case OilType.Synthetic:
                    return "Sintético";
                case OilType.Transmission:
                    return "Transmissão";
                default:
                    return "Outro";
            }
        }

        private static OilType GetOilType(string description)
        {
            switch (description)
            {
                case "Mineral":
                    return OilType.Mineral;
                case "Semissintético":
                    return OilType.SemiSynthetic;
                case "Sintético":
                    return OilType.Synthetic;
                case "Transmissão":
                    return OilType.Transmission;
                default:
                    return OilType.Other;
            }
        }

        private static string GetUnitOfMeasurementDescription(int unitOfMeasurement)
        {
            switch ((UoM)unitOfMeasurement)
            {
                case UoM.Liter:
                    return "Litro";
                case UoM.Bucket:
                    return "Balde";
                case UoM.Unit:
                    return "Unidade";
                default:
                    return "Outro";
            }
        }

        private static UoM GetUnitOfMeasurement(string description)
        {
            switch (description)
            {
                case "Litro":
                    return UoM.Liter;
                case "Balde":
                    return UoM.Bucket;
                case "Unidade":
                    return UoM.Unit;
                default:
                    return UoM.Other;
            }
        }

        public static implicit operator OilViewModel(Oil oil) =>
           new()
           {
               Id = oil.Id,
               Name = oil.Name,
               Viscosity = oil.Viscosity,
               Price = oil.Price,
               StockQuantity = oil.StockQuantity,
               Type = GetOilTypeDescription((int)oil.Type),
               UnitOfMeasurement = GetUnitOfMeasurementDescription((int)oil.UnitOfMeasurement),
           };

        public static implicit operator Oil(OilViewModel model) =>
            new(
                id: model.Id,
                name: model.Name,
                viscosity: model.Viscosity,
                price: model.Price,
                stockQuantity: model.StockQuantity,
                type: GetOilType(model.Type),
                unitOfMeasurement: GetUnitOfMeasurement(model.UnitOfMeasurement)
                );

        public static IEnumerable<OilViewModel> MapMany(IEnumerable<Oil> oils) =>
            oils.Select(oil => (OilViewModel)oil);
    }
}
