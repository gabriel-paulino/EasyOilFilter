using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.PurchaseViewModel
{
    public class PurchaseItemViewModel
    {
        public Guid Id { get; set; }
        public Guid PurchaseId { get; set; }
        public Guid ProductId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }


        public static implicit operator PurchaseItemViewModel(PurchaseItem item) =>
           new()
           {
               Id = item.Id,
               PurchaseId = item.PurchaseId,
               ProductId = item.ProductId,
               ItemDescription = item.ItemDescription,
               UnitOfMeasurement = item.UnitOfMeasurement.GetDescription(),
               Quantity = item.Quantity,
               UnitaryPrice = item.UnitaryPrice,
               TotalItem = item.TotalItem
           };

        public static implicit operator PurchaseItem(PurchaseItemViewModel model) =>
            new(
                id: model.Id,
                purchaseId: model.PurchaseId,
                productId: model.ProductId,
                itemDescription: model.ItemDescription,
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement),
                quantity: model.Quantity,
                unitaryPrice: model.UnitaryPrice,
                totalItem: model.TotalItem
                );

        public static implicit operator PurchaseItemViewModel(ProductViewModel model) =>
            new()
            {
                ProductId = Guid.Parse(model.Id),
                ItemDescription = model.Name,
                UnitOfMeasurement = model.UnitOfMeasurement,
                UnitaryPrice = model.Price,
            };
    }
}