using EasyOilFilter.Domain.Entities.Base;
using Flunt.Validations;

namespace EasyOilFilter.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Payment(Guid id, Guid purchaseId, DateTime paymentDate, decimal amountPaid)
        {
            Id = id;
            PurchaseId = purchaseId;
            PaymentDate = paymentDate;
            AmountPaid = amountPaid;
        }

        public Payment(Guid purchaseId, DateTime paymentDate, decimal amountPaid)
        {
            AddNotifications(GetContract(purchaseId, paymentDate, amountPaid));

            if (IsValid)
            {
                PurchaseId = purchaseId;
                PaymentDate = paymentDate;
                AmountPaid = amountPaid;
            }
        }

        public Guid PurchaseId { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public decimal AmountPaid { get; private set; }

        private static Contract<Payment> GetContract(Guid purchaseId, DateTime paymentDate, decimal amountPaid) =>
            new Contract<Payment>()
                .IsGreaterThan(amountPaid, 0, "AmountPaid", "O valor de pagamento deve ser maior que 0.")
                .IsFalse(purchaseId == Guid.Empty, "PurchaseId", "O vínculo com a compra deve ser informado.")
                .IsNotMinValue(paymentDate, "PaymentDate", "A data de pagamento deve ser informada.")
            ;
    }
}