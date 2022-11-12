using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.ViewModels.PaymentViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IPaymentService : IDisposable
    {
        Task<(bool isAdd, IEnumerable<string> messages)> Add(AddPaymentViewModel model);
        Task<IEnumerable<Payment>> Get(Guid purchaseId);
    }
}