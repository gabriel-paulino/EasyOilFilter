using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum UoM
    {
        [Description("")]
        None,
        [Description("Litro")]
        Liter,
        [Description("Balde")]
        Bucket,
        [Description("Outro")]
        Other,
        [Description("Unidade")]
        Unity
    }
}
