using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum UoM
    {
        [Description("Litro")]
        Liter,
        [Description("Balde")]
        Bucket,
        [Description("Unidade")]
        Unit,
        [Description("Outro")]
        Other
    }
}
