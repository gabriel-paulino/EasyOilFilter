using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities
{
    public class SoldItemReport : BaseEntity
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }
}