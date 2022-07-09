using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using System.Diagnostics;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class ReportForm : Form
    {
        private readonly IReportService _reportService;

        public ReportForm(IReportService reportService)
        {
            _reportService = reportService;
            InitializeComponent();
        }

        private async void ButtonSaleReport_Click(object sender, EventArgs e)
        {
            var startDate = StartDatePicker.Value;
            var finalDate = FinalDatePicker.Value;

            if (startDate > finalDate)
            {
                MessageBox.Show("A 'Data inicial' não pode ser maior que a 'Data final'.");
                return;
            }

            var (saved, path, errorMessage) = await _reportService.SaveReport(startDate, finalDate, ReportType.Sales);

            MessageBox.Show(saved
                ? $"Relatório salvo com sucesso. Disponível em: '{path}'."
                : errorMessage);

            if (saved)
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }

        private async void ButtonSoldItemReport_Click(object sender, EventArgs e)
        {
            var startDate = StartDatePicker.Value;
            var finalDate = FinalDatePicker.Value;

            if (startDate > finalDate)
            {
                MessageBox.Show("A 'Data inicial' não pode ser maior que a 'Data final'.");
                return;
            }

            var (saved, path, errorMessage) = await _reportService.SaveReport(startDate, finalDate, ReportType.SoldItems);

            MessageBox.Show(saved
                ? $"Relatório salvo com sucesso. Disponível em: '{path}'."
                : errorMessage);

            if (saved)
                Process.Start(new ProcessStartInfo { FileName = path, UseShellExecute = true });
        }

        private void SaleReportForm_Load(object sender, EventArgs e)
        {
            InitializeStartDate();
        }

        private void InitializeStartDate()
        {
            var today = DateTime.Today;
            StartDatePicker.Value = new DateTime(today.Year, today.Month, 1);
        }
    }
}