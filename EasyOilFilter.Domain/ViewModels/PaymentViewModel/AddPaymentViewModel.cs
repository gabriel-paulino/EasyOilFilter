using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Utils;

namespace EasyOilFilter.Domain.ViewModels.PaymentViewModel;

public class AddPaymentViewModel
{
    public Guid PurchaseId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public decimal PurchaseTotal { get; set; }
    public string BankAccount { get; set; }

    public static implicit operator Payment(AddPaymentViewModel model) => new
        (
            purchaseId: model.PurchaseId,
            paymentDate: model.PaymentDate,
            amountPaid: model.AmountPaid,
            bankAccount: EnumUtility.GetEnumByDescription<BankAccount>(model.BankAccount)
        );
}
