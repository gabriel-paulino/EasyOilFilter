using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public decimal StockQuantity { get; protected set; }
        public ProductType Type { get; protected set; }
        public UoM UnitOfMeasurement { get; protected set; }
        public string Viscosity { get; protected set; }
        public OilType OilType { get; protected set; }
        public string Manufacturer { get; protected set; }
        public FilterType FilterType { get; protected set; }
    }
}