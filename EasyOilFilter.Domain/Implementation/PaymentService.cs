using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.ViewModels.PaymentViewModel;

namespace EasyOilFilter.Domain.Implementation;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(
        IPaymentRepository productRepository)
    {
        _paymentRepository = productRepository;
    }

    public void Dispose() => _paymentRepository.Dispose();

    public async Task<(bool isAdd, IEnumerable<string> messages)> Add(AddPaymentViewModel model)
    {
        var entity = (Payment)model;

        if (!entity.IsValid)
            return (false, entity.Notifications.Select(n => n.Message));

        decimal maxValueAllowedPay = model.PurchaseTotal;
        var payments = await GetByPurchaseId(entity.PurchaseId);

        if (payments?.Any() ?? false)
        {
            decimal totalPaid = payments.Sum(payment => payment.AmountPaid);
            maxValueAllowedPay = model.PurchaseTotal - totalPaid;
        }

        if (entity.AmountPaid > maxValueAllowedPay)
            return (false,
                (
                 $"Baseado em pagamentos anteriores...{Environment.NewLine}" + 
                 $"O valor máximo permitido é de {maxValueAllowedPay:C2}.").GetListText()
                );

        bool isAddPayment = await _paymentRepository.Add(model);

        return isAddPayment
            ? (true, Enumerable.Empty<string>())
            : (false, "Falha ao adicionar o pagamento.".GetListText());
    }

    public async Task<IEnumerable<Payment>> GetByPurchaseId(Guid purchaseId) =>
        await _paymentRepository.GetPaidByPurchaseId(purchaseId);
}