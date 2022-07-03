namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IReportService
    {
        Task<(bool saved, string path, string errorMessage)> SaveSaleReport(DateTime startDate, DateTime finalDate);
    }
}