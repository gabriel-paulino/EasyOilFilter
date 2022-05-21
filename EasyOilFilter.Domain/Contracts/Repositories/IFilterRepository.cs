using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IFilterRepository : IDisposable
    {
        Task<IEnumerable<Filter>> GetAll();
        Task<IEnumerable<Filter>> Get(int page, int quantity);
        Task<IEnumerable<Filter>> Get(string name = "", string manufacturer = "", FilterType type = FilterType.All);
        Task<Filter> Get(Guid id);
        Task<IEnumerable<Filter>> GetByCode(string code);
        Task<IEnumerable<Filter>> GetByManufacturer(string manufacturer);
        Task<IEnumerable<Filter>> Get(FilterType type);
        Task<bool> Create(Filter filter);
        Task<bool> Update(Filter filter);
        Task<bool> UpdatePrice(Guid id, decimal price);
    }
}