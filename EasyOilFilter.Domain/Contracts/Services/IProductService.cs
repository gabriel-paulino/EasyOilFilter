﻿using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IProductService : IDisposable
    {
        Task<IEnumerable<ProductViewModel>> GetProducts(string name);

        Task<IEnumerable<FilterViewModel>> GetAllFilters();
        Task<IEnumerable<FilterViewModel>> GetFilters(int page, int quantity);
        Task<FilterViewModel> GetFilter(Guid id);
        Task<IEnumerable<FilterViewModel>> Get(SearchFilterViewModel model);
        Task<IEnumerable<FilterViewModel>> GetFiltersByName(string name);
        Task<IEnumerable<FilterViewModel>> GetByManufacturer(string manufacturer);
        Task<IEnumerable<FilterViewModel>> Get(FilterType type);
        Task<(FilterViewModel result, string error)> Create(AddFilterViewModel model);
        Task<(FilterViewModel result, string error)> Update(Guid id, FilterViewModel model);
        Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByAbsoluteValue(decimal absoluteValue);
        Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByPercentage(decimal percentage);

        Task<IEnumerable<OilViewModel>> GetAllOils();
        Task<IEnumerable<OilViewModel>> GetOils(int page, int quantity);
        Task<OilViewModel> GetOil(Guid id);
        Task<IEnumerable<OilViewModel>> Get(SearchOilViewModel model);
        Task<IEnumerable<OilViewModel>> GetOilsByName(string name);
        Task<IEnumerable<OilViewModel>> GetByViscosity(string viscosity);
        Task<IEnumerable<OilViewModel>> Get(OilType type);
        Task<(OilViewModel result, string error)> Create(AddOilViewModel model);
        Task<(OilViewModel result, string error)> Update(Guid id, OilViewModel model);
        Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByAbsoluteValue(decimal absoluteValue);
        Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByPercentage(decimal percentage);
        Task<(bool sucess, string message)> Delete(Guid id);
    }
}