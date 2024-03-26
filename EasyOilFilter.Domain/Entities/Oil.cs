using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities;

public class Oil : Product
{

    public string Viscosity { get; }
    public string Api { get; }
    public OilType OilType { get; }

    public Oil
    (
        Guid id,
        string name,
        string viscosity,
        string api,
        decimal defaultPrice,
        decimal alternativePrice,
        decimal stockQuantity,
        OilType oilType,
        UoM defaultUoM,
        UoM alternativeUoM,
        bool hasAlternative
    )
    {
        Id = id;
        Name = name;
        Viscosity = viscosity;
        Api = api;
        DefaultPrice = defaultPrice;
        DefaultUoM = defaultUoM;
        AlternativePrice = alternativePrice;
        AlternativeUoM = alternativeUoM;
        StockQuantity = stockQuantity;
        OilType = oilType;
        HasAlternative = hasAlternative;
    }

    public Oil(string name, string viscosity, string api, decimal defaultPrice, decimal alternativePrice, decimal stockQuantity, OilType oilType, UoM defaultUoM, UoM alternativeUoM, bool hasAlternative)
    {
        AddNotifications(GetContract(name, viscosity, api, defaultPrice, stockQuantity, oilType, defaultUoM));

        if (IsValid)
        {
            Name = name;
            Viscosity = viscosity;
            Api = api;
            DefaultPrice = defaultPrice;
            AlternativePrice = alternativePrice;
            StockQuantity = stockQuantity;
            OilType = oilType;
            DefaultUoM = defaultUoM;
            AlternativeUoM = alternativeUoM;
            Type = ProductType.Oil;
            HasAlternative = hasAlternative;
        }
    }

    public void ChangeDefaultPriceByAbsoluteValue(decimal absoluteValue) => DefaultPrice += absoluteValue;

    public void ChangeDefaultPriceByPercentage(decimal percentage) => DefaultPrice = (1 + percentage / 100) * DefaultPrice;

    private static Contract<Oil> GetContract(string name, string viscosity, string api, decimal defaultPrice, decimal stockQuantity, OilType type, UoM defaultUoM) =>
        new Contract<Oil>()
            .IsLowerThan(name, 100, "Name", "O nome do lubrificante não pode ter mais do que 100 caracteres.")
            .IsGreaterThan(name, 2, "Name", "O nome do lubrificante deve ter pelo menos 2 caracteres.")
            .IsLowerThan(viscosity, 10, "Viscosity", "A viscosidade do lubrificante não pode ter mais do que 10 caracteres.")
            .IsLowerThan(api, 10, "Api", "A API do lubrificante não pode ter mais do que 10 caracteres.")
            .IsGreaterThan(viscosity, 2, "Viscosity", "A viscosidade do lubrificante deve ter pelo menos 2 caracteres.")
            .IsBetween((int)type, 1, 5, "Type", "O tipo do lubrificante deve ser informado.")
            .IsBetween((int)defaultUoM, 0, 2, "DefaultUoM", "A embalagem padrão do lubrificante deve ser preenchida.")
            .IsBetween(defaultPrice, 1, 1000, "DefaultPrice", "O preço do lubrificante deve ser pelo menos R$ 1 e não maior que R$ 1000.")
            .IsGreaterThan(stockQuantity, 0, "StockQuantity", "A quantidade em estoque do lubrificante deve ser maior que 0.");
}