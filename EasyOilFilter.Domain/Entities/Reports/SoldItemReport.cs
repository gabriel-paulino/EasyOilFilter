using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities.Reports
{
    public class SoldItemReport : BaseEntity
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
    }
}