using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class SaleItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UoM UnitOfMeasurementni { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }
    }
}