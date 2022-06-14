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
        public string Api { get; set; }
        public decimal DefaultPrice { get; set; }
        public decimal AlternativePrice { get; set; }
        public decimal StockQuantity { get; set; }
        public string OilType { get; set; }
        public string DefaultUoM { get; set; }
        public string AlternativeUoM { get; set; }
        public bool HasAlternative { get; set; }

        public static implicit operator OilViewModel(Oil oil) =>
           new()
           {
               Id = oil.Id,
               Name = oil.Name,
               Viscosity = oil.Viscosity,
               Api = oil.Api,
               DefaultPrice = oil.DefaultPrice,
               AlternativePrice = oil?.AlternativePrice ?? 0.0m,
               StockQuantity = oil.StockQuantity,
               OilType = oil.OilType.GetDescription(),
               DefaultUoM = oil.DefaultUoM.GetDescription(),
               AlternativeUoM = oil?.AlternativeUoM.GetDescription() ?? string.Empty,
               HasAlternative = oil?.HasAlternative ?? false,
           };

        public static implicit operator Oil(OilViewModel model) =>
            new(
                id: model.Id,
                name: model.Name,
                viscosity: model.Viscosity,
                api: model.Api,
                defaultPrice: model.DefaultPrice,
                alternativePrice: model.AlternativePrice,
                stockQuantity: model.StockQuantity,
                oilType: EnumUtility.GetEnumByDescription<OilType>(model.OilType),
                defaultUoM: EnumUtility.GetEnumByDescription<UoM>(model.DefaultUoM),
                alternativeUoM: EnumUtility.GetEnumByDescription<UoM>(model.AlternativeUoM),
                hasAlternative: model?.HasAlternative ?? false
                );

        public static IEnumerable<OilViewModel> MapMany(IEnumerable<Oil> oils) =>
            oils.Select(oil => (OilViewModel)oil);
    }
}