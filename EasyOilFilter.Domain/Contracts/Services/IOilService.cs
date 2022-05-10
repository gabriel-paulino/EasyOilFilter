using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IOilService : IDisposable
    {
        Task<IEnumerable<OilViewModel>> Get(int page, int quantity);
        Task<OilViewModel> Get(Guid id);
        Task<IEnumerable<OilViewModel>> GetByName(string name);
        Task<IEnumerable<OilViewModel>> GetByViscosity(string viscosity);
        Task<IEnumerable<OilViewModel>> Get(OilType type);

        Task<bool> Create(AddOilViewModel oil);
        Task<bool> Update(OilViewModel oil);
        Task<bool> Delete(Guid id);
    }
}