using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum UoM
    {
        [Description("Litro")]
        Liter,
        [Description("Balde")]
        Bucket,
        [Description("Outro")]
        Other,
        Unity
    }
}
