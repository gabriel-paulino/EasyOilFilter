using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class SaleItem : BaseEntity
    {
        public SaleItem(Guid id, Guid saleId, Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
        {
            Id = id;
            SaleId = saleId;
            ProductId = productId;
            ItemDescription = itemDescription;
            UnitOfMeasurement = unitOfMeasurement;
            Quantity = quantity;
            UnitaryPrice = unitaryPrice;
            TotalItem = totalItem;
        }

        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }//ToDo - Juntar tabela Oil e Filter formar tabela Product
        public string ItemDescription { get; set; }
        public UoM UnitOfMeasurement { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }
    }
}