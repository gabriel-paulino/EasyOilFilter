using EasyOilFilter.Domain.Contracts.Services;
using EasyOilFilter.Domain.Enums;
using EasyOilFilter.Domain.Extensions;
using EasyOilFilter.Domain.Shared.Utils;
using EasyOilFilter.Domain.ViewModels;
using EasyOilFilter.Domain.ViewModels.SaleViewModel;
using EasyOilFilter.Presentation.Enums;
using System.ComponentModel;
using System.Globalization;

namespace EasyOilFilter.Presentation.Forms
{
    public partial class SaleForm : Form
    {
        private readonly ISaleService _saleService;
        private readonly IProductService _productService;
        public SaleViewModel Model { get; set; }
        public FormMode Mode { get; set; }
        public bool IsAdd { get; private set; }
        public bool IsCanceled { get; private set; }
        public DateTime Date { get; private set; }

        public SaleForm(ISaleService saleService, IProductService productService)
        {
            _saleService = saleService;
            _productService = productService;
            Model = new SaleViewModel();
            IsAdd = false;
            IsCanceled = false;
            InitializeComponent();
        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            ConfigureComponents();

            if (Mode == FormMode.Add)
            {
                ButtonProcessSale.Text = "Adicionar";

                SetDateTimePicker(DateTime.Today);
                SelectPaymentMethod(PaymentMethod.All);
                DisableFooterFields();

                return;
            }

            ButtonProcessSale.Text = "Cancelar venda";
            ButtonProcessSale.Width = 150;

            FillFieldsWithSaleDetails();
            DisableComponents();
        }

        private void FillFieldsWithSaleDetails()
        {
            TextBoxDescription.Text = Model.Description;
            TextBoxDiscountValue.Text = Model.Discount.ToString("C2");
            TextBoxDiscountPercentage.Text = ((Model.Discount / Model.Items.Sum(item => item.TotalItem)) * 100).ToString("F2");
            TextBoxRemarks.Text = Model.Remarks;
            TextBoxTotal.Text = Model.Total.ToString("C2");

            SetDateTimePicker(Model.Date);
            SelectPaymentMethod(EnumUtility.GetEnumByDescription<PaymentMethod>(Model.PaymentMethod));

            Grid.DataSource = Model.Items.ToList();
        }

        private void DisableComponents()
        {
            TextBoxDescription.Enabled = false;
            TextBoxDiscountPercentage.Enabled = false;
            TextBoxRemarks.Enabled = false;
            DateTimePickerSaleDate.Enabled = false;
            ComboBoxPaymentMethod.Enabled = false;
            Grid.ReadOnly = true;

            DisableFooterFields();
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
                    UpdateTotalItem(cell.RowIndex);
                    UpdateTotal();
                    break;
                case 4:
                    UpdateTotalItem(cell.RowIndex);
                    UpdateTotal();
                    break;
                default:
                    break;
            }
        }

