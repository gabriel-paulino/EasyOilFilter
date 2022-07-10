using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Entities.Reports;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface ISaleRepository : IDisposable
    {
        Task<IEnumerable<Sale>> Get(DateTime date);
        Task<IEnumerable<Sale>> Get(DateTime startDate, DateTime finalDate);
        Task<Sale> Get(Guid id);       
        Task<bool> AddHeader(Sale sale);
        Task<bool> AddItem(SaleItem item);
        Task<bool> Cancel(Guid id);
        Task<IEnumerable<SoldItemReport>> GetSoldItems(DateTime startDate, DateTime finalDate);
    }
}