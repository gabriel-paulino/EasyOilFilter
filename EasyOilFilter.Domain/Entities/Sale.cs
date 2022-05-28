using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Sale()
        {
            _items = new List<SaleItem>();
        }

        public Sale(Guid id, string description, PaymentMethod paymentMethod, decimal total, decimal discount, DateTime date, string remarks)
        {
            Id = id;
            Description = description;
            PaymentMethod = paymentMethod;
            Total = total;
            Discount = discount;
            Date = date;
            Remarks = remarks;
            Status = SaleStatus.Finished;

            _items = new List<SaleItem>();
        }

        public string Description { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public SaleStatus Status { get; set; }
        
        private List<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items { get => _items.ToArray(); }

        public void AddItem(SaleItem item)
        {
            _items.Add(item);
        }
    }
}