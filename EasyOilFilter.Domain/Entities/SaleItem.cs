using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities;

public class SaleItem : BaseEntity
{
    public SaleItem(Guid productId, string itemDescription, UoM unitOfMeasurement, decimal quantity, decimal unitaryPrice, decimal totalItem)
    {
        ProductId = productId;
        ItemDescription = itemDescription;
        UnitOfMeasurement = unitOfMeasurement;
        Quantity = quantity;
        UnitaryPrice = unitaryPrice;
        TotalItem = totalItem;
    }

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

    public Guid SaleId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ItemDescription { get; private set; }
    public UoM UnitOfMeasurement { get; private set; }
    public decimal Quantity { get; private set; }
    public decimal UnitaryPrice { get; private set; }
    public decimal TotalItem { get; private set; }

    public void SetSaleId(Guid saleId)
    {
        SaleId = saleId;
    }
}