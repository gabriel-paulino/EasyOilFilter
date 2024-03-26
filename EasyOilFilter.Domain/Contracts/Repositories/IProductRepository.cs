using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories;

public interface IProductRepository : IDisposable
{
    Task<Product> GetAsync(Guid id);
    Task<IEnumerable<Product>> GetAsync(IEnumerable<Guid> ids);
    Task<IEnumerable<T>> GetAllAsync<T>() where T : Product;
    Task<IEnumerable<T>> GetByName<T>(string name) where T : Product;
    Task<bool> CreateAsync<T>(T product) where T : Product;
    Task<bool> UpdateAsync<T>(T product) where T : Product;
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<Filter>> GetAsync(string name = "", string manufacturer = "", FilterType type = FilterType.None);
    Task<IEnumerable<Oil>> GetAsync(string name = "", string viscosity = "", OilType type = OilType.None);
    Task<bool> UpdateDefaultPriceAsync(Guid id, decimal defaultPrice);
    Task<bool> SetStockQuantityAsync(Guid id, decimal stockQuantity);
}