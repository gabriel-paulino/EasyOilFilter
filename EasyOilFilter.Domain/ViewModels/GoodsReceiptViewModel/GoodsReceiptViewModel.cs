using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.GoodsReceiptViewModel
{
    public class GoodsReceiptViewModel
    {
        public GoodsReceiptViewModel()
        {
            Items = new List<GoodsReceiptItemViewModel>();
        }

        public Guid Id { get; set; }
        public string Provider { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public IEnumerable<GoodsReceiptItemViewModel> Items { get; set; }

        public static implicit operator GoodsReceiptViewModel(GoodsReceipt sale) =>
            new()
            {
                Id = sale.Id,
                Provider = sale.Provider,
                Total = sale.Total,
                Date = sale.Date,
                Remarks = sale.Remarks,
                Status = sale.Status.GetDescription(),
                Items = sale.Items.Select(item => (GoodsReceiptItemViewModel)item),
            };
        

        public static implicit operator GoodsReceipt(GoodsReceiptViewModel model)
        {
            var sale = new GoodsReceipt(
                            id: model.Id,
                            provider: model.Provider,
                            total: model.Total,
                            date: model.Date,
                            remarks: model.Remarks,
                            status: EnumUtility.GetEnumByDescription<DocumentStatus>(model.Status)
                            );

            foreach (var item in model.Items)
                sale.AddItem(item);
            
            return sale;
        }
            

        public static IEnumerable<GoodsReceiptViewModel> MapMany(IEnumerable<GoodsReceipt> sales) =>
            sales.Select(sale => (GoodsReceiptViewModel)sale);
    }
}