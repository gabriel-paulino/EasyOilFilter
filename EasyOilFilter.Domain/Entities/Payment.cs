using EasyOilFilter.Domain.Entities.Base;
using EasyOilFilter.Domain.Enums;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Payment
        (
            Guid id,
            Guid purchaseId,
            DateTime paymentDate,
            decimal amountPaid,
            BankAccount bankAccount,
            PaymentStatus status
        )
        {
            Id = id;
            PurchaseId = purchaseId;
            PaymentDate = paymentDate;
            AmountPaid = amountPaid;
            BankAccount = bankAccount;
            Status = status;
        }

        public Payment(Guid purchaseId, DateTime paymentDate, decimal amountPaid, BankAccount bankAccount)
        {
            AddNotifications(GetContract(purchaseId, paymentDate, amountPaid, bankAccount));

            if (IsValid)
            {
                PurchaseId = purchaseId;
                PaymentDate = paymentDate;
                AmountPaid = amountPaid;
                BankAccount = bankAccount;
                Status = PaymentStatus.Done;
            }
        }

        public Guid PurchaseId { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public decimal AmountPaid { get; private set; }
        public BankAccount BankAccount { get; private set; }
        public PaymentStatus Status { get; private set; }

        private static Contract<Payment> GetContract(Guid purchaseId, DateTime paymentDate, decimal amountPaid, BankAccount bankAccount) =>
            new Contract<Payment>()
                .IsGreaterThan(amountPaid, 0, "AmountPaid", "O valor de pagamento deve ser maior que 0.")
                .IsFalse(purchaseId == Guid.Empty, "PurchaseId", "O vínculo com a compra deve ser informado.")
                .IsNotMinValue(paymentDate, "PaymentDate", "A data de pagamento deve ser informada.")
                .IsBetween((int)bankAccount, 1, 5, "BankAccount", "A fonte do recurso (banco) deve ser informado.")
            ;
    }
}