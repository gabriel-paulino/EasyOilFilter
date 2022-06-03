﻿using System.ComponentModel;

namespace EasyOilFilter.Domain.Enums
{
    public enum PaymentMethod
    {
        [Description("")]
        All,
        [Description("Dinheiro")]
        Money,
        [Description("Pix")]
        Pix,
        [Description("Cartão de crédito")]
        Credit,
        [Description("Cartão de débito")]
        Debit,
        [Description("Parcelado 2x")]
        TwoInstallments,
        [Description("Boleto bancário")]
        BankSlip,
        [Description("Outro")]
        Other
    }
}
