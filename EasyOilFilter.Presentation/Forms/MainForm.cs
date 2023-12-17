using Microsoft.Extensions.DependencyInjection;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonLubs_Click(object sender, EventArgs e)
        {
            using var oilForm = Program.ServiceProvider.GetRequiredService<OilForm>();
            oilForm.ShowDialog();
        }

        private void ButtonFilters_Click(object sender, EventArgs e)
        {
            using var filterForm = Program.ServiceProvider.GetRequiredService<FilterForm>();
            filterForm.ShowDialog();
        }

        private void ButtonSale_Click(object sender, EventArgs e)
        {
            using var saleListForm = Program.ServiceProvider.GetRequiredService<SaleListForm>();
            saleListForm.ShowDialog();
        }

        private void ButtonPurchase_Click(object sender, EventArgs e)
        {
            using var purchaseListForm = Program.ServiceProvider.GetRequiredService<PurchaseListForm>();
            purchaseListForm.ShowDialog();
        }

        private void ButtonReport_Click(object sender, EventArgs e)
        {
            using var reportForm = Program.ServiceProvider.GetRequiredService<ReportForm>();
            reportForm.ShowDialog();
        }
    }
}