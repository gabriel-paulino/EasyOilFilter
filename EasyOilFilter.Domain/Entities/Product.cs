using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; protected set; }
        public decimal DefaultPrice { get; protected set; }
        public decimal AlternativePrice { get; protected set; }
        public decimal StockQuantity { get; protected set; }
        public ProductType Type { get; protected set; }
        public UoM DefaultUoM { get; protected set; }
        public UoM AlternativeUoM { get; protected set; }
        public string Viscosity { get; protected set; }
        public string Api { get; protected set; }
        public OilType OilType { get; protected set; }
        public string Manufacturer { get; protected set; }
        public FilterType FilterType { get; protected set; }
        public bool HasAlternative { get; protected set; }


        public void ReduceStock(decimal quantity, UoM uom)
        {
            StockQuantity -= GetQuantityInDefaultUoM(quantity, uom);
        }

        public void IncreseStock(decimal quantity, UoM uom)
        {
            StockQuantity += GetQuantityInDefaultUoM(quantity, uom);
        }

        public decimal GetQuantityInDefaultUoM(decimal quantity, UoM uom)
        {
            if (uom == AlternativeUoM)
                quantity /= 20;

            return quantity;
        }
    }
}