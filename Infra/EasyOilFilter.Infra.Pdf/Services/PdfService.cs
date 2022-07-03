using EasyOilFilter.Domain.Contracts.Services;
using iText.Html2pdf;
using iText.Html2pdf.Resolver.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;

namespace EasyOilFilter.Infra.Pdf.Services
{
    public class PdfService : IPdfService
    {
        public byte[] CreatePdf(string htmlContent)
        {
            using var memoryStream = new MemoryStream();
            var fontProvider = new DefaultFontProvider(false, true, false);
            var converterProperties = new ConverterProperties();
            converterProperties.SetFontProvider(fontProvider);

            using var writer = new PdfWriter(memoryStream);
            using (var pdf = new PdfDocument(writer))
            {
                pdf.SetDefaultPageSize(PageSize.A4);
                HtmlConverter.ConvertToPdf(htmlContent, pdf, converterProperties);
                pdf.Close();
                return memoryStream.ToArray();
            }
        }
    }
}