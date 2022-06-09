using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public Purchase(string provider, DateTime date, string remarks, List<PurchaseItem> items)
        {
            Provider = provider;
            Date = date;
            Remarks = remarks;
            Status = DocumentStatus.Finished;

            _items = items;
        }

        public Purchase(Guid id, string provider, decimal total, DateTime date, string remarks, DocumentStatus status)
        {
            Id = id;
            Provider = provider;
            Total = total;
            Date = date;
            Remarks = remarks;
            Status = status;

            _items = new List<PurchaseItem>();
        }

        public string Provider { get; private set; }
        public decimal Total { get; private set; }
        public DateTime Date { get; private set; }
        public string Remarks { get; private set; }
        public DocumentStatus Status { get; private set; }

        private List<PurchaseItem> _items;
        public IReadOnlyCollection<PurchaseItem> Items { get => _items.ToArray(); }

        public void SetTotal(decimal total)
        {
            Total = total;
        }

        public void AddItem(PurchaseItem item)
        {
            _items.Add(item);
        }
    }
}