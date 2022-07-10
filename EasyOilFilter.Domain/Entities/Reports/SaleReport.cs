using EasyOilFilter.Domain.Entities.Base;

namespace EasyOilFilter.Domain.Entities.Reports
{
    public class SaleReport : BaseEntity
    {
        public SaleReport(
            IEnumerable<SaleByDate> salesByDate, 
            IEnumerable<SaleByPaymentMethod> salesByPaymentMethod)
        {
            SalesByDate = salesByDate;
            SalesByPaymentMethod = salesByPaymentMethod;
            TotalSales = salesByPaymentMethod.Sum(sale => sale.Total);
        }

        public IEnumerable<SaleByDate> SalesByDate { get; private set; }
        public IEnumerable<SaleByPaymentMethod> SalesByPaymentMethod { get; private set; }
        public decimal TotalSales { get; private set; }
    }
}