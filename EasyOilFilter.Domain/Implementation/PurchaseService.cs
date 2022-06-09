using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public PurchaseService(
            IPurchaseRepository goodsReceiptRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _purchaseRepository = goodsReceiptRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public void Dispose() => _purchaseRepository.Dispose();

        public async Task<IEnumerable<PurchaseViewModel>> Get(DateTime date)
        {
            var purchases = await _purchaseRepository.Get(date);

            return purchases?.Any() ?? false
                ? PurchaseViewModel.MapMany(purchases)
                : default;
        }

        public async Task<(bool sucess, string message)> Create(AddPurchaseViewModel model)
        {
            var purchase = (Purchase)model;

            _notification.AddNotifications(purchase.Notifications);

            if (!_notification.IsValid)
                return (false, purchase.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await AddHeader(purchase);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar compra.");
            }

            success = await AddPurchaseItems(purchase.Items);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar compra.");
            }

            var (successToReduceStock, errorMessage) = await ReduceStock(purchase.Items);

            if (!successToReduceStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao adicionar compra. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        public async Task<(bool sucess, string message)> Cancel(PurchaseViewModel model)
        {
            var purchase = (Purchase)model;

            _notification.AddNotifications(purchase.Notifications);

            if (!_notification.IsValid)
                return (false, purchase.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await Cancel(purchase.Id);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao cancelar compra.");
            }

            var (successToReversalStock, errorMessage) = await ReversalStock(purchase.Items);

            if (!successToReversalStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao cancelar compra. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        private async Task<bool> Cancel(Guid id)
        {
            return await _purchaseRepository.Cancel(id);
        }

        private async Task<bool> AddHeader(Purchase purchase)
        {
            return await _purchaseRepository.AddHeader(purchase);
        }

        private async Task<bool> AddPurchaseItems(IEnumerable<PurchaseItem> items)
        {
            bool success = true;

            foreach (var item in items)
            {
                if (await _purchaseRepository.AddItem(item))
                    continue;
                else
                {
                    success = false;
                    break;
                }
            }

            return success;
        }

        private async Task<(bool success, string errorMessage)> ReduceStock(IEnumerable<PurchaseItem> items)
        {
            bool success = true;
            string errorMessage = string.Empty;

            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                decimal soldAmount = items.FirstOrDefault(item => item.ProductId == product.Id).Quantity;

                if (soldAmount > product.StockQuantity)
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
                    errorMessage = $"Falha ao atualizar estoque. Produto '{product.Name}'.";

                    break;
                }
            }

            return (success, errorMessage);
        }

        private async Task<(bool success, string errorMessage)> ReversalStock(IEnumerable<PurchaseItem> items)
        {
            bool success = true;
            string errorMessage = string.Empty;

            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                decimal soldAmount = items.FirstOrDefault(item => item.ProductId == product.Id).Quantity;

                product.IncreseStock(soldAmount);

                if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                    continue;
                else
                {
                    success = false;
                    errorMessage = $"Falha ao estornar estoque. Produto '{product.Name}'.";

                    break;
                }
            }

            return (success, errorMessage);
        }
    }
}