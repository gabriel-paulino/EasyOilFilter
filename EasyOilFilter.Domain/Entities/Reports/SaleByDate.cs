using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities.Reports
{
    public class SaleByDate : BaseEntity
    {
        public SaleByDate(DateTime date, decimal total)
        {
            Date = date;
            Total = total;
        }

        public DateTime Date { get; private set; }
        public decimal Total { get; private set; }
    }
}