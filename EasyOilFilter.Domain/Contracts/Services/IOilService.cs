using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IOilService : IDisposable
    {
        Task<IEnumerable<OilViewModel>> GetAll();
        Task<IEnumerable<OilViewModel>> Get(int page, int quantity);
        Task<OilViewModel> Get(Guid id);
        Task<IEnumerable<OilViewModel>> Get(SearchOilViewModel model);
        Task<IEnumerable<OilViewModel>> GetByName(string name);
        Task<IEnumerable<OilViewModel>> GetByViscosity(string viscosity);
        Task<IEnumerable<OilViewModel>> Get(OilType type);

        Task<OilViewModel> Create(AddOilViewModel model);
        Task<OilViewModel> Update(Guid id, OilViewModel model);
        Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByAbsoluteValue(decimal absoluteValue);
        Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByPercentage(decimal percentage);
    }
}