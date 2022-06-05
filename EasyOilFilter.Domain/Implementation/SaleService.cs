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
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public SaleService(
            ISaleRepository saleRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
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

        public async Task<(bool sucess, string message)> Create(AddSaleViewModel model)
        {
            var sale = (Sale)model;

            _notification.AddNotifications(sale.Notifications);

            if (!_notification.IsValid)
                return (false, sale.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await AddHeader(sale);

            if(!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar venda.");
            }    

            success = await AddSaleItems(sale.Items);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar venda.");
            }

            var (successToReduceStock, errorMessage) = await ReduceStock(sale.Items);

            if (!successToReduceStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao adicionar venda. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }


        private async Task<bool> AddHeader(Sale sale)
        {
            return await _saleRepository.AddHeader(sale);
        }

        private async Task<bool> AddSaleItems(IEnumerable<SaleItem> items)
        {
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

        private async Task<(bool success, string errorMessage)> ReduceStock(IEnumerable<SaleItem> items)
        {
            bool success = true;
            string errorMessage = string.Empty;

            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                decimal soldAmount = items.FirstOrDefault(item => item.ProductId == product.Id).Quantity;
                
                if(soldAmount > product.StockQuantity)
                {
                    success = false;
                    errorMessage = 
                        $"Falha ao abater estoque do produto '{product.Name}'. " +
                        $"Quantidade decai para valor negativo.";

                    break;
                }

                product.ReduceStock(soldAmount);

                if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                    continue;
                else
                {
                    success = false;
                    errorMessage = $"Falha ao atualizar estoque do produto '{product.Name}'.";

                    break;
                }
            }

            return (success, errorMessage);
        }
    }
}