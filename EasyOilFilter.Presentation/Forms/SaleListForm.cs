using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleListForm : Form
    {
        private readonly ISaleService _saleService;

        public SaleListForm(ISaleService saleService)
        {
            _saleService = saleService;
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

            if (sales?.Any() ?? false)
            {
                DataGridView.DataSource = sales.ToList();
                LabelDate.Text = DateTimePickerSearch.Value.ToString("d");
                SetTotalLabel(sales.Sum(sale => sale.Total));
            }
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

        private async void ButtonAddSale_Click(object sender, EventArgs e)
        {
            //ToDo: Open screen of new Sale
            var mock = new AddSaleViewModel()
            {
                Description = "Gol",
                PaymentMethod = "Dinheiro",
                Total = 150.00m,
                Discount = 5,
                Date = DateTime.Now,
                Remarks = "Observações"
            };

            var oil = new AddSaleItemViewModel()
            {
                ProductId = Guid.Parse("95DAC815-5784-4C21-83D2-F794E4FC95F0"),
                ItemDescription = "Selenia Perform",
                UnitOfMeasurement = "Litro",
                Quantity = 3,
                UnitaryPrice = 48.35m,
                TotalItem = 3 * 48.35m
            };

            mock.Items = new List<AddSaleItemViewModel> { oil };

            var (sucess, message) = await _saleService.Create(mock);
        }
    }
}