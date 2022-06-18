using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum OilType
    {
        [Description("")]
        None,
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