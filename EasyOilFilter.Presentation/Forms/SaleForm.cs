using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleForm : Form
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;

        public SaleForm(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
            InitializeComponent();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            ConfigureComponents();
        }

        private void ConfigureComponents()
        {
            SetDateTimePicker(DateTime.Today);
            LoadPaymentMethodComboBox();
            ConfigureGrid();
        }

        private void SetDateTimePicker(DateTime date)
        {
            DateTimePickerSaleDate.Value = date;
        }

        private void LoadPaymentMethodComboBox()
        {
            foreach (var type in EnumUtility.EnumToList<PaymentMethod>())
                ComboBoxPaymentMethod.Items.Add(type.GetDescription());

            ComboBoxPaymentMethod.SelectedIndex = 0;
        }

        private void ConfigureGrid()
        {
            DataGridView.DataSource = new List<SaleItemViewModel>() { new SaleItemViewModel() };
            DataGridView.Columns["Id"].Visible = false;
            DataGridView.Columns["SaleId"].Visible = false;
            DataGridView.Columns["ProductId"].Visible = false;
            DataGridView.Columns["ItemDescription"].HeaderText = "Item";
            DataGridView.Columns["Quantity"].HeaderText = "Quantidade";
            DataGridView.Columns["UnitOfMeasurement"].HeaderText = "Embalagem";
            DataGridView.Columns["UnitaryPrice"].HeaderText = "Preço unitário";
            DataGridView.Columns["TotalItem"].HeaderText = "Total item";
            DataGridView.Columns["UnitaryPrice"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["TotalItem"].DefaultCellStyle.Format = "C2";
            DataGridView.Columns["ItemDescription"].MinimumWidth = 310;
            DataGridView.Columns["UnitaryPrice"].MinimumWidth = 110;
            DataGridView.Columns["TotalItem"].MinimumWidth = 110;
            DataGridView.Columns["TotalItem"].ReadOnly = true;
            DataGridView.Columns["UnitaryPrice"].ReadOnly = true;
            DataGridView.Columns["UnitOfMeasurement"].ReadOnly = true;
            DataGridView.AutoResizeColumns();
        }

        private void DataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs cell)
        {
            if (IsItemQuantityCell(cell))
            {
                cell.Cancel = !decimal.TryParse(cell.FormattedValue.ToString(), out decimal _);
            }
        }

        private void DataGridView_CellValidated(object sender, DataGridViewCellEventArgs cell)
        {
            switch (cell.ColumnIndex)
            {
                case 3:
                    FillSaleItemLine(cell.RowIndex);
                    break;
                case 4:
                    UpdateTotalItem(cell.RowIndex);
                    break;
                default:
                    break;
            }
        }

        private async void FillSaleItemLine(int rowIndex)
        {
            string name = GetCellValue(rowIndex, "ItemDescription")?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(name))
                return;

            var products = await _productService.GetProducts(name);

            if (products.Count() > 1)
            {
                var dataSource = products.Select(product => (SaleItemViewModel)product);
                var selectedProduct = GetSelectedProduct(dataSource);

                if (selectedProduct is null)
                    return;

                SetSaleItemOnDataGridView(rowIndex, selectedProduct);
            }

            else if (products.Count() == 1)
            {
                SetSaleItemOnDataGridView(rowIndex, (SaleItemViewModel)products.FirstOrDefault());
            }

            UpdateTotalItem(rowIndex);
        }

        private void UpdateTotalItem(int rowIndex)
        {
            decimal unitaryPrice = (decimal)GetCellValue(rowIndex, "UnitaryPrice");
            decimal quantity = (decimal)GetCellValue(rowIndex, "Quantity");

            SetCellValue(rowIndex, "TotalItem", unitaryPrice * quantity);
        }

        private SaleItemViewModel? GetSelectedProduct(IEnumerable<SaleItemViewModel> dataSource)
        {
            using (var chooseFromList = new SelectProductModalForm())
            {
                chooseFromList.DataSource = dataSource;
                chooseFromList.ShowDialog();

                return chooseFromList.SelectedData;
            }
        }

        private void SetSaleItemOnDataGridView(int rowIndex, SaleItemViewModel selectedProduct)
        {
            SetCellValue(rowIndex, "ProductId", selectedProduct.ProductId);
            SetCellValue(rowIndex, "ItemDescription", selectedProduct.ItemDescription);
            SetCellValue(rowIndex, "UnitOfMeasurement", selectedProduct.UnitOfMeasurement);
            SetCellValue(rowIndex, "UnitaryPrice", selectedProduct.UnitaryPrice);
        }

        private object GetCellValue(int rowIndex, string column)
        {
            return DataGridView
            .Rows[rowIndex]
            .Cells[column]
            .Value;
        }

        private void SetCellValue(int rowIndex, string column, object value)
        {
            DataGridView
            .Rows[rowIndex]
            .Cells[column]
            .Value = value;
        }

        private bool IsItemQuantityCell(DataGridViewCellValidatingEventArgs cell) => cell.ColumnIndex == 4;
    }
}