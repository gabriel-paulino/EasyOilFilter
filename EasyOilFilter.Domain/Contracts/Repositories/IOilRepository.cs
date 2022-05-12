using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IOilRepository : IDisposable
    {
        Task<IEnumerable<Oil>> GetAll();
        Task<IEnumerable<Oil>> Get(int page, int quantity);
        Task<IEnumerable<Oil>> Get(string name = "", string viscosity = "", OilType type = OilType.All);
        Task<Oil> Get(Guid id);
        Task<IEnumerable<Oil>> GetByName(string name);
        Task<IEnumerable<Oil>> GetByViscosity(string viscosity);
        Task<IEnumerable<Oil>> Get(OilType type);

        Task<bool> Create(Oil oil);
        Task<bool> Update(Oil oil);
        Task<bool> UpdatePrice(Guid id, decimal price);
        Task<bool> Delete(Guid id);
    }
}
