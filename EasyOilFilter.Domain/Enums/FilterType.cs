using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum FilterType
    {
        [Description("")]
        All,
        [Description("Óleo")]
        Oil,
        [Description("Ar")]
        Air,
        [Description("Combustível")]
        Fuel,
        [Description("Diesel")]
        Diesel,
        [Description("Sedimentador")]
        Sediment,
        [Description("Ar condicionado")]
        AirConditioning,
        [Description("Água")]
        Water,
        [Description("Ar interno")]
        SafeAir,
        [Description("Hidráulico")]
        Hydraulic,
        [Description("Desumidificador")]
        Dessumidificador,
        [Description("Outro")]
        Other
    }
}