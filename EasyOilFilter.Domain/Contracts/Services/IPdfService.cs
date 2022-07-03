namespace EasyOilFilter.Domain.Contracts.Services
{
    public interface IPdfService
    {
        byte[] CreatePdf(string htmlContent);
    }
}