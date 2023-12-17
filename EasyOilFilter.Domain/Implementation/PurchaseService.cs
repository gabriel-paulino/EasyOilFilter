using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Contracts.UoW;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;

namespace EasyOilFilter.Domain.Implementation
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService
        (
            IPurchaseRepository goodsReceiptRepository,
            IProductRepository productRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork
        )
        {
            _purchaseRepository = goodsReceiptRepository;
            _productRepository = productRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public void Dispose() => _purchaseRepository.Dispose();

        public async Task<IEnumerable<PurchaseViewModel>> Get(DateTime startDate, DateTime endDate)
        {
            var purchases = await _purchaseRepository.Get(startDate, endDate);

            return purchases?.Any() ?? false
                ? PurchaseViewModel.MapMany(purchases)
                : default;
        }

        public async Task<(bool sucess, string message)> Create(AddPurchaseViewModel model)
        {
            var purchase = (Purchase)model;

            if (!purchase.IsValid)
                return (false, purchase.GetFirstNotificationMessage());

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

            var (successToIncreseStock, errorMessage) = await IncreseStock(purchase.Items);

            if (!successToIncreseStock)
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

            if (!purchase.IsValid)
                return (false, purchase.GetFirstNotificationMessage());

            _unitOfWork.BeginTransaction();

            var (wasCanceled, cancelErrorMessage) = await Cancel(purchase.Id);

            if (!wasCanceled)
            {
                _unitOfWork.Rollback();
                return (false, cancelErrorMessage);
            }

            var (successToReversalStock, errorMessage) = await ReduceStock(purchase.Items);

            if (!successToReversalStock)
            {
                _unitOfWork.Rollback();
                return (false, $"Falha ao cancelar compra. Detalhes: {errorMessage}");
            }

            _unitOfWork.Commit();

            return (true, string.Empty);
        }

        private async Task<(bool wasCanceled, string errorMessage)> Cancel(Guid id)
        {
            var payments = await _paymentRepository.GetPaidByPurchaseId(id);

            if (payments?.Any() ?? false)
                return (false, 
                    $"Não é possível realizar o cancelamento dessa compra.{Environment.NewLine}" +
                    $"Existem pagamentos efetuados.");

            bool wasCanceled = await _purchaseRepository.Cancel(id);

            if (wasCanceled)
                return (true, string.Empty);

            return (false, "Erro ao cancelar compra.");
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
            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                var canceledPurchaseItem = items.FirstOrDefault(item => item.ProductId == product.Id);

                if (canceledPurchaseItem.Quantity > product.StockQuantity)
                    return (false,
                        $"Falha ao abater estoque do produto '{product.Name}'. " +
                        $"Quantidade decai para valor negativo.");

                product.ReduceStock(canceledPurchaseItem.Quantity, canceledPurchaseItem.UnitOfMeasurement);

                if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                    continue;
                else
                    return (false, $"Falha ao decrementar estoque. Produto '{product.Name}'.");

            }

            return (true, string.Empty);
        }

        private async Task<(bool success, string errorMessage)> IncreseStock(IEnumerable<PurchaseItem> items)
        {
            var products = await _productRepository.Get(items.Select(x => x.ProductId));

            foreach (var product in products)
            {
                var purchasedItem = items.FirstOrDefault(item => item.ProductId == product.Id);

                product.IncreseStock(purchasedItem.Quantity, purchasedItem.UnitOfMeasurement);

                if (await _productRepository.SetStockQuantity(product.Id, product.StockQuantity))
                    continue;
                else
                    return (false, $"Falha ao incrementar estoque. Produto '{product.Name}'.");
            }

            return (true, string.Empty);
        }
    }
}