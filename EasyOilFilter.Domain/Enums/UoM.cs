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
        [Description("Unidade")]
        Unity,
        [Description("Outro")]
        Other,
    }
}
