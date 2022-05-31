using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.ViewModels.OilViewModel;
using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Implementation
{
    public class OilService : IOilService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public OilService(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork, 
            NotificationContext notification)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<IEnumerable<OilViewModel>> GetAll()
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

        public async Task<IEnumerable<OilViewModel>> Get(int page, int quantity)
        {
            var oils = await _productRepository.GetOils(page, quantity);

            return oils?.Any() ?? false
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<OilViewModel> Get(Guid id)
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

        public async Task<IEnumerable<OilViewModel>> GetByName(string name)
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

        public async Task<OilViewModel> Create(AddOilViewModel model)
        {
            var oil = (Oil)model;
            _notification.AddNotifications(oil.Notifications);

            if (!_notification.IsValid)
                return default;

            if (await _productRepository.Create(oil))
                return (OilViewModel)oil;

            _notification.AddNotification("Id", "Falha ao adicionar lubrificante.");
            return default;
        }

        public async Task<OilViewModel> Update(Guid id, OilViewModel model)
        {
            if (id != model.Id)
            {
                _notification.AddNotification("Id", "Lubrificante não encontrado.");
                return default;
            }

            var updatedOil = (Oil)model;
            _notification.AddNotifications(updatedOil.Notifications);

            if (!_notification.IsValid)
                return default;

            _unitOfWork.BeginTransaction();

            if (await _productRepository.Update(updatedOil))
            {
                _unitOfWork.Commit();
                return updatedOil;
            }

            _unitOfWork.Rollback();

            _notification.AddNotification("Id", "Falha ao atualizar lubrificante.");
            return default;
        }

        public async Task<(bool Sucess, string Message)> ChangePriceOfAllOilsByAbsoluteValue(decimal absoluteValue)
        {
            if(absoluteValue == 0)
                return (false, "Valor absoluto deve ser diferente de zero.");

            var oils = await _productRepository.GetAllOils();

            if (!oils?.Any() ?? true)
                return (true, "Não existem lubrificantes cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var oil in oils)
            {
                oil.ChangePriceByAbsoluteValue(absoluteValue);
                if (await _productRepository.UpdatePrice(oil.Id, oil.Price)) continue;
                else
                {
                    sucess = false;
                    message = $"Falha ao atualizar preço do lubrificante: {oil.Name}.";
                    _unitOfWork.Rollback();
                    break;
                } 
            }

            if(sucess) _unitOfWork.Commit();

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
                oil.ChangePriceByPercentage(percentage);
                if (await _productRepository.UpdatePrice(oil.Id, oil.Price)) continue;
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

        public void Dispose() => _productRepository.Dispose();
    }
}