using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class FilterService : IFilterService
    {
        private readonly IFilterRepository _filterRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public FilterService(
            IFilterRepository filterRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _filterRepository = filterRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByAbsoluteValue(decimal absoluteValue)
        {
            if (absoluteValue == 0)
                return (false, "Valor absoluto deve ser diferente de zero.");

            var filters = await _filterRepository.GetAll();

            if (!filters?.Any() ?? true)
                return (true, "Não existem filtros cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var filter in filters)
            {
                filter.ChangePriceByAbsoluteValue(absoluteValue);
                if (await _filterRepository.UpdatePrice(filter.Id, filter.Price)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do filtro: {filter.Code}.";
                    _unitOfWork.Rollback();
                    break;
                }
            }

            if (sucess) _unitOfWork.Commit();

            return (sucess, message);
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByPercentage(decimal percentage)
        {
            if (percentage == 0)
                return (false, "Porcentagem deve ser diferente de zero.");

            var filters = await _filterRepository.GetAll();

            if (!filters?.Any() ?? true)
                return (true, "Não existem filtros cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var filter in filters)
            {
                filter.ChangePriceByPercentage(percentage);
                if (await _filterRepository.UpdatePrice(filter.Id, filter.Price)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do filtro: {filter.Code}.";
                    _unitOfWork.Rollback();
                    break;
                }
            }

            if (sucess) _unitOfWork.Commit();

            return (sucess, message);
        }

        public async Task<FilterViewModel> Create(AddFilterViewModel model)
        {
            var filter = (Filter)model;
            _notification.AddNotifications(filter.Notifications);

            if (!_notification.IsValid)
                return default;

            if (await _filterRepository.Create(filter))
                return (FilterViewModel)filter;

            _notification.AddNotification("Id", "Falha ao adicionar filtro.");
            return default;
        }

        public async Task<IEnumerable<FilterViewModel>> Get(int page, int quantity)
        {
            var filters = await _filterRepository.Get(page, quantity);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<FilterViewModel> Get(Guid id)
        {
            return await _filterRepository.Get(id);
        }

        public async Task<IEnumerable<FilterViewModel>> Get(SearchFilterViewModel model)
        {
            var filters = await _filterRepository.Get(model.Name, model.Manufacturer, model.Type);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> Get(FilterType type)
        {
            var filters = await _filterRepository.Get(type);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetAll()
        {
            var filters = await _filterRepository.GetAll();

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetByCode(string code)
        {
            var filters = await _filterRepository.GetByCode(code);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetByManufacturer(string manufacturer)
        {
            var filter = await _filterRepository.GetByManufacturer(manufacturer);

            return filter?.Any() ?? false
                ? FilterViewModel.MapMany(filter)
                : default;
        }

        public async Task<FilterViewModel> Update(Guid id, FilterViewModel model)
        {
            if (id != model.Id)
            {
                _notification.AddNotification("Id", "Filtro não encontrado.");
                return default;
            }

            var updatedFilter = (Filter)model;
            _notification.AddNotifications(updatedFilter.Notifications);

            if (!_notification.IsValid)
                return default;

            _unitOfWork.BeginTransaction();

            if (await _filterRepository.Update(updatedFilter))
            {
                _unitOfWork.Commit();
                return updatedFilter;
            }

            _unitOfWork.Rollback();

            _notification.AddNotification("Id", "Falha ao atualizar filtro.");
            return default;
        }

        public void Dispose() => _filterRepository.Dispose();
    }
}