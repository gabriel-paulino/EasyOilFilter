using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface ISaleService : IDisposable
    {
        Task<IEnumerable<SaleViewModel>> Get(DateTime date);
        Task<(bool sucess, string message)> Create(AddSaleViewModel model);
        Task<(bool sucess, string message)> Cancel(SaleViewModel model);
    }
}