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
        private readonly IOilRepository _oilRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public OilService(
            IOilRepository oilRepository,
            IUnitOfWork unitOfWork, 
            NotificationContext notification)
        {
            _oilRepository = oilRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public async Task<IEnumerable<OilViewModel>> Get(int page, int quantity)
        {
            var oils = await _oilRepository.Get(page, quantity);

            return oils.Any()
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<OilViewModel> Get(Guid id)
        {
            return await _oilRepository.Get(id);
        }

        public async Task<IEnumerable<OilViewModel>> Get(OilType type)
        {
            var oils = await _oilRepository.Get(type);

            return oils.Any()
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> GetByName(string name)
        {
            var oils = await _oilRepository.GetByName(name);

            return oils.Any()
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<IEnumerable<OilViewModel>> GetByViscosity(string viscosity)
        {
            var oils = await _oilRepository.GetByViscosity(viscosity);

            return oils.Any()
                ? OilViewModel.MapMany(oils)
                : default;
        }

        public async Task<OilViewModel> Create(AddOilViewModel model)
        {
            var oil = (Oil)model;
            _notification.AddNotifications(oil.Notifications);

            if (!_notification.IsValid)
                return default;

            if (await _oilRepository.Create(oil))
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
            _notification.AddNotifications(updatedOil);

            if (!_notification.IsValid)
                return default;

            _unitOfWork.BeginTransaction();

            if (await _oilRepository.Update(updatedOil))
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

            var oils = await _oilRepository.GetAll();

            if (!oils.Any())
                return (true, "Não existem lubrificantes cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var oil in oils)
            {
                oil.ChangePriceByAbsoluteValue(absoluteValue);
                if (await _oilRepository.Update(oil)) continue;
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

            var oils = await _oilRepository.GetAll();

            if (!oils.Any())
                return (true, "Não existem lubrificantes cadastrados.");

            bool sucess = true;
            string message = string.Empty;

            _unitOfWork.BeginTransaction();

            foreach (var oil in oils)
            {
                oil.ChangePriceByPercentage(percentage);
                if (await _oilRepository.Update(oil)) continue;
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

        public async Task<(bool Sucess, string Message)> Delete(Guid id)
        {
            var oil = await _oilRepository.Get(id);

            if (oil is null)
                return (false, $"Não existe lubrificante com Id: {id}.");

            _unitOfWork.BeginTransaction();

            if (await _oilRepository.Delete(id))
            {
                _unitOfWork.Commit();
                return (true, string.Empty);
            }

            _unitOfWork.Rollback();
            return (false, string.Empty);
        }

        public void Dispose() => _oilRepository.Dispose();   
    }
}