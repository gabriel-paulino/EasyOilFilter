using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class GoodsReceiptItem : BaseEntity
    {
        public GoodsReceiptItem(Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
        {
            ProductId = productId;
            ItemDescription = itemDescription;
            UnitOfMeasurement = unitOfMeasurement;
            Quantity = quantity;
            UnitaryPrice = unitaryPrice;
            TotalItem = totalItem;
        }

        public GoodsReceiptItem(Guid id, Guid goodsReceiptId, Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
        {
            Id = id;
            GoodsReceiptId = goodsReceiptId;
            ProductId = productId;
            ItemDescription = itemDescription;
            UnitOfMeasurement = unitOfMeasurement;
            Quantity = quantity;
            UnitaryPrice = unitaryPrice;
            TotalItem = totalItem;
        }

        public Guid GoodsReceiptId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ItemDescription { get; private set; }
        public UoM UnitOfMeasurement { get; private set; }
        public decimal Quantity { get; private set; }
        public decimal UnitaryPrice { get; private set; }
        public decimal TotalItem { get; private set; }

        public void SetGoodsReceiptId(Guid goodsReceiptId)
        {
            GoodsReceiptId = goodsReceiptId;
        }
    }
}