using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum ProductType
    {
        [Description("Lubrificante")]
        Oil,
        [Description("Filtro")]
        Filter
    }
}