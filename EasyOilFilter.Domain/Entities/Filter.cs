using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Filter : Product
    {
        public Filter(Guid id, string name, string manufacturer, decimal defaultPrice, decimal stockQuantity, FilterType filterType)
        {
            Id = id;
            Name = name;
            Manufacturer = manufacturer;
            DefaultPrice = defaultPrice;
            StockQuantity = stockQuantity;
            FilterType = filterType;
        }

        public Filter(string name, string manufacturer, decimal defaultPrice, decimal stockQuantity, FilterType filterType)
        {
            AddNotifications(GetContract(name, manufacturer, defaultPrice, stockQuantity, filterType));

            if (IsValid)
            {
                Name = name;
                Manufacturer = manufacturer;
                DefaultPrice = defaultPrice;
                StockQuantity = stockQuantity;
                FilterType = filterType;
                Type = ProductType.Filter;
                DefaultUoM = UoM.Unity;
            }
        }

        public void ChangeDefaultPriceByAbsoluteValue(decimal absoluteValue)
        {
            DefaultPrice += absoluteValue;
        }

        public void ChangeDefaultPriceByPercentage(decimal percentage)
        {
            DefaultPrice = (1 + percentage / 100) * DefaultPrice;
        }

        private static Contract<Filter> GetContract(string name, string manufacturer, decimal defaultPrice, decimal stockQuantity, FilterType type) =>
            new Contract<Filter>()
                .IsLowerThan(name, 100, "Name", "O código do filtro não pode ter mais do que 100 caracteres.")
                .IsGreaterThan(name, 2, "Name", "O código do filtro deve ter pelo menos 2 caracteres.")
                .IsLowerThan(manufacturer, 30, "Manufacturer", "O fabricante do filtro não pode ter mais do que 30 caracteres.")
                .IsGreaterThan(manufacturer, 2, "Manufacturer", "O fabricante do filtro deve ter pelo menos 2 caracteres.")
                .IsBetween((int)type, 0, 12, "FilterType", "O tipo do filtro deve ser informado.")
                .IsBetween(defaultPrice, 1, 1000, "DefaultPrice", "O preço do filtro deve ser pelo menos R$ 1 e não maior que R$ 1000.")
                .IsGreaterThan(stockQuantity, 0, "StockQuantity", "A quantidade em estoque do filtro deve ser maior que 0.");
    }
}