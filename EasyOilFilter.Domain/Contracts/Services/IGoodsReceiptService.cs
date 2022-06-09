using EasyOilFilter.Domain.ViewModels.GoodsReceiptViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    internal interface IGoodsReceiptService : IDisposable
    {
        Task<IEnumerable<GoodsReceiptViewModel>> Get(DateTime date);
        Task<(bool sucess, string message)> Create(AddGoodsReceiptViewModel model);
        Task<(bool sucess, string message)> Cancel(GoodsReceiptViewModel model);
    }
}