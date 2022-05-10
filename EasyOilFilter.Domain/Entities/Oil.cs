using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Oil : BaseEntity
    {
        public Oil(Guid id, string name, string viscosity, decimal price, OilType type, UoM unitOfMeasurement)
        {
            AddNotifications(GetContract(name, viscosity, price, type, unitOfMeasurement));

            if (IsValid)
            {
                Id = id;
                Name = name;
                Viscosity = viscosity;
                Price = price;
                Type = type;
                UnitOfMeasurement = unitOfMeasurement;
            }
        }

        public Oil(string name, string viscosity, decimal price, OilType type, UoM unitOfMeasurement)
        {
            AddNotifications(GetContract(name, viscosity, price, type, unitOfMeasurement));

            if (IsValid)
            {
                Name = name;
                Viscosity = viscosity;
                Price = price;
                Type = type;
                UnitOfMeasurement = unitOfMeasurement;
            }
        }

        public string? Name { get; set; }
        public string? Viscosity { get; set; }
        public decimal Price { get; set; }
        public OilType Type { get; set; }
        public UoM UnitOfMeasurement { get; set; }

        private static Contract<Oil> GetContract(string name, string viscosity, decimal price, OilType type, UoM unitOfMeasurement) =>
            new Contract<Oil>()
                .IsNullOrEmpty(name, "Name", "O nome do lubrificante deve estar preenchido.")
                .IsLowerThan(name, 100, "Name", "O nome do lubrificante não pode ter mais do que 100 caracteres.")
                .IsGreaterThan(name, 3, "Name", "O nome do lubrificante deve ter pelo menos 3 caracteres.")
                .IsNullOrEmpty(name, "Name", "A viscosidade do lubrificante deve estar preenchida.")
                .IsLowerThan(viscosity, 10, "Viscosity", "A viscosidade do lubrificante não pode ter mais do que 100 caracteres.")
                .IsGreaterThan(viscosity, 2, "Viscosity", "A viscosidade do lubrificante deve ter pelo menos 2 caracteres.")
                .IsBetween((int)type, 0, 14, "Type", "O tipo do lubrificante deve ser selecionado.")
                .IsBetween((int)unitOfMeasurement, 0, 9, "UnitOfMeasurement", "A unidade de medida do lubrificante deve ser selecionada.")
                .IsBetween(price, 1, 1000, "Price", "O preço do lubrificante deve ser pelo menos R$ 1 e não superior que R$ 1000.");
    }
}