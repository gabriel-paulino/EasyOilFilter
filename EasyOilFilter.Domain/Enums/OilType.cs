using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum OilType
    {
        [Description("Todos")]
        All,
        [Description("Mineral")]
        Mineral,
        [Description("Semissintético")]
        SemiSynthetic,
        [Description("Sintético")]
        Synthetic,
        [Description("Transmissão")]
        Transmission,
        [Description("Outro")]
        Other  
    }
}