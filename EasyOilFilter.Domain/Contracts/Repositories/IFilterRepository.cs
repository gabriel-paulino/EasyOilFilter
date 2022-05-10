using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IFilterRepository : IDisposable
    {
        Task<IEnumerable<Filter>> Get(int page, int quantity);
        Task<Filter> Get(Guid id);
        Task<IEnumerable<Filter>> Get(string code);
        Task<IEnumerable<Filter>> Get(FilterType type);

        Task<bool> Create(Filter filter);
        Task<bool> Update(Filter filter);
        Task<bool> Delete(Guid id);
    }
}