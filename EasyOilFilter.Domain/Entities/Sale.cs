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

        public PaymentMethodType PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public string Client { get; set; }

        private IEnumerable<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items { get => _items.ToArray(); }
    }
}
