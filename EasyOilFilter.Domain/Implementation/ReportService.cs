using EasyOilFilter.Domain.Contracts.Repositories;
using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Entities.Reports;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using System.Text;

namespace EasyOilFilter.Domain.Implementation;

public class ReportService : IReportService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IPdfService _pdfService;

    public ReportService(ISaleRepository saleRepository, IPdfService pdfService)
    {
        _saleRepository = saleRepository;
        _pdfService = pdfService;
    }

    public async Task<(bool saved, string path, string errorMessage)> SaveReport(DateTime startDate, DateTime finalDate, ReportType reportType)
    {
        try
        {
            string bodyContent = await GetBodyContent(startDate, finalDate, reportType);
            string htmlContent = GetHtmlContent(bodyContent, reportType);

            var pdf = _pdfService.CreatePdf(htmlContent);

            string fileName = $"resumo_{startDate:dd-MM-yy}_{finalDate:dd-MM-yy}.pdf";

            string path = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "relatorios",
                reportType.GetDescription()
                );

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string pathFile = Path.Combine(path, fileName);

            File.WriteAllBytes(pathFile, pdf);

            return (true, pathFile, string.Empty);
        }
        catch (Exception ex)
        {
            return (false, string.Empty, $"Falha ao gerar relatório de vendas. Detalhes: {ex.Message}");
        }
    }

    private string GetHtmlContent(string bodyContent, ReportType reportType) =>
        @$"
                <!doctype html>
                <html lang=""en"">
                  <head>
                    <meta charset=""utf-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1, shrink-to-fit=no"">
                    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/css/bootstrap.min.css"" integrity=""sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO"" crossorigin=""anonymous"">
                    <title>Relatório de {reportType.GetDescription()}</title>
                  </head>
                  <body>
                    {bodyContent}
                    <script src=""https://code.jquery.com/jquery-3.3.1.slim.min.js"" integrity=""sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.14.3/dist/umd/popper.min.js"" integrity=""sha384-ZMP7rVo3mIykV+2+9J3UJ46jBk0WLaUAdn689aCwoqbBJiSnjAK/l8WvCWPIPm49"" crossorigin=""anonymous""></script>
                    <script src=""https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/js/bootstrap.min.js"" integrity=""sha384-ChfqqxuZUCnJSK3+MXmPNIyE6ZbWh2IMqE241rYiqJxyMiZ6OW/JmZQ5stwEULTy"" crossorigin=""anonymous""></script>
                  </body>
                </html>
            ";

    private async Task<string> GetBodyContent(DateTime startDate, DateTime finalDate, ReportType reportType)
    {
        switch (reportType)
        {
            case ReportType.Sales:
                return await GetSalesBodyContent(startDate, finalDate);
            case ReportType.SoldItems:
                return await GetSoldItemsBodyContent(startDate, finalDate);
            default:
                return string.Empty;
        }
    }

    private async Task<string> GetSoldItemsBodyContent(DateTime startDate, DateTime finalDate)
    {
        var soldItems = await _saleRepository.GetSoldItems(startDate, finalDate);

        return
            $@"
                <div class=""w-75 mx-auto text-center"">
                    <h1>Relatório de itens vendidos</h1> 
                    <div>
                        <h6 style=""margin:0"">{startDate:dd/MM/yy} até {finalDate:dd/MM/yy}</h6> 
                    </div>
                    <br>
                    {GetTableSoldItems(soldItems)}                 
                </div>
                ";
    }

    private async Task<string> GetSalesBodyContent(DateTime startDate, DateTime finalDate)
    {
        var report = await _saleRepository.GetSaleReport(startDate, finalDate);

        return
            $@"
                <div class=""w-75 mx-auto text-center"">
                    <h1>Relatório de vendas</h1> 
                    <div>
                        <h6 style=""margin:0"">{startDate:dd/MM/yy} até {finalDate:dd/MM/yy}</h6> 
                        <h5 class=""font-weight-bold"">{report.TotalSales:C2}</h5>
                    </div>
                    <br>
                    {GetTableSalesGroupedByDate(report.SalesByDate)}
                    <br>
                    {GetTableSalesGroupedByPaymentMethod(report.SalesByPaymentMethod)}
                </div>
                ";
    }

    private string GetTableSoldItems(IEnumerable<SoldItemReport> soldItems)
    {
        var builder = new StringBuilder();

        foreach (var soldItem in soldItems)
        {
            builder.AppendLine(
                @$"
                        <tr>
                            <td scope=""row"">{soldItem.Name}</th>
                            <td>{soldItem.Quantity:N2}</td>
                        </tr>
                    ");
        }

        return
            $@"
                <table class=""table table-striped"">
                        <thead>
                            <tr>
                              <th style=""width:60%"" scope=""col"">Produto</th>
                              <th style=""width:40%"" scope=""col"">Total vendido</th>
                            </tr>
                        </thead>
                        <tbody>
                            {builder}
                        </tbody>
                    </table>
                ";
    }

    private string GetTableSalesGroupedByPaymentMethod(IEnumerable<SaleByPaymentMethod> salesByPaymentMethod)
    {
        var builder = new StringBuilder();

        foreach (var paymentMethodSales in salesByPaymentMethod)
        {
            builder.AppendLine(
                @$"
                        <tr>
                            <td scope=""row"">{paymentMethodSales.PaymentMethod.GetDescription()}</th>
                            <td>{paymentMethodSales.Total:C2}</td>
                        </tr>
                    ");
        }

        return
            $@"
                <table class=""table table-striped"">
                        <thead>
                            <tr>
                              <th style=""width:60%"" scope=""col"">Forma de pagamento</th>
                              <th style=""width:40%"" scope=""col"">Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {builder}
                        </tbody>
                    </table>
                ";
    }

    private string GetTableSalesGroupedByDate(IEnumerable<SaleByDate> salesByDate)
    {
        var builder = new StringBuilder();

        foreach (var sale in salesByDate)
        {
            builder.AppendLine(
                $@"
                        <tr>
                            <td scope=""row"">{sale.Date:dd/MM/yyyy}</th>
                            <td>{sale.Total:C2}</td>
                        </tr>
                    ");
        }

        return
            $@"
                <table class=""table table-striped"">
                        <thead>
                            <tr>
                                <th style=""width:60%"" scope=""col"">Data</th>
                                <th style=""width:40%"" scope=""col"">Vendas</th>
                            </tr>
                        </thead>
                        <tbody>
                            {builder}
                        </tbody>
                </table>
                ";
    }
}