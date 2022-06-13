using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Oil : Product
    {
        public Oil(Guid id, string name, string viscosity, string api, decimal price, decimal stockQuantity, OilType oilType, UoM unitOfMeasurement)
        {
            AddNotifications(GetContract(name, viscosity, api, price, stockQuantity, oilType, unitOfMeasurement));

            if (IsValid)
            {
                Id = id;
                Name = name;
                Viscosity = viscosity;
                Api = api;
                Price = price;
                StockQuantity = stockQuantity;
                OilType = oilType;
                UnitOfMeasurement = unitOfMeasurement;
            }
        }

        public Oil(string name, string viscosity, string api, decimal price, decimal stockQuantity, OilType oilType, UoM unitOfMeasurement)
        {
            AddNotifications(GetContract(name, viscosity, api, price, stockQuantity, oilType, unitOfMeasurement));

            if (IsValid)
            {
                Name = name;
                Viscosity = viscosity;
                Api = api;
                Price = price;
                StockQuantity = stockQuantity;
                OilType = oilType;
                UnitOfMeasurement = unitOfMeasurement;
                Type = ProductType.Oil;
            }
        }

        public void ChangePriceByAbsoluteValue(decimal absoluteValue)
        {
            Price += absoluteValue;
        }

        public void ChangePriceByPercentage(decimal percentage)
        {
            Price = (1 + percentage / 100) * Price;
        }

        private static Contract<Oil> GetContract(string name, string viscosity, string api, decimal price, decimal stockQuantity, OilType type, UoM unitOfMeasurement) =>
            new Contract<Oil>()
                .IsLowerThan(name, 100, "Name", "O nome do lubrificante não pode ter mais do que 100 caracteres.")
                .IsGreaterThan(name, 2, "Name", "O nome do lubrificante deve ter pelo menos 2 caracteres.")
                .IsLowerThan(viscosity, 10, "Viscosity", "A viscosidade do lubrificante não pode ter mais do que 10 caracteres.")
                .IsLowerThan(api, 5, "Api", "A API do lubrificante não pode ter mais do que 5 caracteres.")
                .IsGreaterThan(viscosity, 2, "Viscosity", "A viscosidade do lubrificante deve ter pelo menos 2 caracteres.")
                .IsBetween((int)type, 1, 5, "Type", "O tipo do lubrificante deve ser informado.")
                .IsBetween((int)unitOfMeasurement, 0, 2, "UnitOfMeasurement", "A unidade de medida do lubrificante deve ser preenchida.")
                .IsBetween(price, 1, 1000, "Price", "O preço do lubrificante deve ser pelo menos R$ 1 e não maior que R$ 1000.")
                .IsGreaterThan(stockQuantity, 0, "StockQuantity", "A quantidade em estoque do lubrificante deve ser maior que 0.");
    }
}