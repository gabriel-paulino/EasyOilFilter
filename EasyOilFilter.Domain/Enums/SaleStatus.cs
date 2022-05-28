using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum SaleStatus
    {
        [Description("Finalizada")]
        Finished,
        [Description("Cancelada")]
        Canceled,
        [Description("Outro")]
        Other
    }
}