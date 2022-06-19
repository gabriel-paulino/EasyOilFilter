using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.SaleViewModel
{
    public class SaleItemViewModel
    {
        public Guid Id { get; set; }
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }


        public static implicit operator SaleItemViewModel(SaleItem item) =>
           new()
           {
               Id = item.Id,
               SaleId = item.SaleId,
               ProductId = item.ProductId,
               ItemDescription = item.ItemDescription,
               UnitOfMeasurement = item.UnitOfMeasurement.GetDescription(),
               Quantity = item.Quantity,
               UnitaryPrice = item.UnitaryPrice,
               TotalItem = item.TotalItem
           };

        public static implicit operator SaleItem(SaleItemViewModel model) =>
            new(
                id: model.Id,
                saleId: model.SaleId,
                productId: model.ProductId,
                itemDescription: model.ItemDescription,
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement),
                quantity: model.Quantity,
                unitaryPrice: model.UnitaryPrice,
                totalItem: model.TotalItem
                );
    }
}