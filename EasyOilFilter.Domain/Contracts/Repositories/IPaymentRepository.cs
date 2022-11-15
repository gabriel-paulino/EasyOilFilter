using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IPaymentRepository : IDisposable
    {
        Task<bool> Add(Payment payment);
        Task<IEnumerable<Payment>> GetAllByPurchaseId(Guid purchaseId);
        Task<IEnumerable<Payment>> GetPaidByPurchaseId(Guid purchaseId);
        Task<bool> Cancel(Guid id);
    }
}