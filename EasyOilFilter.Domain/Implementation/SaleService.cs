using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public SaleService(
            ISaleRepository saleRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public void Dispose() => _saleRepository.Dispose();

        public async Task<IEnumerable<SaleViewModel>> Get(DateTime date)
        {
            var sales = await _saleRepository.Get(date);
                       
            return sales?.Any() ?? false
                ? SaleViewModel.MapMany(sales)
                : default;
        }

        public async Task<(bool sucess, string message)> Create(SaleViewModel model)
        {
            var sale = (Sale)model;
            _notification.AddNotifications(sale.Notifications);

            if (!_notification.IsValid)
                return (false, sale.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool sucess = await AddHeader(sale);

            if(!sucess)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar venda.");
            }    

            sucess = await AddSaleItems(sale.Items);

            if (!sucess)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar venda.");
            }


            //ToDo: Reduzir quantidade em estoque dos itens

            _unitOfWork.Commit();

            return (true, string.Empty);
        }


        private async Task<bool> AddHeader(Sale sale)
        {
            return await _saleRepository.AddHeader(sale);
        }

        private async Task<bool> AddSaleItems(IEnumerable<SaleItem> items)
        {
            //ToDo - Verificar possivel simplificação

            bool success = true;

            foreach (var item in items)
            {
                if (await _saleRepository.AddItem(item))
                    continue;
                else
                {
                    success = false;
                    break;
                }
            }

            return success;
        }
    }
}