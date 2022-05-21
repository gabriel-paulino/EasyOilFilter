using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities.Base
{
    public abstract class Product : BaseEntity
    {
        public decimal Price { get; protected set; }
        public decimal StockQuantity { get; protected set; }
    }
}