using EasyOilFilter.Domain.Enums;

namespace EasyOilFilter.Domain.Contracts.Services;

public interface IReportService
{
    Task<(bool saved, string path, string errorMessage)> SaveReport(DateTime startDate, DateTime finalDate, ReportType reportType);
}