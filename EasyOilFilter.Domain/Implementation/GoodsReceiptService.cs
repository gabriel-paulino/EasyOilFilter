using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Shared.Contexts;
using EasyOilFilter.Domain.ViewModels.GoodsReceiptViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class GoodsReceiptService : IGoodsReceiptService
    {
        private readonly IGoodsReceiptRepository _goodsReceiptRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NotificationContext _notification;

        public GoodsReceiptService(
            IGoodsReceiptRepository goodsReceiptRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notification)
        {
            _goodsReceiptRepository = goodsReceiptRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public void Dispose() => _goodsReceiptRepository.Dispose();

        public async Task<IEnumerable<GoodsReceiptViewModel>> Get(DateTime date)
        {
            var goodsReceipts = await _goodsReceiptRepository.Get(date);

            return goodsReceipts?.Any() ?? false
                ? GoodsReceiptViewModel.MapMany(goodsReceipts)
                : default;
        }

        public async Task<(bool sucess, string message)> Create(AddGoodsReceiptViewModel model)
        {
            var goodsReceipt = (GoodsReceipt)model;

            _notification.AddNotifications(goodsReceipt.Notifications);

            if (!_notification.IsValid)
                return (false, goodsReceipt.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await AddHeader(goodsReceipt);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar recebimento de mercadoria.");
            }

            success = await AddGoodsReceiptItems(goodsReceipt.Items);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao adicionar recebimento de mercadoria.");
            }

            var (successToReduceStock, errorMessage) = await ReduceStock(goodsReceipt.Items);

            if (!successToReduceStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao adicionar recebimento de mercadoria. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        public async Task<(bool sucess, string message)> Cancel(GoodsReceiptViewModel model)
        {
            var goodsReceipt = (GoodsReceipt)model;

            _notification.AddNotifications(goodsReceipt.Notifications);

            if (!_notification.IsValid)
                return (false, goodsReceipt.Notifications.FirstOrDefault().Message);

            _unitOfWork.BeginTransaction();

            bool success = await Cancel(goodsReceipt.Id);

            if (!success)
            {
                _unitOfWork.Rollback();
                return (false, "Falha ao cancelar recebimento de mercadoria.");
            }

            var (successToReversalStock, errorMessage) = await ReversalStock(goodsReceipt.Items);

            if (!successToReversalStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao cancelar recebimento de mercadoria. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        private async Task<bool> Cancel(Guid id)
        {
            return await _goodsReceiptRepository.Cancel(id);
        }

        private async Task<bool> AddHeader(GoodsReceipt goodsReceipt)
        {
            return await _goodsReceiptRepository.AddHeader(goodsReceipt);
        }

        private async Task<bool> AddGoodsReceiptItems(IEnumerable<GoodsReceiptItem> items)
        {
            bool success = true;

            foreach (var item in items)
            {
                if (await _goodsReceiptRepository.AddItem(item))
                    continue;
                else
                {
                    success = false;
                    break;
                }
            }

            return success;
        }

        private async Task<(bool success, string errorMessage)> ReduceStock(IEnumerable<GoodsReceiptItem> items)
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

        private async Task<(bool success, string errorMessage)> ReversalStock(IEnumerable<GoodsReceiptItem> items)
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