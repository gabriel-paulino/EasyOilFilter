using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities;

public class PurchaseItem : BaseEntity
{
    public PurchaseItem(Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
    {
        ProductId = productId;
        ItemDescription = itemDescription;
        UnitOfMeasurement = unitOfMeasurement;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalItem = totalItem;
    }

    public PurchaseItem(Guid id, Guid purchaseId, Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
    {
        Id = id;
        PurchaseId = purchaseId;
        ProductId = productId;
        ItemDescription = itemDescription;
        UnitOfMeasurement = unitOfMeasurement;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalItem = totalItem;
    }

    public Guid PurchaseId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ItemDescription { get; private set; }
    public UoM UnitOfMeasurement { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitaryPrice { get; private set; }
    public decimal TotalItem { get; private set; }

    public void SetPurchaseId(Guid purchaseId)
    {
        PurchaseId = purchaseId;
    }
}