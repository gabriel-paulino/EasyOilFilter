using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Filter: Product
    {
        public Filter(Guid id, string name, string manufacturer, decimal price, decimal stockQuantity, FilterType filterType)
        {
            AddNotifications(GetContract(name, manufacturer, price, stockQuantity, filterType));

            if (IsValid)
            {
                Id = id;
                Name = name;
                Manufacturer = manufacturer;
                Price = price;
                StockQuantity = stockQuantity;
                FilterType = filterType;
            }
        }

        public Filter(string name, string manufacturer, decimal price, decimal stockQuantity, FilterType filterType)
        {
            AddNotifications(GetContract(name, manufacturer, price, stockQuantity, filterType));

            if (IsValid)
            {
                Name = name;
                Manufacturer = manufacturer;
                Price = price;
                StockQuantity = stockQuantity;
                FilterType = filterType;
                Type = ProductType.Filter;
                UnitOfMeasurement = UoM.Unity;
            }
        }

        public string Name { get; private set; }
        public string Manufacturer { get; private set; }
        public FilterType FilterType { get; private set; }
        
        public void ChangePriceByAbsoluteValue(decimal absoluteValue)
        {
            Price += absoluteValue;
        }

        public void ChangePriceByPercentage(decimal percentage)
        {
            Price = (1 + percentage / 100) * Price;
        }

        private static Contract<Filter> GetContract(string name, string manufacturer, decimal price, decimal stockQuantity, FilterType type) =>
            new Contract<Filter>()
                .IsLowerThan(name, 100, "Name", "O código do filtro não pode ter mais do que 100 caracteres.")
                .IsGreaterThan(name, 2, "Name", "O código do filtro deve ter pelo menos 2 caracteres.")
                .IsLowerThan(manufacturer, 15, "Manufacturer", "O fabricante do filtro não pode ter mais do que 15 caracteres.")
                .IsGreaterThan(manufacturer, 2, "Manufacturer", "O fabricante do filtro deve ter pelo menos 2 caracteres.")
                .IsBetween((int)type, 0, 4, "Type", "O tipo do filtro deve ser informado.")
                .IsBetween(price, 1, 1000, "Price", "O preço do filtro deve ser pelo menos R$ 1 e não maior que R$ 1000.")
                .IsGreaterThan(stockQuantity, 0, "StockQuantity", "A quantidade em estoque do filtro deve ser maior que 0.");
    }
}