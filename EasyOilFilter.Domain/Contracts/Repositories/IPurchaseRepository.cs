using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IPurchaseRepository : IDisposable
    {
        Task<IEnumerable<Purchase>> Get(DateTime startDate, DateTime endDate);
        Task<Purchase> Get(Guid id);
        Task<bool> AddHeader(Purchase sale);
        Task<bool> AddItem(PurchaseItem item);
        Task<bool> Cancel(Guid id);
    }
}