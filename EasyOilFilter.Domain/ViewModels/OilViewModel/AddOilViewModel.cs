using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.OilViewModel;

public class AddOilViewModel
{
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

    public static implicit operator Oil(AddOilViewModel model) =>
        new(
            name: model.Name,
            viscosity: model.Viscosity,
            api: model.Api,
            defaultPrice: model.DefaultPrice,
            alternativePrice: model.AlternativePrice,
            stockQuantity: model.StockQuantity,
            oilType: EnumUtility.GetEnumByDescription<OilType>(model.OilType),
            defaultUoM: EnumUtility.GetEnumByDescription<UoM>(model.DefaultUoM),
            alternativeUoM: EnumUtility.GetEnumByDescription<UoM>(model.AlternativeUoM),
            hasAlternative: model.HasAlternative
            );
}