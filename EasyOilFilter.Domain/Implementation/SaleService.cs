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
                ? SaleViewModel.MapMany(sales.OrderBy(sale => sale.Date))
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

            if (!success)
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

        public async Task<(bool sucess, string message)> Cancel(SaleViewModel model)
        {
            var sale = (Sale)model;

            _notification.AddNotifications(sale.Notifications);

            if (!_notification.IsValid)
                return (false, sale.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await Cancel(sale.Id);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao cancelar venda.");
            }

            var (successToReversalStock, errorMessage) = await ReversalStock(sale.Items);

            if (!successToReversalStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao cancelar venda. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        private async Task<bool> Cancel(Guid id)
        {
            return await _saleRepository.Cancel(id);
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
            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                var soldItems = items.Where(item => item.ProductId == product.Id);

                foreach (var soldItem in soldItems)
                {
                    decimal quantitySoldInDefaultUoM = product.GetQuantityInDefaultUoM(soldItem.Quantity, soldItem.UnitOfMeasurement);

                    if (quantitySoldInDefaultUoM > product.StockQuantity)
                        return (false,
                            $"Falha ao abater estoque do produto '{product.Name}'. " +
                            $"Quantidade decai para valor negativo.");

                    product.ReduceStock(soldItem.Quantity, soldItem.UnitOfMeasurement);

                    if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                        continue;
                    else
                        return (false, $"Falha ao decrementar estoque. Produto '{product.Name}'.");
                }
            }

            return (true, string.Empty);
        }

        private async Task<(bool success, string errorMessage)> ReversalStock(IEnumerable<SaleItem> items)
        {
            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                var canceledSoldItems = items.Where(item => item.ProductId == product.Id);

                foreach (var canceledSoldItem in canceledSoldItems)
                {
                    product.IncreseStock(canceledSoldItem.Quantity, canceledSoldItem.UnitOfMeasurement);

                    if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                        continue;
                    else
                        return (false, $"Falha ao incrementar estoque. Produto '{product.Name}'.");
                }
            }

            return (true, string.Empty);
        }
    }
}