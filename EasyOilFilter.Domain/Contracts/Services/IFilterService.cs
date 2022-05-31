using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IFilterService : IDisposable
    {
        Task<IEnumerable<FilterViewModel>> GetAll();
        Task<IEnumerable<FilterViewModel>> Get(int page, int quantity);
        Task<FilterViewModel> Get(Guid id);
        Task<IEnumerable<FilterViewModel>> Get(SearchFilterViewModel model);
        Task<IEnumerable<FilterViewModel>> GetByName(string name);
        Task<IEnumerable<FilterViewModel>> GetByManufacturer(string manufacturer);
        Task<IEnumerable<FilterViewModel>> Get(FilterType type);

        Task<FilterViewModel> Create(AddFilterViewModel model);
        Task<FilterViewModel> Update(Guid id, FilterViewModel model);
        Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByAbsoluteValue(decimal absoluteValue);
        Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByPercentage(decimal percentage);
    }
}