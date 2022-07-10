using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities.Reports
{
    public class SoldItemReport : BaseEntity
    {
        public SoldItemReport(string name, decimal quantity)
        {
            Name = name;
            Quantity = quantity;
        }

        public string Name { get; private set; }
        public decimal Quantity { get; private set; }
    }
}