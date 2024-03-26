using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels;
using EasyOilFilter.Domain.ViewModels.FilterViewModel;
using EasyOilFilter.Domain.ViewModels.OilViewModel;

namespace EasyOilFilter.Domain.Implementation;

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
        var products = await _productRepository
            .GetByName<Product>(name)
            .ConfigureAwait(false);

        if (products.Any())
            return ProductViewModel.MapMany(products.ToList());

        return Enumerable.Empty<ProductViewModel>();
    }

    public async Task<(bool Sucess, string Message)> ChangePriceOfAllFiltersByAbsoluteValue(decimal absoluteValue)
    {
        if (absoluteValue == 0)
            return (false, "Valor absoluto deve ser diferente de zero.");

        var filters = await _productRepository.GetAllAsync<Filter>().ConfigureAwait(false);

        if (!filters?.Any() ?? true)
            return (true, "Não existem filtros cadastrados.");

        bool sucess = true;
        string message = string.Empty;

        _unitOfWork.BeginTransaction();

        foreach (var filter in filters)
        {
            filter.ChangeDefaultPriceByAbsoluteValue(absoluteValue);
            if (await _productRepository.UpdateDefaultPriceAsync(filter.Id, filter.DefaultPrice).ConfigureAwait(false)) continue;
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

        var filters = await _productRepository.GetAllAsync<Filter>().ConfigureAwait(false);

        if (!filters?.Any() ?? true)
            return (true, "Não existem filtros cadastrados.");

        bool sucess = true;
        string message = string.Empty;

        _unitOfWork.BeginTransaction();

        foreach (var filter in filters)
        {
            filter.ChangeDefaultPriceByPercentage(percentage);
            if (await _productRepository.UpdateDefaultPriceAsync(filter.Id, filter.DefaultPrice).ConfigureAwait(false)) continue;
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

        bool sucess = await _productRepository
            .CreateAsync(filter)
            .ConfigureAwait(false);

        if (sucess)
            return ((FilterViewModel)filter, string.Empty);

        return (default, "Falha ao adicionar filtro.");
    }

    public async Task<IEnumerable<FilterViewModel>> Get(SearchFilterViewModel model)
    {
        var filters = await _productRepository.GetAsync(model.Name, model.Manufacturer, model.FilterType).ConfigureAwait(false);

        return filters?.Any() ?? false
            ? FilterViewModel.MapMany(filters)
            : default;
    }

    public async Task<IEnumerable<FilterViewModel>> GetAllFilters()
    {
        var filters = await _productRepository.GetAllAsync<Filter>().ConfigureAwait(false);

        return filters?.Any() ?? false
            ? FilterViewModel.MapMany(filters)
            : default;
    }

    public async Task<IEnumerable<FilterViewModel>> GetFiltersByName(string name)
    {
        var filters = await _productRepository
            .GetByName<Filter>(name)
            .ConfigureAwait(false);

        if (filters.Any())
            return FilterViewModel.MapMany(filters.ToList());

        return Enumerable.Empty<FilterViewModel>();
    }

    public async Task<(FilterViewModel result, string error)> Update(Guid id, FilterViewModel model)
    {
        if (id != model.Id)
            return (default, "Filtro não encontrado.");

        var updatedFilter = (Filter)model;

        if (!updatedFilter.IsValid)
            return (default, updatedFilter.GetFirstNotificationMessage());

        _unitOfWork.BeginTransaction();

        if (await _productRepository.UpdateAsync(updatedFilter).ConfigureAwait(false))
        {
            _unitOfWork.Commit();
            return (updatedFilter, string.Empty);
        }

        _unitOfWork.Rollback();

        return (default, "Falha ao atualizar filtro.");
    }

    public async Task<IEnumerable<OilViewModel>> GetAllOils()
    {
        var oils = await _productRepository.GetAllAsync<Oil>().ConfigureAwait(false);

        return oils?.Any() ?? false
            ? OilViewModel.MapMany(oils)
            : default;
    }

    public async Task<IEnumerable<OilViewModel>> Get(SearchOilViewModel model)
    {
        var oils = await _productRepository.GetAsync(model.Name, model.Viscosity, model.OilType).ConfigureAwait(false);

        return oils?.Any() ?? false
            ? OilViewModel.MapMany(oils)
            : default;
    }

    public async Task<IEnumerable<OilViewModel>> GetOilsByName(string name)
    {
        var oils = await _productRepository
            .GetByName<Oil>(name)
            .ConfigureAwait(false);

        if (oils.Any())
            return OilViewModel.MapMany(oils.ToList());

        return Enumerable.Empty<OilViewModel>();
    }

    public async Task<(OilViewModel result, string error)> Create(AddOilViewModel model)
    {
        var oil = (Oil)model;

        if (!oil.IsValid)
            return (default, oil.GetFirstNotificationMessage());

        if (await _productRepository.CreateAsync(oil).ConfigureAwait(false))
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

        if (await _productRepository.UpdateAsync(updatedOil).ConfigureAwait(false))
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

        var oils = await _productRepository.GetAllAsync<Oil>().ConfigureAwait(false);

        if (!oils?.Any() ?? true)
            return (true, "Não existem lubrificantes cadastrados.");

        bool sucess = true;
        string message = string.Empty;

        _unitOfWork.BeginTransaction();

        foreach (var oil in oils)
        {
            oil.ChangeDefaultPriceByAbsoluteValue(absoluteValue);
            if (await _productRepository.UpdateDefaultPriceAsync(oil.Id, oil.DefaultPrice).ConfigureAwait(false)) continue;
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

        var oils = await _productRepository.GetAllAsync<Oil>().ConfigureAwait(false);

        if (!oils?.Any() ?? true)
            return (true, "Não existem lubrificantes cadastrados.");

        bool sucess = true;
        string message = string.Empty;

        _unitOfWork.BeginTransaction();

        foreach (var oil in oils)
        {
            oil.ChangeDefaultPriceByPercentage(percentage);
            if (await _productRepository.UpdateDefaultPriceAsync(oil.Id, oil.DefaultPrice).ConfigureAwait(false)) continue;
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
        var product = await _productRepository.GetAsync(id).ConfigureAwait(false);

        if (product is null)
            return (false, $"Produto com id '{id}' não encontrado.");

        bool deleted = await _productRepository.DeleteAsync(id).ConfigureAwait(false);

        return deleted
            ? (true, string.Empty)
            : (false, $"Falha ao deletar produto: {product.Name}.");
    }
}
