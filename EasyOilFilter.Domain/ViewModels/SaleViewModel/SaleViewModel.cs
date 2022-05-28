using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.SaleViewModel
{
    public class SaleViewModel
    {
        public SaleViewModel()
        {
            Items = new List<SaleItemViewModel>();
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<SaleItemViewModel> Items { get; set; }

        public static implicit operator SaleViewModel(Sale sale) =>
            new()
            {
                Id = sale.Id,
                Description = sale.Description,
                PaymentMethod = sale.PaymentMethod.GetDescription(),
                Total = sale.Total,
                Discount = sale.Discount,
                Date = sale.Date,
                Remarks = sale.Remarks,
                Items = sale.Items.Select(item => (SaleItemViewModel)item),
            };
        

        public static implicit operator Sale(SaleViewModel model)
        {
            var sale = new Sale(
                            id: model.Id,
                            description: model.Description,
                            paymentMethod: EnumUtility.GetEnumByDescription<PaymentMethod>(model.PaymentMethod),
                            total: model.Total,
                            discount: model.Discount,
                            date: model.Date,
                            remarks: model.Remarks
                            );

            foreach (var item in model.Items)
                sale.AddItem(item);
            
            return sale;
        }
            

        public static IEnumerable<SaleViewModel> MapMany(IEnumerable<Sale> sales) =>
            sales.Select(sale => (SaleViewModel)sale);
    }
}