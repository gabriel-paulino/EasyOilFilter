using EasyOilFilter.Domain.Entities;

namespace EasyOilFilter.Domain.Contracts.Repositories
{
    public interface IGoodsReceiptRepository : IDisposable
    {
        Task<IEnumerable<GoodsReceipt>> Get(DateTime date);
        Task<GoodsReceipt> Get(Guid id);
        Task<bool> AddHeader(GoodsReceipt sale);
        Task<bool> AddItem(GoodsReceiptItem item);
        Task<bool> Cancel(Guid id);
    }
}