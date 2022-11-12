using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IPaymentRepository : IDisposable
    {
        Task<bool> Add(Payment payment);
        Task<IEnumerable<Payment>> Get(Guid purchaseId);
    }
}