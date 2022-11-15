using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum PaymentStatus
    {
        [Description("Pago")]
        Done,
        [Description("Cancelado")]
        Canceled
    }
}