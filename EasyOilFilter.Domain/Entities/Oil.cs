using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Oil : BaseEntity
    {
        public string Name { get; set; }
        public string Viscosity { get; set; }
        public decimal Price { get; set; }
        public OilType Type { get; set; }
        public UoM UnitOfMeasurement { get; set; }
    }
}