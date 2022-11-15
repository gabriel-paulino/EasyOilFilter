using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum BankAccount
    {
        [Description("")]
        None,
        [Description("Nubank")]
        Nubank,
        [Description("Santander jurídica")]
        SantanderJuridical,
        [Description("Santander física")]
        SantanderNatural,
        [Description("Mercado pago")]
        MercadoPago,
        [Description("Outro")]
        Other
    }
}