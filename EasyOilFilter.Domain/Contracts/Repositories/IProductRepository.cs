using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IProductRepository : IDisposable
    {
        Task<IEnumerable<Product>> GetAll();
        Task<IEnumerable<Product>> GetByName(string name);
        Task<IEnumerable<Product>> Get(IEnumerable<Guid> ids);
        Task<IEnumerable<Filter>> GetAllFilters();
        Task<IEnumerable<Filter>> GetFilters(int page, int quantity);
        Task<IEnumerable<Filter>> Get(string name = "", string manufacturer = "", FilterType type = FilterType.All);
        Task<Filter> GetFilter(Guid id);
        Task<IEnumerable<Filter>> GetFiltersByName(string name);
        Task<IEnumerable<Filter>> GetByManufacturer(string manufacturer);
        Task<IEnumerable<Filter>> Get(FilterType type);
        Task<bool> Create(Filter filter);
        Task<bool> Update(Filter filter);
        Task<bool> UpdateDefaultPrice(Guid id, decimal defaultPrice);
        Task<bool> SetStockQuantity(Guid id, decimal stockQuantity);

        Task<IEnumerable<Oil>> GetAllOils();
        Task<IEnumerable<Oil>> GetOils(int page, int quantity);
        Task<IEnumerable<Oil>> Get(string name = "", string viscosity = "", OilType type = OilType.All);
        Task<Oil> GetOil(Guid id);
        Task<IEnumerable<Oil>> GetOilsByName(string name);
        Task<IEnumerable<Oil>> GetByViscosity(string viscosity);
        Task<IEnumerable<Oil>> Get(OilType type);
        Task<bool> Create(Oil oil);
        Task<bool> Update(Oil oil);
    }
}