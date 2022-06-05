using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleListForm : Form
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public SaleListForm(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
            InitializeComponent();
        }

        private async void SaleForm_Load(object sender, EventArgs e)
        {
            SetDateInfo(DateTime.Today);
            ConfigureGrid();

            var sales = await _saleService.Get(DateTime.Today);

            if (sales?.Any() ?? false)
            {
                DataGridView.DataSource = sales.ToList();               
                SetTotalLabel(sales.Sum(sale => sale.Total));
            }          
        }

        private async void ButtonSearch_Click(object sender, EventArgs e)
        {
            var sales = await _saleService.Get(DateTimePickerSearch.Value);
            SetDateLabel(DateTimePickerSearch.Value);

            if (sales?.Any() ?? false)
            {
                DataGridView.DataSource = sales.ToList();                
                SetTotalLabel(sales.Sum(sale => sale.Total));

                return;
            }
            ResetList();  
        }

        private void ResetList()
        {
            SetTotalLabel(default);
            DataGridView.DataSource = new List<SaleViewModel>();
        }

        private void SetDateInfo(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
            DateTimePickerSearch.Value = date;
        }

        private void SetTotalLabel(decimal total)
        {
            LabelTotal.Text = total.ToString("C2");
        }

        private void SetDateLabel(DateTime date)
        {
            LabelDate.Text = date.ToString("d");
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<SaleViewModel>();
            DataGridView.ReadOnly = true;
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["Discount"].Visible = false;
            DataGridView.Columns["Date"].Visible = false;
            DataGridView.Columns["Remarks"].Visible = false;
            DataGridView.Columns["Items"].Visible = false;
            DataGridView.Columns["Status"].Visible = false;
            DataGridView.Columns["Description"].HeaderText = "Descrição";
            DataGridView.Columns["PaymentMethod"].HeaderText = "Forma de pagamento";
            DataGridView.Columns["Total"].HeaderText = "Valor";
            DataGridView.Columns["Total"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["Description"].MinimumWidth = 310;
            DataGridView.Columns["PaymentMethod"].MinimumWidth = 155;
            DataGridView.Columns["Total"].MinimumWidth = 140;
            DataGridView.AutoResizeColumns();
        }

        private void ButtonAddSale_Click(object sender, EventArgs e)
        {
            using (var saleForm = new SaleForm(_saleService, _productService))
            {
                saleForm.ShowDialog();
            }
        }
    }
}