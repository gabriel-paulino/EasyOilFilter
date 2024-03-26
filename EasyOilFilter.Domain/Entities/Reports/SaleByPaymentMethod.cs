using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Entities.Reports;

public class SaleByPaymentMethod : BaseEntity
{
    public SaleByPaymentMethod(PaymentMethod paymentMethod, decimal total)
    {
        PaymentMethod = paymentMethod;
        Total = total;
    }

    public PaymentMethod PaymentMethod { get; private set; }
    public decimal Total { get; private set; }

}