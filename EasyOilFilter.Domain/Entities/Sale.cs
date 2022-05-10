﻿using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities
{
    public class Sale : BaseEntity
    {
        public Sale()
        {
            _items = new List<SaleItem>();
        }

        private IEnumerable<SaleItem> _items;

        public PaymentMethodType PaymentMethod { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        public DateTime Date { get; set; }
        public string Remark { get; set; }
        public string Client { get; set; }
        public IReadOnlyCollection<SaleItem> Items { get => _items.ToArray(); }
    }
}
