using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface ISaleRepository : IDisposable
    {
        Task<IEnumerable<Sale>> Get(DateTime date);
        Task<Sale> Get(Guid id);       
        Task<bool> AddHeader(Sale sale);
        Task<bool> AddItem(SaleItem item);
        Task<bool> Cancel(Guid id);
    }
}