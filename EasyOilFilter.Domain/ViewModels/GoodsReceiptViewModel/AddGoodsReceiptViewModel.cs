using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.ViewModels.GoodsReceiptViewModel
{
    public class AddGoodsReceiptViewModel
    {
        public string Provider { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<AddGoodsReceiptItemViewModel> Items { get; set; }

        public static implicit operator GoodsReceipt(AddGoodsReceiptViewModel model)
        {
            var items = model.Items;

            var goodsReceipt = new GoodsReceipt(
                            provider: model.Provider,
                            date: model.Date,
                            remarks: model.Remarks,
                            items: items.Select(item => (GoodsReceiptItem)item).ToList()
                            );

            foreach (var item in goodsReceipt.Items)
                item.SetGoodsReceiptId(goodsReceipt.Id);

            decimal totalItems = items.Sum(item => item.TotalItem);

            goodsReceipt.SetTotal(totalItems);

            return goodsReceipt;
        }
    }
}