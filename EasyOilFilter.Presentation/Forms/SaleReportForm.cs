using EasyOilFilter.Domain.Contracts.Services;
using System.Diagnostics;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleReportForm : Form
    {
        private readonly IReportService _reportService;

        public SaleReportForm(IReportService reportService)
        {
            _reportService = reportService;
            InitializeComponent();
        }

        private async void ButtonGenerate_Click(object sender, EventArgs e)
        {
            var startDate = StartDatePicker.Value;
            var finalDate = FinalDatePicker.Value;

            if (startDate > finalDate)
            {
                MessageBox.Show("A 'Data inicial' não pode ser maior que a 'Data final'.");
                return;
            }

            var (saved, path, errorMessage) = await _reportService.SaveSaleReport(startDate, finalDate);

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