        private void Grid_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Mode == FormMode.Add)
                AddNewEmptyLineOnGrid();
        }

        private void ButtonProcessSale_Click(object sender, EventArgs e)
        {
            switch (Mode)
            {
                case FormMode.Add:
                    AddSale();
                    break;
                case FormMode.OnlyReadCanCancel:
                    CancelSale();
                    break;
                default:
                    break;
            }
        }

        private async void AddSale()
        {
            var (sale, errorMessage) = GetAddSaleViewModel();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }

            var (sucess, message) = await _saleService.Create(sale);

            MessageBox.Show(sucess
                ? "Venda adicionada com sucesso."
                : message);

            if (sucess)
            {
                ResetSaleForm();
                IsAdd = true;
                Date = sale.Date;
            }
        }

        private async void CancelSale()
        {
            var choice = MessageBox.Show(
                    $"Cancelar uma venda é um processo irreversível.{Environment.NewLine}" +
                    $"Todos os itens utilizados nesta venda, serão estornados ao estoque.{Environment.NewLine}" +
                    $"Deseja continuar?",
                    "Alerta",
                    MessageBoxButtons.YesNo);

            if (choice == DialogResult.Yes)
            {
                var (sucess, message) = await _saleService.Cancel(Model);

                MessageBox.Show(sucess
                    ? "Venda cancelada com sucesso."
                    : message);

                if (sucess)
                {
                    IsCanceled = true;
                    Date = Model.Date;
                    ButtonProcessSale.Enabled = false;
                }
            }
        }

        private void TextBoxDiscountPercentage_Validating(object sender, CancelEventArgs e)
        {
            bool isNumeric = decimal.TryParse(TextBoxDiscountPercentage.Text, out decimal discountPercentage);
            bool isValidDiscountPercentage = discountPercentage >= 0;

            e.Cancel = !isNumeric || !isValidDiscountPercentage;
        }

        private void TextBoxDiscountPercentage_Validated(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void ConfigureComponents()
        {
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
        }

        private void SelectPaymentMethod(PaymentMethod paymentMethod)
        {
            ComboBoxPaymentMethod.SelectedIndex = (int)paymentMethod;
        }

        private void DisableFooterFields()
        {
            TextBoxDiscountValue.Enabled = false;
            TextBoxTotal.Enabled = false;
        }

        private void ConfigureGrid()
        {
            Grid.DataSource = new List<SaleItemViewModel>() { new SaleItemViewModel() };
            Grid.Columns["Id"].Visible = false;
            Grid.Columns["SaleId"].Visible = false;
            Grid.Columns["ProductId"].Visible = false;
            Grid.Columns["ItemDescription"].HeaderText = "Item";
            Grid.Columns["Quantity"].HeaderText = "Quantidade";
            Grid.Columns["UnitOfMeasurement"].HeaderText = "Embalagem";
            Grid.Columns["UnitaryPrice"].HeaderText = "Preço unitário";
            Grid.Columns["TotalItem"].HeaderText = "Total item";
            Grid.Columns["UnitaryPrice"].DefaultCellStyle.Format = "C2";
            Grid.Columns["TotalItem"].DefaultCellStyle.Format = "C2";
            Grid.Columns["ItemDescription"].MinimumWidth = 310;
            Grid.Columns["UnitaryPrice"].MinimumWidth = 110;
            Grid.Columns["TotalItem"].MinimumWidth = 110;
            Grid.Columns["TotalItem"].ReadOnly = true;
            Grid.Columns["UnitaryPrice"].ReadOnly = true;
            Grid.Columns["UnitOfMeasurement"].ReadOnly = true;
            Grid.AutoResizeColumns();
        }

        private async void FillSaleItemLine(int rowIndex)
        {
            string name = GetCellValue(rowIndex, "ItemDescription")?.ToString() ?? string.Empty;

            if (string.IsNullOrEmpty(name))
                return;

            var products = await _productService.GetProducts(name);

            if (products is null)
                return;

            if (products.Count() > 1)
            {
                var selectedProduct = GetSelectedProduct(products);

                if (selectedProduct is null)
                    return;

                SetProductOnGrid(rowIndex, selectedProduct);
            }

            else if (products.Count() == 1)
                SetProductOnGrid(rowIndex, products.First());
        }

        private void AddNewEmptyLineOnGrid()
        {
            var current = (List<SaleItemViewModel>)Grid.DataSource;

            int lastRow = Grid.RowCount - 1;
            Guid.TryParse(GetCellValue(lastRow, "ProductId").ToString(), out Guid productId);

            if (productId == Guid.Empty)
                return;

            Grid.DataSource = new
                List<SaleItemViewModel>(current)
                {
                    new SaleItemViewModel()
                };
        }

        private void UpdateTotalItem(int rowIndex)
        {
            decimal unitaryPrice = (decimal)GetCellValue(rowIndex, "UnitaryPrice");
            decimal quantity = (decimal)GetCellValue(rowIndex, "Quantity");
            decimal total = unitaryPrice * quantity;

            SetCellValue(rowIndex, "TotalItem", total);
        }

        private void UpdateTotal()
        {
            decimal totalItems = GetTotalItems();

            SetDiscount(totalItems);
            SetTotalSale(totalItems);
        }

        public decimal GetTotalItems()
        {
            decimal totalItems = 0.0m;

            foreach (DataGridViewRow row in Grid.Rows)
            {
                decimal.TryParse(row.Cells["TotalItem"].Value?.ToString(), out decimal totalItem);
                totalItems += totalItem;
            }

            return totalItems;
        }

        private void SetDiscount(decimal totalItems)
        {
            decimal.TryParse(TextBoxDiscountPercentage.Text, out decimal discountPercentage);
            decimal discount = totalItems * discountPercentage / 100;

            TextBoxDiscountValue.Text = discount.ToString("C2");
        }

        private void SetTotalSale(decimal totalItems)
        {
            decimal.TryParse(TextBoxDiscountPercentage.Text, out decimal discountPercentage);
            decimal discount = totalItems * discountPercentage / 100;

            TextBoxTotal.Text = (totalItems - discount).ToString("C2");
        }

        private ProductViewModel? GetSelectedProduct(IEnumerable<ProductViewModel> dataSource)
        {
            using var cfl = new ChooseFromList();
            cfl.DataSource = dataSource;
            cfl.ShowDialog();

            return cfl?.Data is null
                  ? default
                  : (ProductViewModel)cfl.Data;
        }

        private void SetProductOnGrid(int rowIndex, ProductViewModel selectedProduct)
        {
            SetCellValue(rowIndex, "ProductId", selectedProduct.Id);
            SetCellValue(rowIndex, "ItemDescription", selectedProduct.Name);
            SetCellValue(rowIndex, "UnitOfMeasurement", selectedProduct.DefaultUoM);
            SetCellValue(rowIndex, "UnitaryPrice", selectedProduct.DefaultPrice);
        }

        private object GetCellValue(int rowIndex, string column)
        {
            return Grid
            .Rows[rowIndex]
            .Cells[column]
            .Value;
        }

        private void SetCellValue(int rowIndex, string column, object value)
        {
            Grid
            .Rows[rowIndex]
            .Cells[column]
            .Value = value;
        }

        private bool IsItemQuantityCell(DataGridViewCellValidatingEventArgs cell) => cell.ColumnIndex == 4;

        private (AddSaleViewModel sale, string errorMessage) GetAddSaleViewModel()
        {
            string errorMessage = string.Empty;

            string description = TextBoxDescription.Text;
            string paymentMethod = ComboBoxPaymentMethod.SelectedItem?.ToString() ?? string.Empty;
            decimal total = string.IsNullOrEmpty(TextBoxTotal.Text) ? 0 : decimal.Parse(TextBoxTotal.Text, NumberStyles.Currency);
            decimal discount = string.IsNullOrEmpty(TextBoxDiscountValue.Text) ? 0 : decimal.Parse(TextBoxDiscountValue.Text, NumberStyles.Currency);

            bool isInvalidDescription = string.IsNullOrEmpty(description);
            bool isInvalidPaymentMethod = string.IsNullOrEmpty(paymentMethod);
            bool isInvalidTotal = total <= 0;
            bool isInvaliddiscount = discount < 0;

            if (isInvalidDescription)
                errorMessage += $"A 'Descrição' deve ser preenchida.{Environment.NewLine}";

            if (isInvalidPaymentMethod)
                errorMessage += $"A 'Forma de pagamento' deve ser preenchida.{Environment.NewLine}";

            if (isInvalidTotal)
                errorMessage += $"O valor do campo 'Total' está inválido.{Environment.NewLine}";

            if (isInvaliddiscount)
                errorMessage += $"O valor do campo 'Desconto' está inválido.{Environment.NewLine}";

            var (items, message) = GetAddSaleItemsViewModel(errorMessage);

            if (!string.IsNullOrEmpty(message))
                return (new AddSaleViewModel(), message);

            return (
            new AddSaleViewModel()
            {
                Description = description,
                PaymentMethod = paymentMethod,
                Total = total,
                Discount = discount,
                Date = SetCurrentTime(DateTimePickerSaleDate.Value),
                Remarks = TextBoxRemarks.Text,
                Items = items,
            }, string.Empty);
        }

        private (IEnumerable<AddSaleItemViewModel> saleItems, string errorMessage) GetAddSaleItemsViewModel(string errorMessage)
        {
            var saleItems = new List<AddSaleItemViewModel>();

            foreach (DataGridViewRow row in Grid.Rows)
            {
                if (ShouldIgnoreRow(row.Index))
                    break;

                Guid.TryParse(row.Cells["ProductId"].Value?.ToString(), out Guid productId);

                if (productId == Guid.Empty)
                    errorMessage += $"O produto da linha {row.Index + 1} não foi selecionado.{Environment.NewLine}";

                decimal.TryParse(row.Cells["Quantity"].Value?.ToString(), out decimal quantity);

                if (quantity <= 0)
                    errorMessage += $"A quantidade da linha {row.Index + 1} não é válida.{Environment.NewLine}";

                decimal.TryParse(row.Cells["UnitaryPrice"].Value?.ToString(), out decimal unitaryPrice);
                decimal.TryParse(row.Cells["TotalItem"].Value?.ToString(), out decimal totalItem);

                saleItems.Add(
                    new AddSaleItemViewModel()
                    {
                        ProductId = productId,
                        ItemDescription = row.Cells["ItemDescription"].Value?.ToString(),
                        UnitOfMeasurement = row.Cells["UnitOfMeasurement"].Value?.ToString(),
                        Quantity = quantity,
                        UnitaryPrice = unitaryPrice,
                        TotalItem = totalItem
                    });
            }

            return (saleItems, errorMessage);
        }

        private bool ShouldIgnoreRow(int row)
        {
            bool isLastRow = row == Grid.RowCount - 1;
            bool isFirstRow = row == 0;

            if (!isLastRow || isFirstRow)
                return false;

            Guid.TryParse(GetCellValue(row, "ProductId").ToString(), out Guid productId);
            decimal.TryParse(GetCellValue(row, "Quantity").ToString(), out decimal quantity);

            bool hasNoProductSelected = productId == Guid.Empty;
            bool hasNoQuantityFilled = quantity == 0;

            return hasNoProductSelected && hasNoQuantityFilled;
        }

        private DateTime SetCurrentTime(DateTime date)
        {
            var now = DateTime.Now;

            return new DateTime(
                date.Year, date.Month, date.Day, 
                now.Hour, now.Minute, now.Second
                );
        }

        private void ResetSaleForm()
        {
            TextBoxDescription.Clear();
            TextBoxTotal.Clear();
            TextBoxDiscountValue.Clear();
            TextBoxDiscountPercentage.Clear();
            TextBoxRemarks.Clear();
            ComboBoxPaymentMethod.SelectedIndex = (int)PaymentMethod.All;
            SetDateTimePicker(DateTime.Today);
            Grid.DataSource = new List<SaleItemViewModel>() { new SaleItemViewModel() };
        }
    }
}