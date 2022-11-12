using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.PurchaseViewModel
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            Items = new List<PurchaseItemViewModel>();
        }

        public Guid Id { get; set; }
        public string Provider { get; set; }
        public decimal Total { get; set; }
        public decimal PaymentDone { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }

        public IEnumerable<PurchaseItemViewModel> Items { get; set; }

        public static implicit operator PurchaseViewModel(Purchase purchase) =>
            new()
            {
                Id = purchase.Id,
                Provider = purchase.Provider,
                Total = purchase.Total,
                PaymentDone = purchase.PaymentDone,
                Date = purchase.Date,
                Remarks = purchase.Remarks,
                Status = purchase.Status.GetDescription(),
                Items = purchase.Items.Select(item => (PurchaseItemViewModel)item),
            };
        

        public static implicit operator Purchase(PurchaseViewModel model)
        {
            var purchase = new Purchase(
                            id: model.Id,
                            provider: model.Provider,
                            total: model.Total,
                            paymentDone: model.PaymentDone,
                            date: model.Date,
                            remarks: model.Remarks,
                            status: EnumUtility.GetEnumByDescription<DocumentStatus>(model.Status)
                            );

            foreach (var item in model.Items)
                purchase.AddItem(item);
            
            return purchase;
        }
            
        public static IEnumerable<PurchaseViewModel> MapMany(IEnumerable<Purchase> purchases) =>
            purchases.Select(sale => (PurchaseViewModel)sale);
    }
}