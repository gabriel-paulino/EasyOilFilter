using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum ReportType
    {
        [Description("vendas")]
        Sales,
        [Description("itens vendidos")]
        SoldItems
    }
}