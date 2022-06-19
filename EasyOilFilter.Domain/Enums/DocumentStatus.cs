using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum DocumentStatus
    {
        [Description("Finalizada")]
        Finished,
        [Description("Cancelada")]
        Canceled,
        [Description("Outro")]
        Other
    }
}