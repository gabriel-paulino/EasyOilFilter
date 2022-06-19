using EasyOilFilter.Domain.ViewModels.PurchaseViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IPurchaseService : IDisposable
    {
        Task<IEnumerable<PurchaseViewModel>> Get(DateTime date);
        Task<(bool sucess, string message)> Create(AddPurchaseViewModel model);
        Task<(bool sucess, string message)> Cancel(PurchaseViewModel model);
    }
}