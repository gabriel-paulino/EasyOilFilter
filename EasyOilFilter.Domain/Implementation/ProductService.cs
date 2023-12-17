using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService
        (
            IProductRepository productRepository,
            IUnitOfWork unitOfWork
        )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose() => _productRepository.Dispose();

        public async Task<IEnumerable<ProductViewModel>> GetProducts(string name)
        {
            var products = await _productRepository.GetByName(name);

            return products?.Any() ?? false
                ? ProductViewModel.MapMany(products)
                : default;
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByAbsoluteValue(decimal absoluteValue)
        {
            if (absoluteValue == 0)
                return (false, "Valor absoluto deve ser diferente de zero.");

            var filters = await _productRepository.GetAllFilters();

            if (!filters?.Any() ?? true)
                return (true, "Não existem filtros cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var filter in filters)
            {
                filter.ChangeDefaultPriceByAbsoluteValue(absoluteValue);
                if (await _productRepository.UpdateDefaultPrice(filter.Id, filter.DefaultPrice)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do filtro: {filter.Name}.";
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

            var filters = await _productRepository.GetAllFilters();

            if (!filters?.Any() ?? true)
                return (true, "Não existem filtros cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var filter in filters)
            {
                filter.ChangeDefaultPriceByPercentage(percentage);
                if (await _productRepository.UpdateDefaultPrice(filter.Id, filter.DefaultPrice)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do filtro: {filter.Name}.";
                    _unitOfWork.Rollback();
                    break;
                }
            }

            if (sucess) _unitOfWork.Commit();

            return (sucess, message);
        }

        public async Task<(FilterViewModel result, string error)> Create(AddFilterViewModel model)
        {
            var filter = (Filter)model;

            if (!filter.IsValid)
                return (default, filter.GetFirstNotificationMessage());

            if (await _productRepository.Create(filter))
                return ((FilterViewModel)filter, string.Empty);

            return (default, "Falha ao adicionar filtro.");
        }

        public async Task<IEnumerable<FilterViewModel>> GetFilters(int page, int quantity)
        {
            var filters = await _productRepository.GetFilters(page, quantity);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<FilterViewModel> GetFilter(Guid id)
        {
            return await _productRepository.GetFilter(id);
        }

        public async Task<IEnumerable<FilterViewModel>> Get(SearchFilterViewModel model)
        {
            var filters = await _productRepository.Get(model.Name, model.Manufacturer, model.FilterType);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> Get(FilterType type)
        {
            var filters = await _productRepository.Get(type);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetAllFilters()
        {
            var filters = await _productRepository.GetAllFilters();

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetFiltersByName(string name)
        {
            var filters = await _productRepository.GetFiltersByName(name);

            return filters?.Any() ?? false
                ? FilterViewModel.MapMany(filters)
                : default;
        }

        public async Task<IEnumerable<FilterViewModel>> GetByManufacturer(string manufacturer)
        {
            var filter = await _productRepository.GetByManufacturer(manufacturer);

            return filter?.Any() ?? false
                ? FilterViewModel.MapMany(filter)
                : default;
        }

        public async Task<(FilterViewModel result, string error)> Update(Guid id, FilterViewModel model)
        {
            if (id != model.Id)
                return (default, "Filtro não encontrado.");

            var updatedFilter = (Filter)model;

            if (!updatedFilter.IsValid)
                return (default, updatedFilter.GetFirstNotificationMessage());

            _unitOfWork.BeginTransaction();

            if (await _productRepository.Update(updatedFilter))
            {
                _unitOfWork.Commit();
                return (updatedFilter, string.Empty);
            }

            _unitOfWork.Rollback();

            return (default, "Falha ao atualizar filtro.");
        }

        public async Task<IEnumerable<OilViewModel>> GetAllOils()
        {
            var oils = await _productRepository.GetAllOils();

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> Get(SearchOilViewModel model)
        {
            var oils = await _productRepository.Get(model.Name, model.Viscosity, model.OilType);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> GetOils(int page, int quantity)
        {
            var oils = await _productRepository.GetOils(page, quantity);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<OilViewModel> GetOil(Guid id)
        {
            return await _productRepository.GetOil(id);
        }

        public async Task<IEnumerable<OilViewModel>> Get(OilType type)
        {
            var oils = await _productRepository.Get(type);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> GetOilsByName(string name)
        {
            var oils = await _productRepository.GetOilsByName(name);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> GetByViscosity(string viscosity)
        {
            var oils = await _productRepository.GetByViscosity(viscosity);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<(OilViewModel result, string error)> Create(AddOilViewModel model)
        {
            var oil = (Oil)model;

            if (!oil.IsValid)
                return (default, oil.GetFirstNotificationMessage());

            if (await _productRepository.Create(oil))
                return ((OilViewModel)oil, string.Empty);

            return (default, "Falha ao adicionar lubrificante.");
        }

        public async Task<(OilViewModel result, string error)> Update(Guid id, OilViewModel model)
        {
            if (id != model.Id)
                return (default, "Lubrificante não encontrado.");


            var updatedOil = (Oil)model;

            if (!updatedOil.IsValid)
                return (default, updatedOil.GetFirstNotificationMessage());

            _unitOfWork.BeginTransaction();

            if (await _productRepository.Update(updatedOil))
            {
                _unitOfWork.Commit();
                return (updatedOil, string.Empty);
            }

            _unitOfWork.Rollback();

            return (default, "Falha ao atualizar lubrificante.");
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByAbsoluteValue(decimal absoluteValue)
        {
            if (absoluteValue == 0)
                return (false, "Valor absoluto deve ser diferente de zero.");

            var oils = await _productRepository.GetAllOils();

            if (!oils?.Any() ?? true)
                return (true, "Não existem lubrificantes cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var oil in oils)
            {
                oil.ChangeDefaultPriceByAbsoluteValue(absoluteValue);
                if (await _productRepository.UpdateDefaultPrice(oil.Id, oil.DefaultPrice)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do lubrificante: {oil.Name}.";
                    _unitOfWork.Rollback();
                    break;
                }
            }

            if (sucess) _unitOfWork.Commit();

            return (sucess, message);
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByPercentage(decimal percentage)
        {
            if (percentage == 0)
                return (false, "Porcentagem deve ser diferente de zero.");

            var oils = await _productRepository.GetAllOils();

            if (!oils?.Any() ?? true)
                return (true, "Não existem lubrificantes cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var oil in oils)
            {
                oil.ChangeDefaultPriceByPercentage(percentage);
                if (await _productRepository.UpdateDefaultPrice(oil.Id, oil.DefaultPrice)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do lubrificante: {oil.Name}.";
                    _unitOfWork.Rollback();
                    break;
                }
            }

            if (sucess) _unitOfWork.Commit();

            return (sucess, message);
        }

        public async Task<(bool sucess, string message)> Delete(Guid id)
        {
            var product = await _productRepository.Get(id);

            if (product is null)
                return (false, $"Produto com id '{id}' não encontrado.");

            bool deleted = await _productRepository.Delete(id);

            return deleted
                ? (true, string.Empty)
                : (false, $"Falha ao deletar produto: {product.Name}.");
        }
    }
}
