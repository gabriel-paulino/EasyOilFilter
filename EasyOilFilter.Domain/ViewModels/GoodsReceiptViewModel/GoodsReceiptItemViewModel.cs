using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.GoodsReceiptViewModel
{
    public class GoodsReceiptItemViewModel
    {
        public Guid Id { get; set; }
        public Guid GoodsReceiptId { get; set; }
        public Guid ProductId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }


        public static implicit operator GoodsReceiptItemViewModel(GoodsReceiptItem item) =>
           new()
           {
               Id = item.Id,
               GoodsReceiptId = item.GoodsReceiptId,
               ProductId = item.ProductId,
               ItemDescription = item.ItemDescription,
               UnitOfMeasurement = item.UnitOfMeasurement.GetDescription(),
               Quantity = item.Quantity,
               UnitaryPrice = item.UnitaryPrice,
               TotalItem = item.TotalItem
           };

        public static implicit operator GoodsReceiptItem(GoodsReceiptItemViewModel model) =>
            new(
                id: model.Id,
                goodsReceiptId: model.GoodsReceiptId,
                productId: model.ProductId,
                itemDescription: model.ItemDescription,
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement),
                quantity: model.Quantity,
                unitaryPrice: model.UnitaryPrice,
                totalItem: model.TotalItem
                );

        public static implicit operator GoodsReceiptItemViewModel(ProductViewModel model) =>
            new()
            {
                ProductId = Guid.Parse(model.Id),
                ItemDescription = model.Name,
                UnitOfMeasurement = model.UnitOfMeasurement,
                UnitaryPrice = model.Price,
            };
    }
}