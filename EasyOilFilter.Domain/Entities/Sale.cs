using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Sale(string description, PaymentMethod paymentMethod, decimal discount, DateTime date, string remarks, List<SaleItem> items)
        {
            Description = description;
            PaymentMethod = paymentMethod;
            Discount = discount;
            Date = date;
            Remarks = remarks;
            Status = SaleStatus.Finished;

            _items = items;
        }

        public Sale(Guid id, string description, PaymentMethod paymentMethod, decimal total, decimal discount, DateTime date, string remarks, SaleStatus status)
        {
            Id = id;
            Description = description;
            PaymentMethod = paymentMethod;
            Total = total;
            Discount = discount;
            Date = date;
            Remarks = remarks;
            Status = status;

            _items = new List<SaleItem>();
        }

        public string Description { get; private set; }
        public PaymentMethod PaymentMethod { get; private set; }
        public decimal Total { get; private set; }
        public decimal Discount { get; private set; }
        public DateTime Date { get; private set; }
        public string Remarks { get; private set; }
        public SaleStatus Status { get; private set; }
        
        private List<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items { get => _items.ToArray(); }

        public void SetTotal(decimal total)
        {
            Total = total;
        }

        public void AddItem(SaleItem item)
        {
            _items.Add(item);
        }
    }
}