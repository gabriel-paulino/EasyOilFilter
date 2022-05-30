using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.SaleViewModel
{
    public class AddSaleViewModel
    {
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<AddSaleItemViewModel> Items { get; set; }

        public static implicit operator Sale(AddSaleViewModel model)
        {
            var items = model.Items;

            var sale = new Sale(
                            description: model.Description,
                            paymentMethod: EnumUtility.GetEnumByDescription<PaymentMethod>(model.PaymentMethod),
                            discount: model.Discount,
                            date: model.Date,
                            remarks: model.Remarks,
                            items: items.Select(item => (SaleItem)item).ToList()
                            );

            foreach (var item in sale.Items)
                item.SetSaleId(sale.Id);

            sale.SetTotal(items.Sum(item => item.TotalItem));

            return sale;
        }
    }
}