using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.SaleViewModel
{
    public class AddSaleItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ItemDescription { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitaryPrice { get; set; }
        public decimal TotalItem { get; set; }


        public static implicit operator SaleItem(AddSaleItemViewModel model) =>
            new(
                productId: model.ProductId,
                itemDescription: model.ItemDescription,
                unitOfMeasurement: EnumUtility.GetEnumByDescription<UoM>(model.UnitOfMeasurement),
                quantity: model.Quantity,
                unitaryPrice: model.UnitaryPrice,
                totalItem: model.TotalItem
                );
    }
}