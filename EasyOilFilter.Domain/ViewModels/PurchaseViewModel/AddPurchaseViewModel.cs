using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.ViewModels.PurchaseViewModel
{
    public class AddPurchaseViewModel
    {
        public string Provider { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<AddPurchaseItemViewModel> Items { get; set; }

        public static implicit operator Purchase(AddPurchaseViewModel model)
        {
            var items = model.Items;

            var purchase = new Purchase(
                            provider: model.Provider,
                            date: model.Date,
                            remarks: model.Remarks,
                            items: items.Select(item => (PurchaseItem)item).ToList()
                            );

            foreach (var item in purchase.Items)
                item.SetPurchaseId(purchase.Id);

            decimal totalItems = items.Sum(item => item.TotalItem);

            purchase.SetTotal(totalItems);

            return purchase;
        }
    }
}