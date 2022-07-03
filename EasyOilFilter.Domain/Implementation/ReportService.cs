using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Entities;
using EasyOilFilter.Domain.Extensions;
using System.Diagnostics;
using System.Text;

namespace EasyOilFilter.Domain.Implementation
{
    public class ReportService : IReportService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IPdfService _pdfService;

        public ReportService(ISaleRepository saleRepository, IPdfService pdfService)
        {
            _saleRepository = saleRepository;
            _pdfService = pdfService;
        }

        public async Task<(bool saved, string path, string errorMessage)> SaveSaleReport(DateTime startDate, DateTime finalDate)
        {
            try
            {
                var sales = await _saleRepository.Get(startDate, finalDate);

                string bodyContent =
                    $@"
                <div class=""w-75 mx-auto text-center"">
                    <h1>Relatório de vendas</h1> 
                    <div>
                        <h6 style=""margin:0"">{startDate:dd/MM/yy} até {finalDate:dd/MM/yy}</h6> 
                        <h5 class=""font-weight-bold"">{sales.Sum(sale => sale.Total):C2}</h5>
                    </div>
                    <br>
                    <table class=""table table-striped"">
                        <thead>
                            <tr>
                                <th style=""width:60%"" scope=""col"">Data</th>
                                <th style=""width:40%"" scope=""col"">Vendas</th>
                            </tr>
                        </thead>
                        <tbody>
                            {GetSalesGroupedByDateTableRows(sales)}
                        </tbody>
                    </table>
                    <br>
                    <table class=""table table-striped"">
                        <thead>
                            <tr>
                              <th style=""width:60%"" scope=""col"">Forma de pagamento</th>
                              <th style=""width:40%"" scope=""col"">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {GetSalesGroupedByPaymentMethodTableRows(sales)}
                        </tbody>
                    </table>
                </div>
                ";

                string htmlContent = GetHtmlContent(bodyContent);

                var pdf = _pdfService.CreatePdf(htmlContent);

                string fileName = $"resumo_vendas_{startDate:dd-MM-yy}_{finalDate:dd-MM-yy}.pdf";
                string basePatch = @"C:\relatórios\";

                string path = Path.Combine(basePatch, fileName);

                var file = new FileInfo(path);
                file.Directory?.Create();
                File.WriteAllBytes(file.FullName, pdf);

                return (true, path, string.Empty);
            }
            catch (Exception ex)
            {
                return (false, string.Empty, $"Falha ao gerar relatório de vendas. Detalhes: {ex.Message}.");
            }
        }

        private string GetSalesGroupedByPaymentMethodTableRows(IEnumerable<Sale> sales)
        {
            var builder = new StringBuilder();

            var salesGroupedByPaymentMethod = sales
                .OrderBy(sale => sale.Date)
                .GroupBy(sale => sale.PaymentMethod);

            foreach (var paymentMethodSales in salesGroupedByPaymentMethod)
            {
                builder.AppendLine(
                    @$"
                        <tr>
                            <td scope=""row"">{paymentMethodSales.Key.GetDescription()}</th>
                            <td>{paymentMethodSales.Sum(sale => sale.Total):C2}</td>
                        </tr>
                    ");
            }

            return builder.ToString();
        }

        private string GetSalesGroupedByDateTableRows(IEnumerable<Sale> sales)
        {
            var builder = new StringBuilder();

            var salesOrdenedAndGrouped = sales
                .OrderBy(sale => sale.Date)
                .GroupBy(sale => sale.Date.Date);

            foreach (var dateSales in salesOrdenedAndGrouped)
            {
                builder.AppendLine(
                    @$"
                        <tr>
                            <td scope=""row"">{dateSales.Key:dd/MM/yyyy}</th>
                            <td>{dateSales.Sum(sale => sale.Total):C2}</td>
                        </tr>
                    ");
            }

            return builder.ToString();
        }

        private string GetHtmlContent(string bodyContent) =>
            @$"
                <!doctype html>
                <html lang=""en"">
                  <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/css/bootstrap.min.css"" integrity=""sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO"" crossorigin=""anonymous"">
                    <title>Relatório de vendas</title>
                  </head>
                  <body>
                    {bodyContent}
                    <script src=""https://code.jquery.com/jquery-3.3.1.slim.min.js"" integrity=""sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.14.3/dist/umd/popper.min.js"" integrity=""sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/js/bootstrap.min.js"" integrity=""sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy"" crossorigin=""anonymous""></script>
                  </body>
                </html>
            ";
    }
}